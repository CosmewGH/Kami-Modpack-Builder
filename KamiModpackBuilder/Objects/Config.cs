using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiModpackBuilder.Objects
{
    public class Config
    {
        public string LastProject { get; set; }
        public int ApplicationVersion { get; set; }
        public bool Debug { get; set; }

        #region Constructors
        public Config()
        {
            ApplicationVersion = 1;
            Debug = false;
        }
        #endregion

    }
}
