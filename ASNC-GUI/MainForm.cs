using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmiiboSN_Changer;
using ASNC_GUI.Extensions;

namespace ASNC_GUI
{
    public partial class MainForm : Form
    {
        private const string amiitoolFilePath = "amiitool.exe";
        private const string retailKeyFilePath = "key_retail.bin";

        private AmiiToolHelper amiiTool = null;
        private bool gotAmiiToolError = false;

        public MainForm()
        {
            this.InitializeComponent();

            if (!File.Exists(amiitoolFilePath))
            {
                this.rtbOutput.WriteErrorLine($"Couldn't find '{amiitoolFilePath}' next to asnc.exe");
                this.SetControls(false);
            }

            if (!File.Exists(retailKeyFilePath))
            {
                this.rtbOutput.WriteErrorLine($"Couldn't find '{retailKeyFilePath}' next to asnc.exe");
                this.SetControls(false);
            }

            this.tbOutputPath.Text = Application.StartupPath;
            this.amiiTool = new AmiiToolHelper(amiitoolFilePath, retailKeyFilePath);
            this.amiiTool.AmiiToolResult += this.AmiiTool_AmiiToolResult;
        }

        private void AmiiTool_AmiiToolResult(object sender, AmiiboSN_Changer.Events.AmiiToolResultEventArgs e)
        {
            if (!e.IsError)
            {
                return;
            }

            this.rtbOutput.WriteErrorLine("Error while executing amiitool");
            this.rtbOutput.WriteErrorLine($"Arguments used: '{e.Args}'");
            this.rtbOutput.WriteErrorLine($"Error Message received: '{e.Result}'");
            this.gotAmiiToolError = true;
        }

        private void SetControls(bool enabled)
        {
            this.btnAdd.Enabled = enabled;
            this.btnDel.Enabled = enabled;
            this.btnConvert.Enabled = enabled;
            this.btnOutput.Enabled = enabled;
            this.tbOutputPath.ReadOnly = !enabled;
            this.amount.ReadOnly = !enabled;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = "Amiibo backup files (*.bin)|*.bin|All files (*.*)|*.*";
                fd.Multiselect = true;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in fd.FileNames)
                    {
                        this.listBins.Items.Add(file);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.listBins.Items.Count == 0)
            {
                return;
            }

            var selectedItems = this.listBins.SelectedItems.Cast<object>().ToList();
            foreach (var item in selectedItems)
            {
                this.listBins.Items.Remove(item);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            this.rtbOutput.Clear();
            this.amiiTool.AfterSwitchV5Update = this.afterSwitch5Update.Checked;
            this.SetControls(false);

            Task.Run(() =>
            {

                foreach (string amiiboFilePath in this.listBins.Items)
                {
                    this.rtbOutput.WriteInfoLine($"Decrypting: {Path.GetFileNameWithoutExtension(amiiboFilePath)}");
                    var decryptedAmiiboFilePath = this.amiiTool.DecryptAmiibo(amiiboFilePath);
                    this.rtbOutput.WriteDoneLine($"Decrypting: {Path.GetFileNameWithoutExtension(amiiboFilePath)}", true);

                    for (int nr = 1; nr <= this.amount.Value; nr++)
                    {
                        try
                        {
                            this.rtbOutput.WriteInfoLine($"[Amiibo ({nr})] Generating new Serial");
                            this.amiiTool.GenerateSerial(out byte[] serial, out byte firstChecksum, out byte secondChecksum);
                            this.rtbOutput.WriteDoneLine($"[Amiibo ({nr})] Generating new Serial", true);

                            this.rtbOutput.WriteInfoLine($"[Amiibo ({nr})] Writing Serial to Amiibo");
                            this.amiiTool.SetAmiiboSerial(decryptedAmiiboFilePath, serial, firstChecksum, secondChecksum);
                            this.rtbOutput.WriteDoneLine($"[Amiibo ({nr})] Writing Serial to Amiibo", true);

                            var newAmiiboFileName = $"{Path.GetFileNameWithoutExtension(amiiboFilePath)}_ASNC-{nr}.bin";
                            var outputPath = Path.Combine(Path.GetFullPath(this.tbOutputPath.Text), newAmiiboFileName);
                            this.rtbOutput.WriteInfoLine($"[Amiibo ({nr})] Encrypting: {newAmiiboFileName}");
                            this.amiiTool.EncryptAmiibo(decryptedAmiiboFilePath, outputPath);
                            this.rtbOutput.WriteDoneLine($"[Amiibo ({nr})] Encrypting: {newAmiiboFileName}", true);

                            if (this.gotAmiiToolError)
                            {
                                break;
                            }

                            if (nr != this.amount.Value)
                            {
                                this.Invoke((Action)(() => this.rtbOutput.AppendText(Environment.NewLine)));
                            }
                        }
                        catch (Exception ex)
                        {
                            this.rtbOutput.WriteErrorLine($"Error processing for Amiibo ({nr})");
                            this.rtbOutput.WriteErrorLine(ex.Message);
                        }
                    }

                    this.rtbOutput.WriteInfoLine($"Cleaning up {Path.GetFileNameWithoutExtension(amiiboFilePath)}");
                    this.amiiTool.CleanUp(amiiboFilePath, true);
                    this.rtbOutput.WriteDoneLine($"Cleaning up {Path.GetFileNameWithoutExtension(amiiboFilePath)}", true);
                    if (this.gotAmiiToolError)
                    {
                        break;
                    }

                    this.Invoke((Action)(() => this.rtbOutput.AppendText(Environment.NewLine)));
                }

                this.Invoke((Action)(() => this.SetControls(true)));
            });
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Application.StartupPath;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.tbOutputPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
