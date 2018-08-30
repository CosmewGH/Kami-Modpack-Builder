using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiModpackBuilder.FileTypes
{
    class NUS3BANK
    {
        public static ushort GetNUS3Type(string NUS3Name)
        {
            if (NUS3Name.Contains("_se") && NUS3Name.Contains("_common")) return 4;
            else if (NUS3Name.Contains("_vc") && NUS3Name.Contains("_ouen")) return 3;
            else if (NUS3Name.Contains("_vc") && !NUS3Name.Contains("_se") && !NUS3Name.Contains("_ouen")) return 2;
            else if (NUS3Name.Contains("_se") && !NUS3Name.Contains("_vc") && !NUS3Name.Contains("_common")) return 1;
            else return 5;
        }
    }
}
