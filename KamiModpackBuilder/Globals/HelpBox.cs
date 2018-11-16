using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KamiModpackBuilder.Forms;

namespace KamiModpackBuilder.Globals
{
    public static class HelpBox
    {
        private static SmashProjectManager _ProjectManager;

        public static void Initialize(SmashProjectManager a_project)
        {
            _ProjectManager = a_project;
        }

        public static void Show(int index)
        {
            Objects.Config config = _ProjectManager._Config;
            if (index < 0 || index > config.HelpTextHide.Length - 1)
            {
                LogHelper.Error(UIStrings.HELPBOX_ERROR);
                return;
            }

            if (config.HelpTextHide[index]) return;
            if (config.HelpTextShown[index]) return;

            HelpBoxWindow box = new HelpBoxWindow(UIStrings.HELPBOX_TEXT[index]);
            box.ShowDialog();
            if (box.DontShowAgain) config.HelpTextHide[index] = true;
            config.HelpTextShown[index] = true;
        }
    }
}
