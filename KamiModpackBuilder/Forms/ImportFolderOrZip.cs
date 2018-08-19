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
    public partial class ImportFolderOrZip : Form
    {

        public string textInstuctions { set { labelInstuctions.Text = value; } }
        public bool choseFolder = false;
        public bool choseZip = false;

        public ImportFolderOrZip()
        {
            InitializeComponent();
        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {
            choseFolder = true;
            Close();
        }

        private void buttonZip_Click(object sender, EventArgs e)
        {
            choseZip = true;
            Close();
        }
    }
}
