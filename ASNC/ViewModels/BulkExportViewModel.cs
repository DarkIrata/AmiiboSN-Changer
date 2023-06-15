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

namespace ASNC.ViewModels
{
    public class BulkExportViewModel : ViewModelBase
    {
        private readonly ServiceProvider serviceProvider;
        private readonly Action close;
        private readonly AmiiboTagSelectableViewModel[]? tags;

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
            set => this.Set(ref this.selectedExportTargetType, value, nameof(this.SelectedExportTargetType));
        }

        public ICommand AmountUpCommand { get; set; }

        public ICommand AmountDownCommand { get; set; }

        public ICommand SelectOutputPathCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public DelegateCommand ExportCommand { get; set; }

        public bool? DialogResult { get; private set; } = null;

        private Dictionary<string, string> TargetFilestypes = new Dictionary<string, string>()
        {
            { ".bin", ".bin Binary" },
            { FlipperNFCHelper.Filetype, ".nfc Flipper NFC" },
        };

        public BulkExportViewModel(ServiceProvider serviceProvider, Action close, AmiiboTagSelectableViewModel[]? tags, int? lastSelectedExportTargetType = null)
        {
            this.serviceProvider = serviceProvider;
            this.close = close;
            this.tags = tags;

            if (lastSelectedExportTargetType != null)
            {
                this.SelectedExportTargetType = lastSelectedExportTargetType.Value;
            }
            else
            {
                this.SelectedExportTargetType = 0;
            }

            this.AmountUpCommand = new DelegateCommand(() => this.AdjustAmount(1));
            this.AmountDownCommand = new DelegateCommand(() => this.AdjustAmount(-1));
            this.SelectOutputPathCommand = new DelegateCommand(() => this.SelectOutputPath());
            this.CancelCommand = new DelegateCommand(() => this.ExecuteCancel());
            this.ExportCommand = new DelegateCommand(() => this.ExecuteExport(), () => !string.IsNullOrEmpty(this.OutputPath));
        }

        private void AdjustAmount(int value) => this.Amount = (this.numericAmount + value).ToString();

        private void ExecuteExport()
        {
            if (this.tags == null)
            {
                MessageBox.Show("No Amiibo's in selection list");
                return;
            }

            var targetFiletype = this.TargetFilestypes.ElementAt(this.SelectedExportTargetType).Key;
            foreach (var singleTag in this.tags)
            {
                var fileName = Path.GetFileNameWithoutExtension(singleTag.FilePath);
                var baseTag = this.serviceProvider.LibAmiibo.DecryptTag(singleTag.OriginalFileData);
                for (int i = 0; i < this.Amount; i++)
                {
                    baseTag.RandomizeUID();
                    var newFileName = $"{fileName}_asnc_{i}{targetFiletype}";
                    var path = Path.Combine(this.OutputPath!, newFileName);
                    var encryptedTag = this.serviceProvider.LibAmiibo.EncryptTag(baseTag);

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
            }

            var explorer = Process.Start("explorer.exe", this.OutputPath!);
            explorer.WaitForExit(1000);

            this.DialogResult = true;
            this.serviceProvider.Config.Save();
            this.close?.Invoke();
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

        internal void ResetDialog()
        {
            this.DialogResult = null;
        }
    }
}
