using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ASNC.Services;
using ASNC.Views;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;

namespace ASNC.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly ServiceProvider serviceProvider;

        public string Title => "Amiibo SN Changer - " + Assembly.GetExecutingAssembly().GetName().Version;

        public AmiiboViewModel AmiiboVM { get; }

        public AmiiboSelectorViewModel SelectorVM { get; }

        public ICommand OpenSettingsCommand { get; }

        public ICommand OpenAboutCommand { get; }

        public DelegateCommand OpenBulkExportCommand { get; }

        private BulkExportView? bulkExportView;

        public ShellViewModel(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            if (this.serviceProvider.Config.DownloadInfoDataOnStart)
            {
                this.serviceProvider.LibAmiibo.UpdateLocalAmiiboData();
            }

            this.OpenSettingsCommand = new DelegateCommand(() => this.ExecuteOpenSettings());
            this.OpenAboutCommand = new DelegateCommand(() => this.ExecuteOpenAbout());
            this.OpenBulkExportCommand = new DelegateCommand(() => this.ExecuteOpenBulkExport(), () => this.SelectorVM?.AmiiboTags.Count != 0 && this.serviceProvider.LibAmiibo.IsAmiiboKeyProvided);

            this.AmiiboVM = new AmiiboViewModel(this.serviceProvider);
            this.SelectorVM = new AmiiboSelectorViewModel(this.serviceProvider, this.AmiiboVM.SetAmiibo, () => this.OpenBulkExportCommand.RaiseCanExecuteChanged());
        }

        private void ExecuteOpenAbout()
        {
            var view = this.SetupView<AboutView>();
            view.DataContext = new AboutViewModel(view.Close);
            view.ShowDialog();
        }

        private void ExecuteOpenBulkExport()
        {
            if (this.bulkExportView == null)
            {
                this.bulkExportView = this.SetupView<BulkExportView>();
                this.bulkExportView.DataContext = new BulkExportViewModel(this.serviceProvider, this.bulkExportView.Hide, this.SelectorVM.AmiiboTags.ToArray());
            }

            (this.bulkExportView.DataContext as BulkExportViewModel)?.ResetData(this.SelectorVM.AmiiboTags.ToArray());
            this.bulkExportView.ShowDialog();
        }

        private void ExecuteOpenSettings()
        {
            var view = this.SetupView<SettingsView>();
            view.DataContext = new SettingsViewModel(this.serviceProvider, view.Close);
            view.ShowDialog();
            if (view.DataContext is SettingsViewModel vm)
            {
                this.SelectorVM.RefreshAllEntries();
                this.AmiiboVM.ExecuteReloadTag();
                this.OpenBulkExportCommand.RaiseCanExecuteChanged();
            }
        }

        private T SetupView<T>()
            where T : Window, new()
        {
            return new T()
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
        }
    }
}
