using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiiboSN_Changer.Events
{
    public class AmiiToolResultEventArgs : EventArgs
    {
        public bool IsError { get; internal set; }

        public string Args { get; internal set; }

        public string Result { get; internal set; }
    }
}
