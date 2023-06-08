using System.ComponentModel;
using System.Windows;
using ASNC.ViewModels;

namespace ASNC.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            this.InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = ((SettingsViewModel)this.DataContext).DialogResult;
            base.OnClosing(e);
        }
    }
}
