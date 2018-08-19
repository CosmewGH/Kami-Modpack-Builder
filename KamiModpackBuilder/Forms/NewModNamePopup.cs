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
    public partial class NewModNamePopup : Form
    {
        public string nameText { get { return textBoxName.Text; } set { textBoxName.Text = value; } }
        public bool confirmPressed = false;

        public NewModNamePopup()
        {
            InitializeComponent();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            confirmPressed = true;
            Close();
        }
    }
}
