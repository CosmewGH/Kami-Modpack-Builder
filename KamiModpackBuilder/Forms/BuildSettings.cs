using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KamiModpackBuilder.Forms
{
    public partial class BuildSettings : Form
    {

        private SmashProjectManager _SmashProjectManager;
        public bool doBuild = false;

        public BuildSettings(SmashProjectManager project)
        {
            InitializeComponent();

            _SmashProjectManager = project;

            string[] BuildSafetyStrings = { "Allow All", "No Known Crashing Metal Models", "Only Known Working Metal Models" };
            comboBoxCrashSafety.DataSource = BuildSafetyStrings;

            checkBoxWifiSafe.Checked = _SmashProjectManager.CurrentProject.BuildIsWifiSafe;
            comboBoxCrashSafety.SelectedIndex = _SmashProjectManager.CurrentProject.BuildSafetySetting;
        }

        private void BuildSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            _SmashProjectManager.CurrentProject.BuildIsWifiSafe = checkBoxWifiSafe.Checked;
            _SmashProjectManager.CurrentProject.BuildSafetySetting = comboBoxCrashSafety.SelectedIndex;
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            _SmashProjectManager.CurrentProject.BuildIsWifiSafe = checkBoxWifiSafe.Checked;
            _SmashProjectManager.CurrentProject.BuildSafetySetting = comboBoxCrashSafety.SelectedIndex;

            doBuild = true;

            this.Close();
        }
    }
}
