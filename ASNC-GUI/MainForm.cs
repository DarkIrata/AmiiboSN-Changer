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
                    if (!this.amiibos.Any(a => AmiiboSNHelper.EqualAmiiboTag(a.Amiibo, amiibo)))
                    {
                        this.amiibos.Add(new AmiiboTagWrapper(amiibo));
                    }
                    else
                    {
                        MessageBox.Show($"{file} was already loaded", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void AmiiboSNHelper_ActionOutput(object sender, ActionOutputEventArgs e)
        {
            if (!e.Successful)
            {
                Console.WriteLine(e.Output);
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
                this.amiiboImage.Image = amiiboTagWrapper.GetBitmap(256, 256);
            }
        }
    }
}
