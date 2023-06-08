using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;
using LibAmiibo.Data;

namespace ASNC.ViewModels
{
    public class AmiiboTagSelectableViewModel : ViewModelBase
    {
        private const int PartialPathLength = 52;
        private readonly Action<AmiiboTagSelectableViewModel?> removeTag;

        private bool isSelected;

        public bool IsSelected
        {
            get => this.isSelected;
            set => this.Set(ref this.isSelected, value, nameof(this.IsSelected));
        }

        public string Name => this.AmiiboTag?.Amiibo?.RetailName ?? "Error loading..";

        public string ReadableSerial => BitConverter.ToString(this.AmiiboTag?.NtagSerial.ToArray() ?? Array.Empty<byte>()).Replace("-", string.Empty);

        public string PartialPath => this.FilePath.Length > PartialPathLength ? "..." + this.FilePath[^PartialPathLength..] : this.FilePath;

        public string FilePath { get; }

        public AmiiboTag AmiiboTag { get; }

        private BitmapImage? image;

        public BitmapImage? Image
        {
            get => this.image;
            set
            {
                this.Set(ref this.image, value, nameof(this.Image));
                this.ImageLoading = false;
            }
        }

        private bool imageLoading = false;

        public bool ImageLoading
        {
            get => this.imageLoading;
            set => this.Set(ref this.imageLoading, value, nameof(this.ImageLoading));
        }

        public byte[] OriginalFileData { get; }

        public ICommand RemoveTagCommand { get; }

        public AmiiboTagSelectableViewModel(AmiiboTag tag, BitmapImage emptyImage, string filePath, byte[] originalFileData, Action<AmiiboTagSelectableViewModel?> removeTag)
        {
            this.removeTag = removeTag;
            this.Image = emptyImage;
            this.AmiiboTag = tag;
            this.FilePath = filePath;
            this.OriginalFileData = originalFileData;
            this.RemoveTagCommand = new DelegateCommand(() => this.ExecuteRemoveTag());
        }

        private void ExecuteRemoveTag()
        {
            if (MessageBox.Show(
                $"Remove '{this.AmiiboTag?.Amiibo?.RetailName}' from list?",
                "Remove from list",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                this.removeTag(this);
            }
        }
    }
}
