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
using System.IO;

namespace KamiModpackBuilder.Forms
{
    public partial class SlotModProperties : Form
    {

        private string ModPath = String.Empty;
        private string CharName = String.Empty;
        private CharacterSlotModXML XMLData = null;
        private bool IsInitialized = false;

        private string PathKami = String.Empty;
        string PathVoice = String.Empty;
        string PathSound = String.Empty;
        string PathChr00 = String.Empty;
        string PathChr11 = String.Empty;
        string PathChr13 = String.Empty;
        string PathStock90 = String.Empty;
        string PathChrn11 = String.Empty;

        public bool isInitialized { get { return IsInitialized; } }

        public SlotModProperties(string modPath, string charName)
        {
            InitializeComponent();
            ModPath = modPath;
            CharName = charName;
            PathKami = ModPath + Path.DirectorySeparatorChar + "kamimod.xml";
            PathVoice = ModPath + Path.DirectorySeparatorChar + "sound" + Path.DirectorySeparatorChar + "snd_vc_" + CharName + "_cxx.nus3bank";
            PathSound = ModPath + Path.DirectorySeparatorChar + "sound" + Path.DirectorySeparatorChar + "snd_se_" + CharName + "_cxx.nus3bank";
            PathChr00 = ModPath + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar + "chr_00_" + CharName + "_XX.nut";
            PathChr11 = ModPath + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar + "chr_11_" + CharName + "_XX.nut";
            PathChr13 = ModPath + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar + "chr_13_" + CharName + "_XX.nut";
            PathStock90 = ModPath + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar + "stock_90_" + CharName + "_XX.nut";
            PathChrn11 = ModPath + Path.DirectorySeparatorChar + "chr" + Path.DirectorySeparatorChar + "chrn_11_" + CharName + "_XX.nut";

            XMLData = Utils.DeserializeXML<CharacterSlotModXML>(PathKami);
            if (XMLData == null) return;

            if (XMLData.BoxingRingText == null) XMLData.BoxingRingText = String.Empty;
            if (XMLData.CharacterName == null) XMLData.CharacterName = String.Empty;
            if (XMLData.DisplayName == null) XMLData.DisplayName = String.Empty;
            if (XMLData.Notes == null) XMLData.Notes = String.Empty;

            XMLData.chr_00 = File.Exists(PathChr00);
            XMLData.chr_11 = File.Exists(PathChr11);
            XMLData.chr_13 = File.Exists(PathChr13);
            XMLData.stock_90 = File.Exists(PathStock90);
            XMLData.chrn_11 = File.Exists(PathChrn11);
            XMLData.Voice = File.Exists(PathVoice);
            XMLData.Sound = File.Exists(PathSound);

            textBoxDisplayName.Text = XMLData.DisplayName;
            checkBoxUseCustomName.Checked = XMLData.UseCustomName;
            textBoxCustomName.Text = XMLData.CharacterName;
            textBoxBoxingRing.Text = XMLData.BoxingRingText.Replace("\n", "\r\n");
            textBoxCustomName.Enabled = checkBoxUseCustomName.Checked;
            textBoxBoxingRing.Enabled = checkBoxUseCustomName.Checked;

            bool hasModels = false;
            string[] nutFiles = Directory.GetFiles(ModPath + Path.DirectorySeparatorChar + "model" + Path.DirectorySeparatorChar + "body", "*.nut", SearchOption.AllDirectories);
            if (nutFiles.Length > 0) hasModels = true;
            else
            {
                nutFiles = Directory.GetFiles(ModPath + Path.DirectorySeparatorChar + "model", "*.nut", SearchOption.AllDirectories);
                if (nutFiles.Length > 0) hasModels = true;
            }
            if (hasModels)
            {
                FileTypes.NUT nut = new FileTypes.NUT();
                nut.Read(nutFiles[0]);
                if (nut.Textures.Count > 0)
                {
                    XMLData.TextureID = (nut.Textures[0].HashId & 0x0000FF00) >> 8;
                }
            }
            textBoxTextureID.Text = XMLData.TextureID.ToString();

            checkBoxWifiSafe.Checked = XMLData.WifiSafe;
            comboBoxMetalModel.DataSource = new List<String> { "Unknown", "Works", "Missing", "Crashes" };
            comboBoxMetalModel.SelectedIndex = (int)XMLData.MetalModel;

            labelVoicePack.Text = XMLData.Voice ? "Yes" : "No";
            labelSoundPack.Text = XMLData.Sound ? "Yes" : "No";

            buttonExport_chr00.Enabled = XMLData.chr_00;
            buttonExport_chr11.Enabled = XMLData.chr_11;
            buttonExport_chr13.Enabled = XMLData.chr_13;
            buttonExport_stock90.Enabled = XMLData.stock_90;
            buttonExport_chrn11.Enabled = XMLData.chrn_11;
            buttonVoiceRemove.Enabled = XMLData.Voice;
            buttonSoundRemove.Enabled = XMLData.Sound;

            //TODO: Portraits

            textBoxNotes.Text = XMLData.Notes.Replace("\n", "\r\n");

            IsInitialized = true;
        }

        private void ChangeTextureID()
        {
            try
            {
                XMLData.TextureID = int.Parse(textBoxTextureID.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("TextureID is invalid! Must be an integer between 0 and 255.\n{0}",e.Message));
                LogHelper.Info("TextureID is invalid! Must be an integer between 0 and 255.");
                textBoxTextureID.Text = XMLData.TextureID.ToString();
                return;
            }
            if (XMLData.TextureID < 0) XMLData.TextureID = 0;
            if (XMLData.TextureID > 255) XMLData.TextureID = 255;
            textBoxTextureID.Text = XMLData.TextureID.ToString();
            TextureIDFix textureIDFix = new TextureIDFix();
            TextureIDFix.Mod mod = new TextureIDFix.Mod(ModPath + Path.DirectorySeparatorChar + "model");
            textureIDFix.ChangeTextureID(mod, (ushort)XMLData.TextureID);
        }

        private void SaveXMLData()
        {
            XMLData.DisplayName = textBoxDisplayName.Text;
            XMLData.UseCustomName = checkBoxUseCustomName.Checked;
            XMLData.CharacterName = textBoxCustomName.Text;
            XMLData.BoxingRingText = textBoxBoxingRing.Text.Replace("\r\n", "\n");
            if (XMLData.BoxingRingText.Contains('\n'))
            {
                //Only 2 lines are allowed
                string[] parts = XMLData.BoxingRingText.Split('\n');
                XMLData.BoxingRingText = parts[0] + "\n" + parts[1];
            }

            XMLData.WifiSafe = checkBoxWifiSafe.Checked;
            XMLData.MetalModel = (CharacterSlotModXML.MetalModelStatus)comboBoxMetalModel.SelectedIndex;

            //TODO: Portraits

            XMLData.Notes = textBoxNotes.Text.Replace("\r\n", "\n");

            Utils.SerializeXMLToFile<CharacterSlotModXML>(XMLData, PathKami);
            LogHelper.Info("Mod properties saved.");
        }

        private void SlotModProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveXMLData();
        }

        private void checkBoxUseCustomName_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomName.Enabled = checkBoxUseCustomName.Checked;
            textBoxBoxingRing.Enabled = checkBoxUseCustomName.Checked;
        }

        private void buttonTextureIDChange_Click(object sender, EventArgs e)
        {
            ChangeTextureID();
        }

        private void buttonVoiceImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogNus3Bank.ShowDialog();
            if (result == DialogResult.OK)
            {
                Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "sound");
                File.Copy(openFileDialogNus3Bank.FileName, PathVoice, true);
                XMLData.Voice = true;
                labelVoicePack.Text = "Yes";
                LogHelper.Info("Imported voicepack successfully.");
            }
        }

        private void buttonVoiceRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the voicepack?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(PathVoice);
                XMLData.Voice = false;
                labelVoicePack.Text = "No";
                buttonVoiceRemove.Enabled = false;
                LogHelper.Info("Voicepack deleted successfully.");
            }
        }

        private void buttonSoundImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogNus3Bank.ShowDialog();
            if (result == DialogResult.OK)
            {
                Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "sound");
                File.Copy(openFileDialogNus3Bank.FileName, PathSound, true);
                XMLData.Sound = true;
                labelSoundPack.Text = "Yes";
                LogHelper.Info("Imported soundpack successfully.");
            }
        }

        private void buttonSoundRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the voicepack?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(PathSound);
                XMLData.Sound = false;
                labelSoundPack.Text = "No";
                buttonSoundRemove.Enabled = false;
                LogHelper.Info("Voicepack deleted successfully.");
            }
        }

        private void buttonImport_chr00_Click(object sender, EventArgs e)
        {
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
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "chr");
                    File.Copy(openFileDialogPortraits.FileName, PathChr00, true);
                    XMLData.chr_00 = true;
                    buttonExport_chr00.Enabled = true;
                    LogHelper.Info("Imported chr_00 successfully.");
                }
            }
        }

        private void buttonExport_chr00_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_chr11_Click(object sender, EventArgs e)
        {
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
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "chr");
                    File.Copy(openFileDialogPortraits.FileName, PathChr11, true);
                    XMLData.chr_11 = true;
                    buttonExport_chr11.Enabled = true;
                    LogHelper.Info("Imported chr_11 successfully.");
                }
            }
        }

        private void buttonExport_chr11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_chr13_Click(object sender, EventArgs e)
        {
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
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "chr");
                    File.Copy(openFileDialogPortraits.FileName, PathChr13, true);
                    XMLData.chr_13 = true;
                    buttonExport_chr13.Enabled = true;
                    LogHelper.Info("Imported chr_13 successfully.");
                }
            }
        }

        private void buttonExport_chr13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_stock90_Click(object sender, EventArgs e)
        {
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
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "chr");
                    File.Copy(openFileDialogPortraits.FileName, PathStock90, true);
                    XMLData.stock_90 = true;
                    buttonExport_stock90.Enabled = true;
                    LogHelper.Info("Imported stock_90 successfully.");
                }
            }
        }

        private void buttonExport_stock90_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }

        private void buttonImport_chrn11_Click(object sender, EventArgs e)
        {
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
                    Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "chr");
                    File.Copy(openFileDialogPortraits.FileName, PathChrn11, true);
                    XMLData.chrn_11 = true;
                    buttonExport_chrn11.Enabled = true;
                    LogHelper.Info("Imported chrn_11 successfully.");
                }
            }
        }

        private void buttonExport_chrn11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot export to PNG yet :(");
            //TODO: Export to PNG
        }
    }
}
