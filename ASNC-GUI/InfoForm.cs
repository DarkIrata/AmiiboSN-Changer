using System.Diagnostics;
using System.Windows.Forms;

namespace ASNC_GUI
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            this.InitializeComponent();
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(((LinkLabel)sender).Tag.ToString());

        private void pbGithub_Click(object sender, System.EventArgs e) => Process.Start("https://github.com/DarkIrata/AmiiboSN-Changer");

        private void btnOk_Click(object sender, System.EventArgs e) => this.Close();
    }
}
