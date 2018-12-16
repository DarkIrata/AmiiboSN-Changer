using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmiiboSNChanger.Libs;
using AmiiboSNChanger.Libs.Events;
using LibAmiibo.Data;

namespace ASNC_GUI
{
    public partial class MainForm : Form
    {
        private BindingList<AmiiboTagWrapper> amiibos = new BindingList<AmiiboTagWrapper>();

        public MainForm()
        {
            this.InitializeComponent();
            this.CleanUpAmiiboDisplay();

            AmiiboSNHelper.ActionOutput += this.AmiiboSNHelper_ActionOutput;

            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.DataSource = this.amiibos;
        }

        private void AddAmiibos(string[] files)
        {
            foreach (var file in files)
            {
                var amiibo = AmiiboSNHelper.LoadAndDecryptNtag(file);
                if (amiibo != null)
                {
                    if (!this.amiibos.Any(a => AmiiboSNHelper.EqualAmiiboTag(a.AmiiboTag, amiibo)))
                    {
                        this.amiibos.Add(new AmiiboTagWrapper(file, amiibo));
                    }
                    else
                    {
                        MessageBox.Show($"{file} was already loaded", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void CleanUpAmiiboDisplay()
        {
            this.amiiboImage.Image = Properties.Resources.Empty;
            this.lblAmiiboName.Text = "Amiibo Name";

            this.tbStatueId.Clear();
            this.tbSerial.Clear();
            this.tbUID.Clear();
            this.lblWrites.Text = "Write Counter:";

            this.tbNickname.Clear();
            this.tbOwner.Clear();
            this.tbRegisterDate.Clear();

            this.tbDataSource.Clear();
            this.tbPlatform.Clear();
        }

        private void AmiiboSNHelper_ActionOutput(object sender, ActionOutputEventArgs e)
        {
            if (!e.Successful)
            {
                MessageBox.Show(e.Output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = "Amiibo backup files (*.bin)|*.bin|All files (*.*)|*.*";
                fd.Multiselect = true;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    this.AddAmiibos(fd.FileNames);
                }
            }
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDeleteCell(e.ColumnIndex, e.RowIndex))
            {
                var row = this.dataGrid.Rows[e.RowIndex];
                var amiibotTargWrapper = (AmiiboTagWrapper)row.DataBoundItem;
                if (MessageBox.Show($"Remove \"{amiibotTargWrapper.Name}\" from list?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.dataGrid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewColumn column in this.dataGrid.Columns)
            {
                this.dataGrid[column.Index, e.RowIndex].ToolTipText = column.ToolTipText;
            }
        }

        private void dataGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDeleteCell(e.ColumnIndex, e.RowIndex))
            {
                this.dataGrid[e.ColumnIndex, e.RowIndex].Value = Properties.Resources.garbage_enter;
            }
        }

        private void dataGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDeleteCell(e.ColumnIndex, e.RowIndex))
            {
                this.dataGrid[e.ColumnIndex, e.RowIndex].Value = Properties.Resources.garbage;
            }
        }

        private bool IsDeleteCell(int columnIndex, int rowIndex) => columnIndex >= 0 && rowIndex >= 0 && this.dataGrid.Columns[columnIndex].Name == nameof(this.DeleteAmiibo);

        private void dataGrid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
                {
                    if (files.All(f => f.EndsWith(".bin")))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }

        private void dataGrid_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
            {
                this.AddAmiibos(files);
            }
        }

        private void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGrid.SelectedRows.Count > 0)
            {
                var row = this.dataGrid.SelectedRows[0];
                var amiiboTagWrapper = (AmiiboTagWrapper)row.DataBoundItem;

                this.CleanUpAmiiboDisplay();

                this.amiiboImage.Image = amiiboTagWrapper.GetBitmap(170, 170);
                this.lblAmiiboName.Text = amiiboTagWrapper.Name;

                this.tbStatueId.Text = amiiboTagWrapper.AmiiboTag.Amiibo.StatueId;
                this.tbSerial.Text = AmiiboSNHelper.GetBytesAsString(amiiboTagWrapper.AmiiboTag.NtagSerial.ToArray());
                this.tbUID.Text = AmiiboSNHelper.GetBytesAsString(amiiboTagWrapper.AmiiboTag.UID);
                this.lblWrites.Text = "Write Counter: " + amiiboTagWrapper.AmiiboTag.WriteCounter.ToString();

                if (amiiboTagWrapper.AmiiboTag.HasUserData)
                {
                    this.tbNickname.Text = amiiboTagWrapper.AmiiboTag.AmiiboSettings.AmiiboUserData.AmiiboNickname;
                    this.tbOwner.Text = amiiboTagWrapper.AmiiboTag.AmiiboSettings.AmiiboUserData.OwnerMii.MiiNickname;
                    this.tbRegisterDate.Text = amiiboTagWrapper.AmiiboTag.AmiiboSettings.AmiiboUserData.AmiiboSetupDate.ToString("dd.MM.yyyy");
                }

                if (amiiboTagWrapper.AmiiboTag.HasAppData)
                {
                    this.tbDataSource.Text = "0" + amiiboTagWrapper.AmiiboTag.AmiiboSettings.AmiiboAppData.AppDataInitializationTitleID.TitleID.ToString("x2");
                    this.tbPlatform.Text = amiiboTagWrapper.AmiiboTag.AmiiboSettings.AmiiboAppData.AppDataInitializationTitleID.Platform.ToString();
                }
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            this.tbOutput.ReadOnly = true;
            this.btnExecute.Enabled = false;
            this.numUpDown.ReadOnly = true;

            try
            {
                var abort = false;
                foreach (var item in this.amiibos)
                {
                    for (int i = 0; i < this.numUpDown.Value; i++)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(item.FilePath) + AmiiboSNHelper.FileNameExtension + i + AmiiboSNHelper.FileType;
                        var savePath = Path.Combine(this.tbOutput.Text, fileName);
                        item.AmiiboTag.UID = AmiiboSNHelper.CustomRandomizeSerial();

                        if (!item.AmiiboTag.IsUidValid())
                        {
                            var diagResult = MessageBox.Show($"Created invalid UID for '{fileName}'{Environment.NewLine}Yes to skip current amiibo batch{Environment.NewLine}No to skip only faulty amiibo{Environment.NewLine}Cancel to abort generating", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (diagResult == DialogResult.Yes)
                            {
                                break;
                            }
                            else if (diagResult == DialogResult.Cancel)
                            {
                                abort = true;
                                break;
                            }
                        }
                        else
                        {
                            AmiiboSNHelper.EncryptNtag(savePath, item.AmiiboTag);
                        }
                    }

                    item.ReloadAmiibo();
                    
                    if (abort)
                    {
                        break;
                    }
                }

                MessageBox.Show($"Successfully generated {this.amiibos.Count * this.numUpDown.Value} Amiibos!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while generating" + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.tbOutput.ReadOnly = false;
                this.btnExecute.Enabled = true;
                this.numUpDown.ReadOnly = false;
            }
        }

        private void btnOutputSelect_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Application.StartupPath;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.tbOutput.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
