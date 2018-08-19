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
        private DataGridModsList.ModListType ModListType;

        private CharacterGeneralModXML XMLDataCharGeneral = null;
        private StageModXML XMLDataStage = null;
        private GeneralModXML XMLDataGeneral = null;

        private bool IsInitialized = false;

        private string PathKami = String.Empty;

        public bool isInitialized { get { return IsInitialized; } }
        public ModProperties(string modPath, DataGridModsList.ModListType modListType, string charName = "")
        {
            InitializeComponent();
            ModPath = modPath;
            ModListType = modListType;
            CharName = charName;
            PathKami = ModPath + Path.DirectorySeparatorChar + "kamimod.xml";

            switch (ModListType)
            {
                case DataGridModsList.ModListType.CharacterGeneral:
                    XMLDataCharGeneral = Utils.DeserializeXML<CharacterGeneralModXML>(PathKami);
                    if (XMLDataCharGeneral == null) return;
                    textBoxDisplayName.Text = XMLDataCharGeneral.DisplayName;
                    checkBoxWifiSafe.Checked = XMLDataCharGeneral.WifiSafe;
                    if (XMLDataCharGeneral.Notes == null) XMLDataCharGeneral.Notes = String.Empty;
                    textBoxNotes.Text = XMLDataCharGeneral.Notes.Replace("\n", "\r\n");
                    break;
                case DataGridModsList.ModListType.Stage:
                    XMLDataStage = Utils.DeserializeXML<StageModXML>(PathKami);
                    if (XMLDataStage == null) return;
                    textBoxDisplayName.Text = XMLDataStage.DisplayName;
                    checkBoxWifiSafe.Checked = XMLDataStage.WifiSafe;
                    if (XMLDataStage.Notes == null) XMLDataStage.Notes = String.Empty;
                    textBoxNotes.Text = XMLDataStage.Notes.Replace("\n", "\r\n");
                    break;
                case DataGridModsList.ModListType.General:
                    XMLDataGeneral = Utils.DeserializeXML<GeneralModXML>(PathKami);
                    if (XMLDataGeneral == null) return;
                    textBoxDisplayName.Text = XMLDataGeneral.DisplayName;
                    checkBoxWifiSafe.Checked = XMLDataGeneral.WifiSafe;
                    if (XMLDataGeneral.Notes == null) XMLDataGeneral.Notes = String.Empty;
                    textBoxNotes.Text = XMLDataGeneral.Notes.Replace("\n", "\r\n");
                    break;
            }

            IsInitialized = true;
        }

        private void SaveXMLData()
        {
            switch (ModListType)
            {
                case DataGridModsList.ModListType.CharacterGeneral:
                    XMLDataCharGeneral.DisplayName = textBoxDisplayName.Text;
                    XMLDataCharGeneral.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataCharGeneral.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    Utils.SerializeXMLToFile(XMLDataCharGeneral, PathKami);
                    break;
                case DataGridModsList.ModListType.Stage:
                    XMLDataStage.DisplayName = textBoxDisplayName.Text;
                    XMLDataStage.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataStage.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    Utils.SerializeXMLToFile(XMLDataStage, PathKami);
                    break;
                case DataGridModsList.ModListType.General:
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
    }
}
