using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
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

        private bool working;

        public bool Working
        {
            get => this.working;
            set => this.Set(ref this.working, value, nameof(this.Working));
        }

        private string workingSubText = string.Empty;

        public string WorkingSubText
        {
            get => this.workingSubText;
            set => this.Set(ref this.workingSubText, value, nameof(this.WorkingSubText));
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
                this.apiData = await this.serviceProvider.LibAmiibo.GetAmiiboApiDataAsync();
                this.NotifyPropertyChanged(nameof(this.AmiiboDataCount));
            });

            this.SelectRetailKeyCommand = new DelegateCommand(() => this.ExecuteSelectRetailKey());
            this.OpenApiFolderCommand = new DelegateCommand(() => this.ExecuteOpenApiFolder(), () => !this.Working);
            this.UpdateApiDataCommand = new DelegateCommand(() => this.ExecuteUpdateApiData(), () => !this.Working);
            this.DownloadAllImagesCommand = new DelegateCommand(() => this.ExecuteDownloadAllImages());
            this.CancelCommand = new DelegateCommand(() => this.ExecuteCancel(), () => !this.Working);
            this.SaveCommand = new DelegateCommand(() => this.ExecuteSave(), () => !this.Working);
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
            this.serviceProvider.Config.RetailKeyPath = this.KeyFilePath;
            this.serviceProvider.Config.DownloadInfoDataOnStart = this.DownloadInfoDataOnStart;
            this.serviceProvider.Config.DownloadMissingImagesOnLoad = this.DownloadMissingImagesOnLoad;

            this.DialogResult = true;
            this.serviceProvider.ReinitializeLibAmiibo();
            this.serviceProvider.Config.Save();
            this.close?.Invoke();
        }

        private void ExecuteCancel()
        {
            this.DialogResult = false;
            this.close?.Invoke();
        }

        private async void ExecuteDownloadAllImages()
        {
            if (MessageBox.Show(
                "Are you sure, you want to download all Amiibo Images?",
                "Download images",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Working = true;
                this.WorkingSubText = "Downloading images";
                try
                {
                    await this.serviceProvider.LibAmiibo.UpdateMissingLocalAmiiboImages();
                    await Task.Delay(2000);
                    this.WorkingSubText = "Try ensure images";
                    await this.serviceProvider.LibAmiibo.UpdateMissingLocalAmiiboImages();
                    this.WorkingSubText = "Finishing";
                    await Task.Delay(1000);
                }
                catch
                {
                    this.WorkingSubText = "Something went wrong. Please try again..";
                    await Task.Delay(2000);
                }
                this.NotifyPropertyChanged(nameof(this.AmiiboImagesCount));
                this.Working = false;
            }
        }

        private async void ExecuteUpdateApiData()
        {
            this.Working = true;
            this.RaiseCommands();
            this.WorkingSubText = "Downloading json";
            await this.serviceProvider.LibAmiibo.UpdateLocalAmiiboData();
            this.WorkingSubText = "Refreshing Data";
            await this.serviceProvider.LibAmiibo.RefreshInfoDataProvider();
            this.WorkingSubText = "Getting Data";
            this.apiData = await this.serviceProvider.LibAmiibo.GetAmiiboApiDataAsync();

            this.WorkingSubText = string.Empty;
            this.NotifyPropertyChanged(nameof(this.AmiiboDataCount));
            this.Working = false;
            this.RaiseCommands();
        }

        private void ExecuteOpenApiFolder()
        {
            this.Working = true;
            this.RaiseCommands();
            Process.Start("explorer.exe", Path.GetFullPath("./AmiiboApi"));
            this.Working = false;
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
