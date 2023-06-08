using System.ComponentModel;
using System.Windows;
using ASNC.ViewModels;

namespace ASNC.Views
{
    /// <summary>
    /// Interaction logic for BulkExportView.xaml
    /// </summary>
    public partial class BulkExportView : Window
    {
        public BulkExportView()
        {
            this.InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = ((BulkExportViewModel)this.DataContext).DialogResult;
            base.OnClosing(e);
        }
    }
}
