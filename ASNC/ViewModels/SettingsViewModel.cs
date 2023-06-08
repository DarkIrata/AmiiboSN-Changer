using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using ASNC.Services;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;
using LibAmiibo.Data.AmiiboAPI;
using Microsoft.Win32;

namespace ASNC.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ServiceProvider serviceProvider;
        private readonly Action close;
        private AmiiboApiData? apiData;
        private LibAmiibo.LibAmiibo? testAmiibo;
        private bool blockButtons = false;

        public string Title => "Amiibo SN Changer - Settings";

        private string? keyFilePath;

        public string? KeyFilePath
        {
            get => this.keyFilePath;
            set
            {
                this.Set(ref this.keyFilePath, value, nameof(this.KeyFilePath));
                try
                {
                    this.testAmiibo = new LibAmiibo.LibAmiibo(value);
                }
                catch (System.Exception)
                {
                    this.testAmiibo = null;
                }
                this.NotifyPropertyChanged(nameof(this.IsGivenKeyValid));
                this.NotifyPropertyChanged(nameof(this.IsGivenKeyInvalid));
            }
        }

        private bool downloadMissingImagesOnLoad;

        public bool DownloadMissingImagesOnLoad
        {
            get => this.downloadMissingImagesOnLoad;
            set => this.Set(ref this.downloadMissingImagesOnLoad, value, nameof(this.DownloadMissingImagesOnLoad));
        }

        private bool downloadInfoDataOnStart;

        public bool DownloadInfoDataOnStart
        {
            get => this.downloadInfoDataOnStart;
            set => this.Set(ref this.downloadInfoDataOnStart, value, nameof(this.DownloadInfoDataOnStart));
        }

        public bool IsGivenKeyValid => this.testAmiibo?.IsAmiiboKeyProvided ?? false;

        public bool IsGivenKeyInvalid => !this.IsGivenKeyValid;

        public int AmiiboDataCount => this.apiData?.Amiibos?.Count ?? 0;

        public int AmiiboImagesCount => Directory.GetFiles("./AmiiboApi/Images").Length;

        public ICommand SelectRetailKeyCommand { get; set; }

        public DelegateCommand OpenApiFolderCommand { get; set; }

        public DelegateCommand UpdateApiDataCommand { get; set; }

        public ICommand DownloadAllImagesCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public bool? DialogResult { get; private set; } = null;

        public SettingsViewModel(ServiceProvider serviceProvider, Action close)
        {
            this.serviceProvider = serviceProvider;
            this.close = close;

            this.KeyFilePath = this.serviceProvider.Config.RetailKeyPath;
            this.DownloadMissingImagesOnLoad = this.serviceProvider.Config.DownloadMissingImagesOnLoad;
            this.DownloadInfoDataOnStart = this.serviceProvider.Config.DownloadInfoDataOnStart;

            this.NotifyPropertyChanged(nameof(this.AmiiboImagesCount));
            Task.Run(async () =>
            {
                this.apiData = await this.serviceProvider.LibAmiibo.GetAmiiboApiData();
                this.NotifyPropertyChanged(nameof(this.AmiiboDataCount));
            });

            this.SelectRetailKeyCommand = new DelegateCommand(() => this.ExecuteSelectRetailKey());
            this.OpenApiFolderCommand = new DelegateCommand(() => this.ExecuteOpenApiFolder(), () => !this.blockButtons);
            this.UpdateApiDataCommand = new DelegateCommand(() => this.ExecuteUpdateApiData(), () => !this.blockButtons);
            this.DownloadAllImagesCommand = new DelegateCommand(() => this.ExecuteDownloadAllImages());
            this.CancelCommand = new DelegateCommand(() => this.ExecuteCancel(), () => !this.blockButtons);
            this.SaveCommand = new DelegateCommand(() => this.ExecuteSave(), () => !this.blockButtons);
        }

        private void RaiseCommands()
        {
            this.OpenApiFolderCommand.RaiseCanExecuteChanged();
            this.UpdateApiDataCommand.RaiseCanExecuteChanged();
            this.CancelCommand.RaiseCanExecuteChanged();
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteSave()
        {
            this.serviceProvider.Config.RetailKeyPath = this.keyFilePath;
            this.serviceProvider.Config.DownloadInfoDataOnStart = this.DownloadInfoDataOnStart;
            this.serviceProvider.Config.DownloadMissingImagesOnLoad = this.DownloadMissingImagesOnLoad;

            this.DialogResult = true;
            this.serviceProvider.Config.Save();
            this.close?.Invoke();
        }

        private void ExecuteCancel()
        {
            this.DialogResult = false;
            this.close?.Invoke();
        }

        private void ExecuteDownloadAllImages()
        {
            throw new NotImplementedException();
        }

        private async void ExecuteUpdateApiData()
        {
            this.blockButtons = true;
            this.RaiseCommands();

            await this.serviceProvider.LibAmiibo.UpdateLocalAmiiboData();
            await this.serviceProvider.LibAmiibo.RefreshInfoDataProvider();
            this.apiData = await this.serviceProvider.LibAmiibo.GetAmiiboApiData();

            this.NotifyPropertyChanged(nameof(this.AmiiboDataCount));
            this.blockButtons = false;
            this.RaiseCommands();
        }

        private void ExecuteOpenApiFolder()
        {
            this.blockButtons = true;
            this.RaiseCommands();
            Process.Start("explorer.exe", Path.GetFullPath("./AmiiboApi"));
            this.blockButtons = false;
            this.RaiseCommands();
        }

        private void ExecuteSelectRetailKey()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Key bin file (*.bin)|*.bin|All files (*.*)|*.*",
                InitialDirectory = Assembly.GetExecutingAssembly().Location,
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                this.KeyFilePath = dialog.FileName;
            }
        }
    }
}
