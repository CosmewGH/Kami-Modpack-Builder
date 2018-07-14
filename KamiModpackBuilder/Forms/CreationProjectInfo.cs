using KamiModpackBuilder.Objects;
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
    internal partial class CreationProjectInfo : Form
    {
        private SmashMod _Config;
        private SmashProjectManager _Project;

        public CreationProjectInfo(SmashMod config, SmashProjectManager project)
        {
            InitializeComponent();

            _Config = config;
            _Project = project;

            ddpGameRegion.Items.Add("JPN (Zone 1)");
            ddpGameRegion.Items.Add("USA (Zone 2)");
            ddpGameRegion.Items.Add("??? (Zone 3)");
            ddpGameRegion.Items.Add("EUR (Zone 4)");  
        }

        private void ddpGameRegion_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
            {
                ddpGameRegion.SelectedIndex = _Config.GameRegionID - 1;
                txtGameVersion.Value = _Config.GameVersion;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Config.GameVersion = (int)txtGameVersion.Value;
            _Config.GameRegionID = ddpGameRegion.SelectedIndex + 1;
            _Project.SaveProject();
            this.Close();
        }
    }
}
