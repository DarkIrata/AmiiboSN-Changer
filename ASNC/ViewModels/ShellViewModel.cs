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

        private int? lastSelectedExportTargetType = null;

        public ShellViewModel(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            if (this.serviceProvider.Config.DownloadInfoDataOnStart)
            {
                this.serviceProvider.LibAmiibo.UpdateLocalAmiiboData();
            }

            this.OpenSettingsCommand = new DelegateCommand(() => this.ExecuteOpenSettings());
            this.OpenAboutCommand = new DelegateCommand(() => this.ExecuteOpenAbout());
            this.OpenBulkExportCommand = new DelegateCommand(() => this.ExecuteOpenBulkExport(), () => this.SelectorVM?.AmiiboTags.Count != 0);

            this.AmiiboVM = new AmiiboViewModel(this.serviceProvider);
            this.SelectorVM = new AmiiboSelectorViewModel(this.serviceProvider, this.AmiiboVM.SetAmiibo, () => this.OpenBulkExportCommand.RaiseCanExecuteChanged());
        }

        private void ExecuteOpenAbout()
        {
            var view = new AboutView
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            view.DataContext = new AboutViewModel(view.Close);
            view.ShowDialog();
        }

        private void ExecuteOpenBulkExport()
        {
            var view = new BulkExportView
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            view.DataContext = new BulkExportViewModel(this.serviceProvider, view.Close, this.SelectorVM.AmiiboTags.ToArray(), this.lastSelectedExportTargetType);
            view.ShowDialog();
            if (view.DataContext is BulkExportViewModel vm && vm.DialogResult == true)
            {
                this.lastSelectedExportTargetType = vm.SelectedExportTargetType;
            }
        }

        private void ExecuteOpenSettings()
        {
            var view = new SettingsView
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            view.DataContext = new SettingsViewModel(this.serviceProvider, view.Close);
            view.ShowDialog();
        }
    }
}
