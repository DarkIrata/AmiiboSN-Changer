using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using ASNC.Helper;
using ASNC.Services;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;
using LibAmiibo.Data;

namespace ASNC.ViewModels
{
    public class BulkExportViewModel : ViewModelBase
    {
        private readonly ServiceProvider serviceProvider;
        private readonly Action close;
        private readonly string defaultFilenameFormat = "<num> <file> (ASNC)";

        public string Title => "Amiibo SN Changer - Bulk Export";

        private string? outputPath;

        public string? OutputPath
        {
            get => this.outputPath;
            set
            {
                this.Set(ref this.outputPath, value, nameof(this.OutputPath));
                this.ExportCommand.RaiseCanExecuteChanged();
            }
        }

        private ushort numericAmount = 1;

        public string Amount
        {
            get => this.numericAmount.ToString();
            set
            {
                var maxValue = ushort.MaxValue;
                var adjustedValue = InputHelper.GetRangedNumericInput(value, maxValue.ToString().Length, 1, maxValue);
                if (adjustedValue.IsNumber)
                {
                    this.numericAmount = (ushort)adjustedValue.Result;
                    this.NotifyPropertyChanged(nameof(this.Amount));
                }
            }
        }

        public string[] ExportTargetTypes => this.TargetFilestypes.Values.ToArray();

        private int selectedExportTargetType;

        public int SelectedExportTargetType
        {
            get => this.selectedExportTargetType;
            set
            {
                this.Set(ref this.selectedExportTargetType, value, nameof(this.SelectedExportTargetType));
                this.OutputFilenameFormat = this.OutputFilenameFormat;
            }
        }

        private bool openFolderWhenFinished;

        public bool OpenFolderWhenFinished
        {
            get => this.openFolderWhenFinished;
            set => this.Set(ref this.openFolderWhenFinished, value, nameof(this.OpenFolderWhenFinished));
        }

        private bool subFolderPerTag;

        public bool SubFolderPerTag
        {
            get => this.subFolderPerTag;
            set => this.Set(ref this.subFolderPerTag, value, nameof(this.SubFolderPerTag));
        }

        private string? outputFilenameFormat;

        public string? OutputFilenameFormat
        {
            get => this.outputFilenameFormat;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1 || value.Length > 50)
                {
                    return;
                }

                var oldValue = this.OutputFilenameFormat;
                this.Set(ref this.outputFilenameFormat, value, nameof(this.OutputFilenameFormat));
                var formated = this.GetFormatedFilename("mario8bit", "012", "Mario (8-Bit)", "Mario", "0000000002380602");
                if (formated.Length > 0)
                {
                    if (formated.Length > 40)
                    {
                        formated = formated[..40] + "[..]";
                    }

                    this.ExampleFilename = formated + this.TargetFilestypes.ElementAt(this.SelectedExportTargetType).Key;
                }
                else
                {
                    this.Set(ref this.outputFilenameFormat, oldValue, nameof(this.OutputFilenameFormat));
                }
            }
        }

        private string? exampleFilename;

        public string? ExampleFilename // Not a readonly property, because NotifyPropertyChanged(propertyname) didnt update the label
        {
            get => this.exampleFilename;
            set => this.Set(ref this.exampleFilename, value, nameof(this.ExampleFilename));
        }

        public ICommand AmountUpCommand { get; set; }

        public ICommand AmountDownCommand { get; set; }

        public ICommand SelectOutputPathCommand { get; set; }

        public ICommand ResetFilenameFormatCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public DelegateCommand ExportCommand { get; set; }

        public bool? DialogResult { get; private set; } = null;

        private AmiiboTagSelectableViewModel[]? tags;

        private Dictionary<string, string> TargetFilestypes = new Dictionary<string, string>()
        {
            { ".bin", ".bin Binary" },
            { FlipperNFCHelper.Filetype, ".nfc Flipper NFC" },
        };

        public BulkExportViewModel(ServiceProvider serviceProvider, Action close, AmiiboTagSelectableViewModel[]? tags)
        {
            this.serviceProvider = serviceProvider;
            this.close = close;
            this.tags = tags;

            this.ExecuteResetFilenameFormat();

            this.AmountUpCommand = new DelegateCommand(() => this.AdjustAmount(1));
            this.AmountDownCommand = new DelegateCommand(() => this.AdjustAmount(-1));
            this.SelectOutputPathCommand = new DelegateCommand(() => this.SelectOutputPath());
            this.ResetFilenameFormatCommand = new DelegateCommand(() => this.ExecuteResetFilenameFormat());
            this.CancelCommand = new DelegateCommand(() => this.ExecuteCancel());
            this.ExportCommand = new DelegateCommand(() => this.ExecuteExport(), () => !string.IsNullOrEmpty(this.OutputPath));
        }

        private void ExecuteResetFilenameFormat() => this.OutputFilenameFormat = this.defaultFilenameFormat;

        private void AdjustAmount(int value) => this.Amount = (this.numericAmount + value).ToString();

        private void ExecuteExport()
        {
            if (this.tags == null)
            {
                MessageBox.Show("No Amiibo's in selection list");
                return;
            }

            if (string.IsNullOrEmpty(this.OutputPath))
            {
                MessageBox.Show("No Output Path set");
                return;
            }

            var targetFiletype = this.TargetFilestypes.ElementAt(this.SelectedExportTargetType).Key;
            foreach (var singleTag in this.tags)
            {
                var filename = Path.GetFileNameWithoutExtension(singleTag.FilePath);
                var baseTag = this.serviceProvider.LibAmiibo.DecryptTag(singleTag.OriginalFileData);
                if (baseTag == null)
                {
                    continue;
                }

                var leadingZeroHelper = "D" + this.Amount.Length;
                var subFolderName = this.GetOutputSubFolderName(baseTag);

                for (int i = 0; i < this.numericAmount; i++)
                {
                    this.CreateTag(baseTag, i, filename, targetFiletype, leadingZeroHelper, subFolderName);
                }
            }

            if (this.OpenFolderWhenFinished)
            {
                var explorer = Process.Start("explorer.exe", this.OutputPath!);
                explorer.WaitForExit(1000);
            }

            this.DialogResult = true;
            this.serviceProvider.Config.Save();
            this.close?.Invoke();
        }

        private void CreateTag(AmiiboTag tag, int i, string filename, string targetFiletype, string leadingZeroHelper, string? subFolderName)
        {
            tag!.RandomizeUID();

            var num = (i + 1).ToString(leadingZeroHelper);
            var newFileName = this.GetFormatedFilename(filename, num, tag?.Amiibo?.RetailName, tag?.Amiibo?.StatueName, tag?.Amiibo?.StatueId);
            newFileName += targetFiletype;

            var path = Path.Combine(this.OutputPath!, subFolderName ?? string.Empty, newFileName);
            var encryptedTag = this.serviceProvider.LibAmiibo.EncryptTag(tag);

            if (path.EndsWith(FlipperNFCHelper.Filetype))
            {
                var nfcData = FlipperNFCHelper.ToNfc(encryptedTag);
                File.WriteAllText(path, nfcData);
            }
            else
            {
                File.WriteAllBytes(path, encryptedTag);
            }
        }

        private string? GetOutputSubFolderName(AmiiboTag tag)
        {
            if (this.SubFolderPerTag)
            {
                var subFolderName = tag?.Amiibo?.RetailName;
                if (!string.IsNullOrEmpty(subFolderName))
                {
                    var subFolderPath = Path.Combine(this.OutputPath!, subFolderName);
                    var subFolderSuffix = string.Empty;
                    if (Directory.Exists(subFolderPath))
                    {
                        var dirs = Directory.GetDirectories(this.OutputPath!);
                        var dirCount = dirs.Count(d => Path.GetFileName(d)!.StartsWith(subFolderName));
                        if (dirCount > 0)
                        {
                            subFolderSuffix = $" {dirCount}";
                        }
                    }

                    Directory.CreateDirectory(subFolderPath + subFolderSuffix);
                    return subFolderName + subFolderSuffix;
                }
            }

            return null;
        }

        private void ExecuteCancel()
        {
            this.DialogResult = false;
            this.close?.Invoke();
        }

        private void SelectOutputPath()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Application.StartupPath;
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.OutputPath = fbd.SelectedPath;
                }
            }
        }

        private string GetFormatedFilename(string filename, string num, string? retailName = null, string? statueName = null, string? statueId = null)
        {
            var baseName = this.OutputFilenameFormat ?? string.Empty;
            var replaced = baseName.Replace("<file>", filename)
                                   .Replace("<num>", num.ToString())
                                   .Replace("<retail>", retailName ?? string.Empty)
                                   .Replace("<statue>", statueName ?? string.Empty)
                                   .Replace("<statueid>", statueId ?? string.Empty);
            var invalids = Path.GetInvalidFileNameChars();
            var newName = string.Join("_", replaced.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            return newName.Trim();

        }

        internal void ResetData(AmiiboTagSelectableViewModel[]? tags)
        {
            this.tags = tags;
            this.DialogResult = null;
        }
    }
}
