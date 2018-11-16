using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KamiModpackBuilder.Objects
{
    public class Config
    {
        public string LastProject { get; set; }
        public int ApplicationVersion { get; set; }
        public bool Debug { get; set; }
        public string ProjectHexEditorFile { get; set; }

        public bool[] HelpTextHide;

        [XmlIgnore]
        public bool[] HelpTextShown;

        #region Constructors
        public Config()
        {
            ApplicationVersion = 1;
            Debug = false;

            HelpTextHide = new bool[Globals.UIStrings.HELPBOX_TEXT.Length];
            HelpTextShown = new bool[Globals.UIStrings.HELPBOX_TEXT.Length];
        }
        #endregion

    }
}
