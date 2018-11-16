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
            textBoxHexEditor.Text = _Project._Config.ProjectHexEditorFile;
        }

        private void Preferences_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Project._Config.Debug = checkBoxDebug.Checked;
            _Project._Config.ProjectHexEditorFile = textBoxHexEditor.Text;
        }

        private void buttonBrowseHexEditor_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Executables|*.exe";
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBoxHexEditor.Text = dialog.FileName;
        }

        private void buttonResetTips_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _Project._Config.HelpTextHide.Length; ++i)
            {
                _Project._Config.HelpTextHide[i] = false;
            }
        }
    }
}
