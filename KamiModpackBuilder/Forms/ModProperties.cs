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
using System.Diagnostics;

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
        string PathStage13 = String.Empty;
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
                    XMLDataCharGeneral.isDirty = false;
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
                    XMLDataStage.isDirty = false;
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
                    PathStage13 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stage_13_XX.nut";
                    PathStage30 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stage_30_XX.nut";
                    PathStagen10 = ModPath + Path.DirectorySeparatorChar + "ui" + Path.DirectorySeparatorChar + "stagen_10_XX.nut";
                    XMLDataStage.stage_10 = File.Exists(PathStage10);
                    XMLDataStage.stage_11 = File.Exists(PathStage11);
                    XMLDataStage.stage_12 = File.Exists(PathStage12);
                    XMLDataStage.stage_13 = File.Exists(PathStage13);
                    XMLDataStage.stage_30 = File.Exists(PathStage30);
                    XMLDataStage.stagen_10 = File.Exists(PathStagen10);
                    buttonExport_stage10.Enabled = XMLDataStage.stage_10;
                    buttonExport_stage11.Enabled = XMLDataStage.stage_11;
                    buttonExport_stage12.Enabled = XMLDataStage.stage_12;
                    buttonExport_stage13.Enabled = XMLDataStage.stage_13;
                    buttonExport_stage30.Enabled = XMLDataStage.stage_30;
                    buttonExport_stagen10.Enabled = XMLDataStage.stagen_10;

                    if (XMLDataStage.stage_10)
                    {
                        pictureBox_stage10.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage10);
                        UpdatePictureBoxClickable(pictureBox_stage10);
                    }
                    if (XMLDataStage.stage_11)
                    {
                        pictureBox_stage11.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage11);
                        UpdatePictureBoxClickable(pictureBox_stage11);
                    }
                    if (XMLDataStage.stage_12)
                    {
                        pictureBox_stage12.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage12);
                        UpdatePictureBoxClickable(pictureBox_stage12);
                    }
                    if (XMLDataStage.stage_13)
                    {
                        pictureBox_stage13.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage13);
                        UpdatePictureBoxClickable(pictureBox_stage13);
                    }
                    if (XMLDataStage.stage_30)
                    {
                        pictureBox_stage30.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage30);
                        UpdatePictureBoxClickable(pictureBox_stage30);
                    }
                    if (XMLDataStage.stagen_10)
                    {
                        pictureBox_stagen10.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStagen10, true);
                        UpdatePictureBoxClickable(pictureBox_stagen10);
                    }

                    break;
                case ModsList.ModListType.General:
                    XMLDataGeneral = Utils.DeserializeXML<GeneralModXML>(PathKami);
                    if (XMLDataGeneral == null) return;
                    XMLDataGeneral.isDirty = false;
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

        private void UpdatePictureBoxClickable(PictureBox box)
        {
            if (box.BackgroundImage != null)
            {
                if (box.Cursor == Cursors.Hand) return;
                box.Cursor = Cursors.Hand;
                box.Click += portrait_Click;
            }
            else
            {
                if (box.Cursor != Cursors.Hand) return;
                box.Cursor = Cursors.Arrow;
                box.Click -= portrait_Click;
            }
        }

        private void SaveXMLData()
        {
            switch (ModListType)
            {
                case ModsList.ModListType.CharacterGeneral:
                    XMLDataCharGeneral.DisplayName = textBoxDisplayName.Text;
                    XMLDataCharGeneral.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataCharGeneral.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    if (XMLDataCharGeneral.isDirty)
                    {
                        Utils.SerializeXMLToFile(XMLDataCharGeneral, PathKami);
                        LogHelper.Info("Mod properties saved.");
                        XMLDataCharGeneral.isDirty = false;
                    }
                    break;
                case ModsList.ModListType.Stage:
                    XMLDataStage.DisplayName = textBoxDisplayName.Text;
                    XMLDataStage.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataStage.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    if (XMLDataStage.isDirty)
                    {
                        Utils.SerializeXMLToFile(XMLDataStage, PathKami);
                        LogHelper.Info("Mod properties saved.");
                        XMLDataStage.isDirty = false;
                    }
                    break;
                case ModsList.ModListType.General:
                    XMLDataGeneral.DisplayName = textBoxDisplayName.Text;
                    XMLDataGeneral.WifiSafe = checkBoxWifiSafe.Checked;
                    XMLDataGeneral.Notes = textBoxNotes.Text.Replace("\r\n", "\n");
                    if (XMLDataGeneral.isDirty)
                    {
                        Utils.SerializeXMLToFile(XMLDataGeneral, PathKami);
                        LogHelper.Info("Mod properties saved.");
                        XMLDataGeneral.isDirty = false;
                    }
                    break;
            }

        }

        private void ModProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveXMLData();
        }

        private void buttonImport_stage10_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Contains("png"))
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
                pictureBox_stage10.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage10);
                UpdatePictureBoxClickable(pictureBox_stage10);
            }
        }

        private void buttonExport_stage10_Click(object sender, EventArgs e)
        {
            saveFileDialogPortraits.FileName = "stage_10.png";
            DialogResult result = saveFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            pictureBox_stage10.BackgroundImage.Save(saveFileDialogPortraits.FileName);
        }

        private void buttonImport_stage11_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Contains("png"))
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
                pictureBox_stage11.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage11);
                UpdatePictureBoxClickable(pictureBox_stage11);
            }
        }

        private void buttonExport_stage11_Click(object sender, EventArgs e)
        {
            saveFileDialogPortraits.FileName = "stage_11.png";
            DialogResult result = saveFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            pictureBox_stage11.BackgroundImage.Save(saveFileDialogPortraits.FileName);
        }

        private void buttonImport_stage12_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Contains("png"))
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
                pictureBox_stage12.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage12);
                UpdatePictureBoxClickable(pictureBox_stage12);
            }
        }

        private void buttonExport_stage12_Click(object sender, EventArgs e)
        {
            saveFileDialogPortraits.FileName = "stage_12.png";
            DialogResult result = saveFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            pictureBox_stage12.BackgroundImage.Save(saveFileDialogPortraits.FileName);
        }

        private void buttonImport_stage13_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Contains("png"))
            {
                MessageBox.Show("Cannot import PNGs yet :(");
                //TODO: Import PNG
                return;
            }
            else
            {
                //TODO: Validate format
                Directory.CreateDirectory(ModPath + Path.DirectorySeparatorChar + "ui");
                File.Copy(openFileDialogPortraits.FileName, PathStage13, true);
                XMLDataStage.stage_13 = true;
                buttonExport_stage13.Enabled = true;
                LogHelper.Info("Imported stage_13 successfully.");
                pictureBox_stage13.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage13);
                UpdatePictureBoxClickable(pictureBox_stage13);
            }
        }

        private void buttonExport_stage13_Click(object sender, EventArgs e)
        {
            saveFileDialogPortraits.FileName = "stage_13.png";
            DialogResult result = saveFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            pictureBox_stage13.BackgroundImage.Save(saveFileDialogPortraits.FileName);
        }

        private void buttonImport_stage30_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Contains("png"))
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
                pictureBox_stage30.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStage30);
                UpdatePictureBoxClickable(pictureBox_stage30);
            }
        }

        private void buttonExport_stage30_Click(object sender, EventArgs e)
        {
            saveFileDialogPortraits.FileName = "stage_30.png";
            DialogResult result = saveFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            pictureBox_stage30.BackgroundImage.Save(saveFileDialogPortraits.FileName);
        }

        private void buttonImport_stagen10_Click(object sender, EventArgs e)
        {
            if (ModListType != ModsList.ModListType.Stage) return;
            DialogResult result = openFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            if (Path.GetExtension(openFileDialogPortraits.FileName).ToLower().Contains("png"))
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
                pictureBox_stagen10.BackgroundImage = FileTypes.NUT.BitmapFromPortraitNut(PathStagen10, true);
                UpdatePictureBoxClickable(pictureBox_stagen10);
            }
        }

        private void buttonExport_stagen10_Click(object sender, EventArgs e)
        {
            saveFileDialogPortraits.FileName = "stagen_10.png";
            DialogResult result = saveFileDialogPortraits.ShowDialog();
            if (result != DialogResult.OK) return;
            pictureBox_stagen10.BackgroundImage.Save(saveFileDialogPortraits.FileName);
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(ModPath);
        }

        private void portrait_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            ImagePreview w = new ImagePreview();
            w.image = p.BackgroundImage;
            w.ShowDialog();
        }
    }
}
