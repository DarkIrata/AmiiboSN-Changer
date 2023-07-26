using System;
using System.IO;
using System.Threading.Tasks;
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
            this.SetupExceptionHandling();
            this.MainWindow = new ShellView() { DataContext = this.shellViewModel };
            this.MainWindow.Show();

            if (!Config.Exists)
            {
                this.shellViewModel.OpenSettingsCommand.Execute(null);
            }
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                this.LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
            {
                this.LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                this.LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            var timestamp = DateTime.Now.ToString("yyyy-dd-M_HH-mm-ss");
            var filename = $"Error_{timestamp}.txt";
            try
            {
                var message = $"Unhandled exception ({source}){Environment.NewLine}Type: {exception?.GetType().FullName}{Environment.NewLine}Message: {exception?.Message}{Environment.NewLine}StackTrace: {exception?.StackTrace}";
                File.WriteAllText(filename, message);
            }
            catch (Exception ex)
            {
                File.WriteAllText(filename, ex.Message);
            }

            MessageBox.Show($"Unhandled exception occurred!{Environment.NewLine}Error: {exception?.Message}{Environment.NewLine}An error log was generated and named '{filename}'.", "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
