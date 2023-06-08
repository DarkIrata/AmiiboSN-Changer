using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
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

        private int amount = 1;

        public int Amount
        {
            get => this.amount;
            set => this.Set(ref this.amount, value, nameof(this.Amount));
        }

        public ICommand AmountUpCommand { get; set; }

        public ICommand AmountDownCommand { get; set; }

        public ICommand SelectOutputPathCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public DelegateCommand ExportCommand { get; set; }

        public bool? DialogResult { get; private set; } = null;

        public BulkExportViewModel(ServiceProvider serviceProvider, Action close, AmiiboTagSelectableViewModel[]? tags)
        {
            this.serviceProvider = serviceProvider;
            this.close = close;
            this.tags = tags;

            this.AmountUpCommand = new DelegateCommand(() => this.AdjustAmount(1));
            this.AmountDownCommand = new DelegateCommand(() => this.AdjustAmount(-1));
            this.SelectOutputPathCommand = new DelegateCommand(() => this.SelectOutputPath());
            this.CancelCommand = new DelegateCommand(() => this.ExecuteCancel());
            this.ExportCommand = new DelegateCommand(() => this.ExecuteExport(), () => !string.IsNullOrEmpty(this.OutputPath));
        }

        private void AdjustAmount(int value) => this.Amount = Math.Clamp(this.Amount + value, 1, 1000);

        private void ExecuteExport()
        {
            if (this.tags == null)
            {
                MessageBox.Show("No Amiibo's in selection list");
                return;
            }

            foreach (var singleTag in this.tags)
            {
                var fileName = Path.GetFileNameWithoutExtension(singleTag.FilePath);
                var baseTag = this.serviceProvider.LibAmiibo.DecryptTag(singleTag.OriginalFileData);
                for (int i = 0; i < this.Amount; i++)
                {
                    baseTag.RandomizeUID();

                    var newFileName = $"{fileName}_asnc_{i}.bin";
                    var path = Path.Combine(this.OutputPath!, newFileName);
                    var encryptedTag = this.serviceProvider.LibAmiibo.EncryptTag(singleTag.AmiiboTag);
                    File.WriteAllBytes(path, encryptedTag);
                }
            }

            Process.Start("explorer.exe", this.OutputPath!);

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
    }
}
