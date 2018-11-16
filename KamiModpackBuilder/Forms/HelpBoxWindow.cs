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
    public partial class HelpBoxWindow : Form
    {
        public bool DontShowAgain = false;

        public HelpBoxWindow(string text)
        {
            InitializeComponent();
            labelText.Text = text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDoNotShow_Click(object sender, EventArgs e)
        {
            DontShowAgain = true;
            this.Close();
        }
    }
}
