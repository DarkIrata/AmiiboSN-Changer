using System;
using System.Collections.Generic;
using System.Text;

namespace AmiiboSNChanger.Libs.Events
{
    public class ActionOutputEventArgs : EventArgs
    {
        public bool Successful { get; set; }

        public string Output { get; set; }

        public ActionOutputEventArgs(bool successful, string output)
        {
            this.Successful = successful;
            this.Output = output;
        }
    }
}
