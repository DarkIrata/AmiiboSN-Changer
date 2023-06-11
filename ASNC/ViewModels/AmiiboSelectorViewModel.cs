using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ASNC.Controls;
using ASNC.Helper;
using ASNC.Services;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;
using LibAmiibo.Data;
using Microsoft.Win32;

namespace ASNC.ViewModels
{
    public class AmiiboSelectorViewModel : ViewModelBase, IFileDragDropTarget
    {
        private readonly ServiceProvider serviceProvider;
        private readonly ImageQueueService imageQueueService;
        private readonly Action<AmiiboTagSelectableViewModel?> onSelectedAmiiboChanged;
        private readonly Action onListChanged;

        public ObservableCollection<AmiiboTagSelectableViewModel> AmiiboTags { get; set; } = new ObservableCollection<AmiiboTagSelectableViewModel>();

        private AmiiboTagSelectableViewModel? selectedItem;

        public AmiiboTagSelectableViewModel? SelectedItem
        {
            get => this.selectedItem;
            set
            {
                if (this.SelectedItem != null)
                {
                    this.SelectedItem.IsSelected = false;
                }

                this.Set(ref this.selectedItem, value, nameof(this.SelectedItem));

                if (this.SelectedItem != null)
                {
                    this.SelectedItem.IsSelected = true;
                }

                this.onSelectedAmiiboChanged?.Invoke(this.SelectedItem);
            }
        }

        public ICommand LoadTagCommand { get; }

        public AmiiboSelectorViewModel(ServiceProvider serviceProvider, Action<AmiiboTagSelectableViewModel?> onSelectedAmiiboChanged, Action onListChanged)
        {
            this.serviceProvider = serviceProvider;
            this.imageQueueService = new ImageQueueService(this.serviceProvider);
            this.onSelectedAmiiboChanged = onSelectedAmiiboChanged;
            this.onListChanged = onListChanged;

            this.LoadTagCommand = new DelegateCommand(() => this.ExecuteLoadTag());
        }

        private void RemoveTag(AmiiboTagSelectableViewModel? tag)
        {
            if (tag != null)
            {
                this.AmiiboTags.Remove(tag);
                this.SelectedItem = null;
                this.onListChanged?.Invoke();
            }
        }

        private async void ExecuteLoadTag()
        {
            var dialog = new OpenFileDialog
            {
                Filter = $"Amiibo Tag bin file (*.bin)|*.bin|Flipper NFC file (*{FlipperNFCHelper.FileType})|*{FlipperNFCHelper.FileType}|All files (*.*)|*.*",
                InitialDirectory = Assembly.GetExecutingAssembly().Location,
                Multiselect = true
            };

            if (dialog.ShowDialog() == true)
            {
                await this.AddTags(dialog.FileNames);
            }
        }

        public async void OnFileDrop(string[] filepaths) => await this.AddTags(filepaths);

        private async Task AddTags(string[] fileNames)
        {
            AmiiboTagSelectableViewModel? lastEntry = null;
            foreach (var fileName in fileNames)
            {
                lastEntry = await this.AddTag(fileName);
            }

            if (this.SelectedItem == null)
            {
                this.SelectedItem = lastEntry;
            }

            this.onListChanged?.Invoke();
        }

        public async Task<AmiiboTagSelectableViewModel?> AddTag(string filePath)
        {
            AmiiboTagSelectableViewModel? entry = null;
            try
            {
                byte[] bytes = this.GetBytesFromFile(filePath);
                AmiiboTag tag = this.GetAmiiboTagFromBytes(bytes);
                entry = new AmiiboTagSelectableViewModel(tag, this.serviceProvider.EmptyImage, filePath, bytes, this.RemoveTag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error adding Amiibo Tag.{Environment.NewLine}Tag: '{filePath}'{Environment.NewLine}{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            if (entry != null)
            {
                await Application.Current.Dispatcher.InvokeAsync(() => this.AmiiboTags.Add(entry));
                if (this.serviceProvider.Config.DownloadMissingImagesOnLoad)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    // We want this to be on its own thread so we down block loading multiple amiibos without loading them also parallel
                    Task.Run(async () =>
                    {
                        await this.TryLoadImage(entry);
                    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
            }

            return entry;
        }

        private byte[] GetBytesFromFile(string filePath)
        {
            if (filePath.EndsWith(FlipperNFCHelper.FileType))
            {
                var nfcData = File.ReadAllText(filePath);
                return FlipperNFCHelper.ToBin(nfcData);
            }

            return File.ReadAllBytes(filePath);
        }

        private AmiiboTag GetAmiiboTagFromBytes(byte[] bytes)
        {
            if (this.serviceProvider.LibAmiibo.IsAmiiboKeyProvided)
            {
                return this.serviceProvider.LibAmiibo.DecryptTag(bytes);
            }

            return this.serviceProvider.LibAmiibo.ReadEncryptedTag(bytes);
        }

        private async Task TryLoadImage(AmiiboTagSelectableViewModel entry)
        {
            var statueId = entry.AmiiboTag?.Amiibo?.StatueId;
            if (statueId != null)
            {
                entry.ImageLoading = true;
                try
                {
                    await this.imageQueueService.TryGetImage(statueId, true, async (imageFilePath) =>
                    {
                        if (imageFilePath != null)
                        {
                            var fullPath = Path.GetFullPath(imageFilePath);
                            var uri = new Uri(fullPath);
                            await Application.Current.Dispatcher.InvokeAsync(() =>
                            {
                                try
                                {
                                    entry.Image = new BitmapImage(uri);
                                }
                                catch // Since we are on the UI Thread, we need to catch this exception here
                                {
                                }
                                finally
                                {
                                    entry.ImageLoading = false;
                                }
                            });
                        }
                        else
                        {
                            entry.ImageLoading = false;
                        }
                    });
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
        }
    }
}
