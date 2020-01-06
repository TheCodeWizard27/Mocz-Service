using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocz_Parser
{
    public class DictionaryEntry
    {

        public string Conversion { get; set; }
        public string InputText { get; set; }
        public int LeftContextId { get; set; }
        public int RightContextId { get; set; }
        public int Cost { get; set; }

    }
}
