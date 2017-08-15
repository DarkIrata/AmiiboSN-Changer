using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ASNC_GUI
{
    public partial class MainForm : Form
    {
        private const string asncTool = "asnc.exe";

        public MainForm()
        {
            this.InitializeComponent();
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
            foreach (string bin in this.listBins.Items)
            {
                if (!File.Exists("asnc.exe"))
                {
                    this.AppendOutputLine($"Missing asnc.exe in '{Application.StartupPath}'");
                }

                if (!File.Exists(bin))
                {
                    this.AppendOutputLine($"Couldn't find '{bin}'");
                }

                var args = $"\"{bin}\" {this.amount.Value}";
                if (!string.IsNullOrEmpty(this.tbOutputPath.Text))
                {
                    args += $" \"{this.tbOutputPath.Text}\"";
                }

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = asncTool,
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true
                    }
                };
                process.Start();
                process.WaitForExit();

                string data = process.StandardOutput.ReadToEnd();
                AppendOutputLine(data);
            }
        }

        private void AppendOutputLine(string text)
        {
            this.tbToolOutput.AppendText($"[{DateTime.Now}] {text}{Environment.NewLine}");
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
