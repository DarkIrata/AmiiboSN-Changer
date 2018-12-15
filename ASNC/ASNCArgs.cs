using System;
using System.Collections.Generic;
using System.Text;
using PowerArgs;

namespace ASNC
{
    public class ASNCArgs
    {
        [ArgRequired]
        [ArgShortcut("a")]
        [ArgDescription("Path to the target Amiibo.bin file")]
        public string Amiibo { get; set; }

        [ArgDefaultValue(1)]
        [ArgShortcut("c")]
        [ArgDescription("Amount of Amiibos to generate")]
        public uint Amount { get; set; }

        [ArgRequired]
        [ArgShortcut("o")]
        [ArgDescription("Directory where the Amiibos get saved to")]
        public string Output { get; set; }
    }
}
