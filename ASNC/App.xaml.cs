using System;
using System.Windows;
using ASNC.Data;
using ASNC.Services;
using ASNC.ViewModels;
using ASNC.Views;

namespace ASNC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;
        private readonly ShellViewModel shellViewModel;

        public App()
        {
            var config = Config.Load();
            this.serviceProvider = new ServiceProvider(config);

            this.shellViewModel = new ShellViewModel(this.serviceProvider);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.MainWindow = new ShellView() { DataContext = this.shellViewModel };
            this.MainWindow.Show();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!Config.Exists)
            {
                this.shellViewModel.OpenSettingsCommand.Execute(null);
            }
        }
    }
}
