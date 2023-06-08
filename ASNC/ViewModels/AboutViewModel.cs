using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using IPUP.MVVM.Commands;
using IPUP.MVVM.ViewModels;

namespace ASNC.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public string Title => "Amiibo SN Changer - About";

        public string InnerTitle => "Amiibo SN Changer " + Assembly.GetExecutingAssembly().GetName().Version;

        public DelegateCommand<string> OpenUrlCommand { get; }

        public ICommand CloseCommand { get; set; }

        public AboutViewModel(System.Action close)
        {
            this.OpenUrlCommand = new DelegateCommand<string>((url) => Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            }));

            this.CloseCommand = new DelegateCommand(() => close());
        }
    }
}
