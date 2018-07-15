using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KamiModpackBuilder.Forms
{
    public partial class Preferences : Form
    {

        SmashProjectManager _Project;

        public Preferences(SmashProjectManager project)
        {
            InitializeComponent();

            _Project = project;

            checkBoxDebug.Checked = _Project._Config.Debug;
        }

        private void Preferences_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Project._Config.Debug = checkBoxDebug.Checked;
        }
    }
}
