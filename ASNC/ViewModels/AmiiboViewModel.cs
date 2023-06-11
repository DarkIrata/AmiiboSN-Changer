using System;
using System.IO;
using ASNC.Helper;
using ASNC.Services;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;
using LibAmiibo.Data;
using Microsoft.Win32;

namespace ASNC.ViewModels
{
    public class AmiiboViewModel : ViewModelBase
    {
        private readonly ServiceProvider serviceProvider;

        private AmiiboTagSelectableViewModel? selectedAmiibo;

        public AmiiboTagSelectableViewModel? SelectedAmiibo
        {
            get => this.selectedAmiibo;
            set
            {
                this.Set(ref this.selectedAmiibo, value, nameof(this.SelectedAmiibo));
            }
        }

        public AmiiboTag? EditableTag { get; private set; }

        public bool IsDecrypted => this.EditableTag?.IsDecrypted ?? false;

        public string Name => this.EditableTag?.Amiibo?.RetailName ?? string.Empty;

        public string StatueId => this.EditableTag?.Amiibo?.StatueId ?? string.Empty;

        public string ReadableSerial => BitConverter.ToString(this.EditableTag?.NtagSerial.ToArray() ?? Array.Empty<byte>()).Replace('-', ' ');

        public string AmiiboNickname
        {
            get => this.TryGetFromUserData(this.EditableTag?.Settings?.UserData?.Nickname, string.Empty)!;
            set
            {
                if (!this.NicknameReadonly)
                {
                    this.EditableTag!.Settings.UserData.Nickname = value;
                }
                this.NotifyPropertyChanged(nameof(this.AmiiboNickname));
            }
        }

        public string MiiOwner
        {
            get => this.TryGetFromUserData(this.EditableTag?.Settings?.UserData?.OwnerMii?.Nickname, string.Empty)!;
            set
            {
                if (!this.MiiDataRadonly)
                {
                    this.EditableTag!.Settings.UserData.OwnerMii.Nickname = value;
                }
                this.NotifyPropertyChanged(nameof(this.MiiOwner));
            }
        }

        public string MiiAuthor
        {
            get => this.TryGetFromUserData(this.EditableTag?.Settings?.UserData?.OwnerMii?.AuthorNickname, string.Empty)!;
            set
            {
                if (!this.MiiDataRadonly)
                {
                    this.EditableTag!.Settings.UserData.OwnerMii.AuthorNickname = value;
                }
                this.NotifyPropertyChanged(nameof(this.MiiAuthor));
            }
        }

        public ushort? WriteCounter => this.IsDecrypted ? this.EditableTag?.Settings?.WriteCounter ?? 0 : null;

        public string RegistrationDate => this.GetSetupDate();

        public bool HasUserData => this.EditableTag?.HasUserData ?? false;

        public bool NicknameReadonly => !this.HasUserData;

        public bool MiiDataRadonly => !this.HasUserData || (this.EditableTag?.Settings?.UserData?.OwnerMii == null);

        public DelegateCommand RandomizeSerialNumberCommand { get; }

        public DelegateCommand WriteCounterDownCommand { get; }

        public DelegateCommand WriteCounterUpCommand { get; }

        public DelegateCommand ReloadTagCommand { get; }

        public DelegateCommand ExportTagCommand { get; }

        private int lastSelectedExportFilter = 0;

        public AmiiboViewModel(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            this.WriteCounterDownCommand = new DelegateCommand(() => this.AdjustWriteCounterBy(-1), () => this.IsDecrypted);
            this.WriteCounterUpCommand = new DelegateCommand(() => this.AdjustWriteCounterBy(1), () => this.IsDecrypted);

            this.ReloadTagCommand = new DelegateCommand(() => this.ExecuteReloadTag(), () => this.IsDecrypted);
            this.ExportTagCommand = new DelegateCommand(() => this.ExecuteExportTag(), () => this.IsDecrypted);

            this.RandomizeSerialNumberCommand = new DelegateCommand(() =>
            {
                this.EditableTag?.RandomizeUID();
                this.NotifyPropertyChanged(nameof(this.ReadableSerial));
            }, () => this.IsDecrypted);
        }

        private void ExecuteReloadTag()
        {
            if (this.SelectedAmiibo?.OriginalFileData != null)
            {
                if (this.serviceProvider.LibAmiibo?.IsAmiiboKeyProvided ?? false)
                {
                    this.EditableTag = this.serviceProvider!.LibAmiibo!.DecryptTag(this.SelectedAmiibo.OriginalFileData);
                }
                else
                {
                    this.EditableTag = this.serviceProvider!.LibAmiibo!.ReadEncryptedTag(this.SelectedAmiibo.OriginalFileData);
                }
            }
            else
            {
                this.EditableTag = null;
            }

            this.NotifyAllPropertyChanged();
            this.RandomizeSerialNumberCommand.RaiseCanExecuteChanged();
            this.WriteCounterDownCommand.RaiseCanExecuteChanged();
            this.WriteCounterUpCommand.RaiseCanExecuteChanged();
            this.ReloadTagCommand.RaiseCanExecuteChanged();
            this.ExportTagCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteExportTag()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = $"Amiibo Tag bin file (*.bin)|*.bin|Flipper NFC file (*{FlipperNFCHelper.FileType})|*{FlipperNFCHelper.FileType}|All files (*.*)|*.*",
                FilterIndex = this.lastSelectedExportFilter,
                InitialDirectory = Path.GetDirectoryName(this.SelectedAmiibo?.FilePath),
            };

            if (dialog.ShowDialog() == true)
            {
                this.lastSelectedExportFilter = dialog.FilterIndex;
                var newTag = this.serviceProvider.LibAmiibo.EncryptTag(this.EditableTag);

                if (dialog.FileName.EndsWith(FlipperNFCHelper.FileType))
                {
                    var nfcData = FlipperNFCHelper.ToNfc(newTag);
                    File.WriteAllText(dialog.FileName, nfcData);
                }
                else
                {
                    File.WriteAllBytes(dialog.FileName, newTag);
                }
            }
        }

        private void AdjustWriteCounterBy(short value)
        {
            if (this.IsDecrypted && this.EditableTag?.Settings != null)
            {
                var newValue = (this.WriteCounter.HasValue ? this.WriteCounter.Value : 0) + value;
                newValue = Math.Clamp(newValue, 0, 65534);
                this.EditableTag!.Settings!.WriteCounter = (ushort)newValue;
                this.NotifyPropertyChanged(nameof(this.WriteCounter));
            }
        }

        public void SetAmiibo(AmiiboTagSelectableViewModel? selectedAmiibo)
        {
            this.SelectedAmiibo = selectedAmiibo;
            this.ExecuteReloadTag();
        }

        private T TryGetFromUserData<T>(T data, T fallback)
        {
            if (this.HasUserData)
            {
                return data ?? fallback;
            }

            return fallback;
        }

        private string GetSetupDate()
        {
            if (this.HasUserData)
            {
                var result = this.TryGetFromUserData<DateTime?>(this.SelectedAmiibo?.AmiiboTag?.Settings?.UserData?.SetupDate, null);
                if (result.HasValue)
                {
                    return result.Value.ToString("d");
                }
            }

            return string.Empty;
        }
    }
}
