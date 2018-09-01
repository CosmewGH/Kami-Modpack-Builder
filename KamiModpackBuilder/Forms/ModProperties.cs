using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KamiModpackBuilder.Objects;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.UserControls;
using System.IO;

namespace KamiModpackBuilder.Forms
{
    public partial class ModProperties : Form
    {

        private string ModPath = String.Empty;

        private string CharName = String.Empty;
        private ModsList.ModListType ModListType;

        private CharacterGeneralModXML XMLDataCharGeneral = null;
        private StageModXML XMLDataStage = null;
        private GeneralModXML XMLDataGeneral = null;

        private bool IsInitialized = false;

        private string PathKami = String.Empty;
        string PathStage10 = String.Empty;
        string PathStage11 = String.Empty;
        string PathStage12 = String.Empty;
        string PathStage30 = String.Empty;
        string PathStagen10 = String.Empty;

        public bool isInitialized { get { return IsInitialized; } }

        public ModProperties(string modPath, ModsList.ModListType modListType, string charName = "")
        {
            InitializeComponent();
            ModPath = modPath;
            ModListType = modListType;
            CharName = charName;
            PathKami = ModPath + Path.DirectorySeparatorChar + "kamimod.xml";

            switch (ModListType)
            {
                case ModsList.ModListType.CharacterGeneral:
                    XMLDataCharGeneral = Utils.DeserializeXML<CharacterGeneralModXML>(PathKami);
                    if (XMLDataCharGeneral == null) return;
                    textBoxDisplayName.Text = XMLDataCharGeneral.DisplayName;
                    checkBoxWifiSafe.Checked = XMLDataCharGeneral.WifiSafe;
                    if (XMLDataCharGeneral.Notes == null) XMLDataCharGeneral.Notes = String.Empty;
                    textBoxNotes.Text = XMLDataCharGeneral.Notes.Replace("\n", "\r\n");
                    groupBoxPortaits.Visible = false;
                    groupBoxStageData.Visible = false;
                    this.Height -= groupBoxPortaits.Height + groupBoxStageData.Height;
                    break;
                case ModsList.ModListType.Stage:
                    XMLDataStage = Utils.DeserializeXML<StageModXML>(PathKami);
                    if (XMLDataStage == null) return;
                    textBoxDisplayName.Text = XMLDataStage.DisplayName;
                    checkBoxWifiSafe.Checked = XMLDataStage.WifiSafe;
                    if (XMLDataStage.Notes == null) XMLDataStage.Notes = String.Empty;
                    textBoxNotes.Text = XMLDataStage.Notes.Replace("\n", "\r\n");
                    for (int i = 0; i < DB.StagesDB.Stages.Count; ++i)
                    {
                        if (DB.StagesDB.Stages[i].ID == XMLDataStage.IntendedStage)
                        {
                            labelStageName.Text = DB.StagesDB.Stages[i].LabelHuman;
                        }
                    }
                    PathStage10 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stage_10_XX.nut";
                    PathStage11 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stage_11_XX.nut";
                    PathStage12 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stage_12_XX.nut";
                    PathStage30 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stage_30_XX.nut";
                    PathStagen10 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stagen_10_XX.nut";
                    XMLDataStage.stage_10 = File.Exists(PathStage10);
                    XMLDataStage.stage_11 = File.Exists(PathStage11);
                    XMLDataStage.stage_12 = File.Exists(PathStage12);
                    XMLDataStage.stage_30 = File.Exists(PathStage30);
                    XMLDataStage.stagen_10 = File.Exists(PathStagen10);
                    buttonExport_stage10.Enabled = XMLDataStage.stage_10;
                    buttonExport_stage11.Enabled = XMLDataStage.stage_11;
                    buttonExport_stage12.Enabled = XMLDataStage.stage_12;
                    buttonExport_stage30.Enabled = XMLDataStage.stage_30;
                    buttonExport_stagen10.Enabled = XMLDataStage.stagen_10;
                    break;
                case ModsList.ModListType.General:
                    XMLDataGeneral = Utils.DeserializeXML<GeneralModXML>(PathKami);
                    if (XMLDataGeneral == null) return;
                    textBoxDisplayName.Text = XMLDataGeneral.DisplayName;
                    checkBoxWifiSafe.Checked = XMLDataGeneral.WifiSafe;
                    if (XMLDataGeneral.Notes == null) XMLDataGeneral.Notes = String.Empty;
                    textBoxNotes.Text = XMLDataGeneral.Notes.Replace("\n", "\r\n");
                    groupBoxPortaits.Visible = false;
                    groupBoxStageData.Visible = false;
                    this.Height -= groupBoxPortaits.Height + groupBoxStageData.Height;
                    break;
            }

            IsInitialized = true;
        }

        private void SaveXMLData()
        {
            switch (ModListType)
            {
                case ModsList.ModListType.CharacterGeneral:
                    XMLDataCharGeneral.DisplayName = textBoxDisplayName.Text;
                    XMLDataCharGeneral.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataCharGeneral.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    Utils.SerializeXMLToFile(XMLDataCharGeneral, PathKami);
                    break;
                case ModsList.ModListType.Stage:
                    XMLDataStage.DisplayName = textBoxDisplayName.Text;
                    XMLDataStage.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataStage.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    Utils.SerializeXMLToFile(XMLDataStage, PathKami);
                    break;
                case ModsList.ModListType.General:
                    XMLDataGeneral.DisplayName = textBoxDisplayName.Text;
                    XMLDataGeneral.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataGeneral.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    Utils.SerializeXMLToFile(XMLDataGeneral, PathKami);
                    break;
            }

            LogHelper.Info("Mod properties saved.");
        }

        private void ModProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveXMLData();
        }

        private void buttonImport_stage10_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Equals("png"))
                {
                    MessageBox.Show("Cannot import PNGs yet :(");
                    //TODO: Import PNG
                    return;
                }
                else
                {
                    //TODO: Validate format
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "ui");
                    File.Copy(openFileDialogPortraits.FileName, PathStage10, true);
                    XMLDataStage.stage_10 = true;
                    buttonExport_stage10.Enabled = true;
                    LogHelper.Info("Imported stage_10 successfully.");
                }
            }
        }

        private void buttonExport_stage10_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_stage11_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Equals("png"))
                {
                    MessageBox.Show("Cannot import PNGs yet :(");
                    //TODO: Import PNG
                    return;
                }
                else
                {
                    //TODO: Validate format
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "ui");
                    File.Copy(openFileDialogPortraits.FileName, PathStage11, true);
                    XMLDataStage.stage_11 = true;
                    buttonExport_stage11.Enabled = true;
                    LogHelper.Info("Imported stage_11 successfully.");
                }
            }
        }

        private void buttonExport_stage11_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_stage12_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Equals("png"))
                {
                    MessageBox.Show("Cannot import PNGs yet :(");
                    //TODO: Import PNG
                    return;
                }
                else
                {
                    //TODO: Validate format
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "ui");
                    File.Copy(openFileDialogPortraits.FileName, PathStage12, true);
                    XMLDataStage.stage_12 = true;
                    buttonExport_stage12.Enabled = true;
                    LogHelper.Info("Imported stage_12 successfully.");
                }
            }
        }

        private void buttonExport_stage12_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_stage30_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Equals("png"))
                {
                    MessageBox.Show("Cannot import PNGs yet :(");
                    //TODO: Import PNG
                    return;
                }
                else
                {
                    //TODO: Validate format
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "ui");
                    File.Copy(openFileDialogPortraits.FileName, PathStage30, true);
                    XMLDataStage.stage_30 = true;
                    buttonExport_stage30.Enabled = true;
                    LogHelper.Info("Imported stage_30 successfully.");
                }
            }
        }

        private void buttonExport_stage30_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_stagen10_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Equals("png"))
                {
                    MessageBox.Show("Cannot import PNGs yet :(");
                    //TODO: Import PNG
                    return;
                }
                else
                {
                    //TODO: Validate format
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "ui");
                    File.Copy(openFileDialogPortraits.FileName, PathStagen10, true);
                    XMLDataStage.stagen_10 = true;
                    buttonExport_stagen10.Enabled = true;
                    LogHelper.Info("Imported stagen_10 successfully.");
                }
            }
        }

        private void buttonExport_stagen10_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }
    }
}
