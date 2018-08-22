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
using KamiModpackBuilder.DB;
using System.IO;
using KamiModpackBuilder.Globals;

namespace KamiModpackBuilder.Forms
{
    public partial class ModImportCharacterSlot : Form
    {
        #region Properties
        public Fighter _CurrentFighter;

        public string _BaseFolderPath;

        public string[] ModelNutDirectories;
        public string[] Files_chr_00;
        public string[] Files_chr_11;
        public string[] Files_chr_13;
        public string[] Files_stock_90;
        public string[] Files_chrn_11;
        public string[] Files_Sound_Nus3bank;
        public string[] Files_Voice_Nus3bank;
        #endregion

        #region Member References
        private SmashProjectManager _SmashProjectManager;
        #endregion

        #region Members
        private string[] ComboBoxList_ModelNutDirectories;
        private string[] ComboBoxList_Files_chr_00;
        private string[] ComboBoxList_Files_chr_11;
        private string[] ComboBoxList_Files_chr_13;
        private string[] ComboBoxList_Files_stock_90;
        private string[] ComboBoxList_Files_chrn_11;
        private string[] ComboBoxList_Files_Sound_Nus3bank;
        private string[] ComboBoxList_Files_Voice_Nus3bank;

        private int modelPartsCount = 1;
        private int rowCount = 0;
        #endregion

        #region Constructors
        public ModImportCharacterSlot(SmashProjectManager a_smashProjectManager)
        {
            InitializeComponent();

            _SmashProjectManager = a_smashProjectManager;

        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            modelPartsCount = _CurrentFighter.modelParts.Count;
            rowCount = (int)Math.Ceiling((float)ModelNutDirectories.Length / (float)(modelPartsCount + ((_CurrentFighter.lowPolySlots == Fighter.LowPolySlots.None) ? 0 : 1)));
            if (rowCount > 1) buttonRemoveRow.Enabled = true;

            ComboBoxList_ModelNutDirectories = GetComboBoxStrings(ModelNutDirectories, true);
            ComboBoxList_Files_chr_00 = GetComboBoxStrings(Files_chr_00, false);
            ComboBoxList_Files_chr_11 = GetComboBoxStrings(Files_chr_11, false);
            ComboBoxList_Files_chr_13 = GetComboBoxStrings(Files_chr_13, false);
            ComboBoxList_Files_stock_90 = GetComboBoxStrings(Files_stock_90, false);
            ComboBoxList_Files_chrn_11 = GetComboBoxStrings(Files_chrn_11, false);
            ComboBoxList_Files_Sound_Nus3bank = GetComboBoxStrings(Files_Sound_Nus3bank, false);
            ComboBoxList_Files_Voice_Nus3bank = GetComboBoxStrings(Files_Voice_Nus3bank, false);

            SetupColumnVisibility();

            BindComboBoxColumns();

            //Add rows and fill in starting data
            for (int i = 0; i < rowCount; ++i)
            {
                dataGridView1.Rows.Add("NewMod" + (i + 1));
                int haslxx = _CurrentFighter.lowPolySlots != Fighter.LowPolySlots.None ? 1 : 0;
                for (int k = 0; k < modelPartsCount; ++k)
                {
                    (dataGridView1.Rows[i].Cells[k + 1] as DataGridViewComboBoxCell).Value = ComboBoxList_ModelNutDirectories[(i + 1 + ((k + (k < 1 ? 0 : haslxx)) * rowCount) < ComboBoxList_ModelNutDirectories.Length) ? i + 1 + ((k + (k < 1 ? 0 : haslxx)) * rowCount) : 0];
                }
                if (haslxx > 0) (dataGridView1.Rows[i].Cells[21] as DataGridViewComboBoxCell).Value = ComboBoxList_ModelNutDirectories[(i + 1 + rowCount < ComboBoxList_ModelNutDirectories.Length) ? i + 1 + rowCount : 0];
                (dataGridView1.Rows[i].Cells[22] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chr_00[(i + 1 < ComboBoxList_Files_chr_00.Length) ? i + 1 : 0];
                (dataGridView1.Rows[i].Cells[23] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chr_11[(i + 1 < ComboBoxList_Files_chr_11.Length) ? i + 1 : 0];
                (dataGridView1.Rows[i].Cells[24] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chr_13[(i + 1 < ComboBoxList_Files_chr_13.Length) ? i + 1 : 0];
                (dataGridView1.Rows[i].Cells[25] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_stock_90[(i + 1 < ComboBoxList_Files_stock_90.Length) ? i + 1 : 0];
                (dataGridView1.Rows[i].Cells[26] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chrn_11[(i + 1 < ComboBoxList_Files_chrn_11.Length) ? i + 1 : 0];
                (dataGridView1.Rows[i].Cells[27] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_Sound_Nus3bank[(i + 1 < ComboBoxList_Files_Sound_Nus3bank.Length) ? i + 1 : 0];
                (dataGridView1.Rows[i].Cells[28] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_Voice_Nus3bank[(i + 1 < ComboBoxList_Files_Voice_Nus3bank.Length) ? i + 1 : 0];
            }
        }
        #endregion

        #region Private Methods

        #region Events
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRemoveRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow rowToRemove = null;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                rowToRemove = dataGridView1.SelectedRows[0];
            }
            else if (dataGridView1.SelectedCells.Count > 0)
            {
                rowToRemove = dataGridView1.SelectedCells[0].OwningRow;
            }
            else rowToRemove = dataGridView1.Rows[0];

            DialogResult result = MessageBox.Show(String.Format("Delete row \"{0}\"?", rowToRemove.Cells[0].Value), "Row Deletion Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(rowToRemove);
                --rowCount;
                if (rowCount <= 1) buttonRemoveRow.Enabled = false;
            }
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow newRow = dataGridView1.Rows[dataGridView1.Rows.Add("NewRow" + (rowCount + 1))];

            for (int i = 0; i < modelPartsCount; ++i)
            {
                (newRow.Cells[i + 1] as DataGridViewComboBoxCell).Value = ComboBoxList_ModelNutDirectories[0];
            }
            (newRow.Cells[21] as DataGridViewComboBoxCell).Value = ComboBoxList_ModelNutDirectories[0];
            (newRow.Cells[22] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chr_00[0];
            (newRow.Cells[23] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chr_11[0];
            (newRow.Cells[24] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chr_13[0];
            (newRow.Cells[25] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_stock_90[0];
            (newRow.Cells[26] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_chrn_11[0];
            (newRow.Cells[27] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_Sound_Nus3bank[0];
            (newRow.Cells[28] as DataGridViewComboBoxCell).Value = ComboBoxList_Files_Voice_Nus3bank[0];

            ++rowCount;
            if (!buttonRemoveRow.Enabled) buttonRemoveRow.Enabled = true;
        }

        private void ModImportCharacterSlot_Shown(object sender, EventArgs e)
        {
            this.Activate();
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            dataGridView1.BeginEdit(true);
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            //Validate that the rows are set up correctly
            bool keepDefaultName = false;
            for (int i = 0; i < rowCount; ++i)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                string rowName = Globals.PathHelper.RemoveInvalidFilenameChars(((string)row.Cells[0].Value));
                row.Cells[0].Value = rowName;
                if (rowName.Length <= 0)
                {
                    MessageBox.Show("At least one row is not named! Make sure all the skins have a name before importing.");
                    return;
                }
                if (!keepDefaultName)
                {
                    if (rowName.Contains("NewMod") || rowName.Contains("NewRow"))
                    {
                        DialogResult result = MessageBox.Show("It looks like at least one row has the default name. Are you sure you want to keep that?", "Default name warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                            keepDefaultName = true;
                        else
                            return;
                    }
                }

                bool pass = false;
                for (int j = 1; j < 29; ++j)
                {
                    if (!string.Equals((string)row.Cells[j].Value, "None"))
                    {
                        pass = true;
                        continue;
                    }
                }
                if (!pass)
                {
                    MessageBox.Show("A least one row doesn't have any mod files selected! You can't have a mod with no files!");
                    return;
                }
            }
            //Make sure no rows have the same name
            string[] rowNames = new string[rowCount];
            for (int i = 0; i < rowCount; ++i)
            {
                rowNames[i] = (string)dataGridView1.Rows[i].Cells[0].Value;
            }
            for (int i = 0; i < rowCount; ++i)
            {
                for (int j = i + 1; j < rowCount; ++j)
                {
                    if (string.Equals(rowNames[i], rowNames[j]))
                    {
                        MessageBox.Show("Rows cannot have the same name, this would result in 2 skins with the same folder name. Please adjust the names.");
                        return;
                    }
                }
            }
            //Import the mod
            string baseCharacterPath = PathHelper.FolderCharSlotsMods + (_CurrentFighter.name.ToLower()) + Path.DirectorySeparatorChar;
            for (int i = 0; i < rowCount; ++i)
            {
                string baseModPath = baseCharacterPath + rowNames[i] + Path.DirectorySeparatorChar;
                #region XML File Creation
                CharacterSlotModXML xml = new CharacterSlotModXML();

                string[] modelParts = new string[modelPartsCount];
                for (int k = 0; k < modelPartsCount; ++k)
                {
                    modelParts[k] = (string)dataGridView1.Rows[i].Cells[k + 1].Value;
                }
                string lxx = (string)dataGridView1.Rows[i].Cells[21].Value;
                string chr_00 = (string)dataGridView1.Rows[i].Cells[22].Value;
                string chr_11 = (string)dataGridView1.Rows[i].Cells[23].Value;
                string chr_13 = (string)dataGridView1.Rows[i].Cells[24].Value;
                string stock_90 = (string)dataGridView1.Rows[i].Cells[25].Value;
                string chrn_11 = (string)dataGridView1.Rows[i].Cells[26].Value;
                string sound = (string)dataGridView1.Rows[i].Cells[27].Value;
                string voice = (string)dataGridView1.Rows[i].Cells[28].Value;

                xml.DisplayName = rowNames[i];
                xml.chr_00 = (string.Equals(chr_00, "None") ? false : true);
                xml.chr_11 = (string.Equals(chr_11, "None") ? false : true);
                xml.chr_13 = (string.Equals(chr_13, "None") ? false : true);
                xml.stock_90 = (string.Equals(stock_90, "None") ? false : true);
                xml.chrn_11 = (string.Equals(chrn_11, "None") ? false : true);
                xml.Sound = (string.Equals(sound, "None") ? false : true);
                xml.Voice = (string.Equals(voice, "None") ? false : true);
                xml.Haslxx = (_CurrentFighter.lowPolySlots == Fighter.LowPolySlots.None) ? false : (string.Equals(lxx, "None") ? false : true);
                xml.TextureID = -1;//Is recalculated lower
                xml.MetalModel = CharacterSlotModXML.MetalModelStatus.Unknown;
                xml.WifiSafe = true; //Assuming wifi-safe
                xml.UseCustomName = xml.chrn_11;
                xml.CharacterName = xml.UseCustomName ? rowNames[i] : "";
                Utils.SerializeXMLToFile<CharacterSlotModXML>(xml, baseModPath + "kamimod.xml");
                #endregion

                #region Model Files
                string baseModelPath = baseModPath + "model" + Path.DirectorySeparatorChar;
                for (int k = 0; k < modelPartsCount; ++k)
                {
                    if (modelParts[k].Equals("None")) continue;
                    string foldername = GetFilenameFromComboBoxString(ModelNutDirectories, ComboBoxList_ModelNutDirectories, modelParts[k]);
                    if (xml.TextureID == -1)
                    {
                        FileTypes.NUT nut = new FileTypes.NUT();
                        nut.Read(foldername);
                        if (nut.Textures.Count > 0)
                        {
                            xml.TextureID = nut.Textures[0].HashId;
                        }
                    }
                    foldername = foldername.Replace(Path.GetFileName(foldername), string.Empty);
                    Utils.CopyAllValidFilesBetweenDirectories(foldername, baseModelPath + _CurrentFighter.modelParts[k] + Path.DirectorySeparatorChar);
                }
                if (_CurrentFighter.lowPolySlots != Fighter.LowPolySlots.None)
                {
                    if (!lxx.Equals("None"))
                    {
                        string foldername = GetFilenameFromComboBoxString(ModelNutDirectories, ComboBoxList_ModelNutDirectories, lxx);
                        foldername = foldername.Replace(Path.GetFileName(foldername), string.Empty);
                        Utils.CopyAllValidFilesBetweenDirectories(foldername, baseModelPath + "lxx" + Path.DirectorySeparatorChar);
                    }
                }
                #endregion

                #region Chr Files
                if (xml.chr_00 || xml.chr_11 || xml.chr_13 || xml.stock_90 || xml.chrn_11)
                {
                    string baseChrPath = baseModPath + "chr" + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(baseChrPath);
                    if (xml.chr_00) File.Copy(GetFilenameFromComboBoxString(Files_chr_00, ComboBoxList_Files_chr_00, chr_00), baseChrPath + "chr_00_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.chr_11) File.Copy(GetFilenameFromComboBoxString(Files_chr_11, ComboBoxList_Files_chr_11, chr_11), baseChrPath + "chr_11_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.chr_13) File.Copy(GetFilenameFromComboBoxString(Files_chr_13, ComboBoxList_Files_chr_13, chr_13), baseChrPath + "chr_13_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.stock_90) File.Copy(GetFilenameFromComboBoxString(Files_stock_90, ComboBoxList_Files_stock_90, stock_90), baseChrPath + "stock_90_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.chrn_11) File.Copy(GetFilenameFromComboBoxString(Files_chrn_11, ComboBoxList_Files_chrn_11, chrn_11), baseChrPath + "chrn_11_" + _CurrentFighter.name + "_XX.nut");
                }
                #endregion

                #region Sound Files
                if (xml.Sound || xml.Voice)
                {
                    string baseSoundPath = baseModPath + "sound" + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(baseSoundPath);
                    if (xml.Sound) File.Copy(GetFilenameFromComboBoxString(Files_Sound_Nus3bank, ComboBoxList_Files_Sound_Nus3bank, sound), baseSoundPath + "snd_se_" + _CurrentFighter.name + "_cxx.nus3bank");
                    if (xml.Voice) File.Copy(GetFilenameFromComboBoxString(Files_Voice_Nus3bank, ComboBoxList_Files_Voice_Nus3bank, voice), baseSoundPath + "snd_vc_" + _CurrentFighter.name + "_cxx.nus3bank");
                }
                #endregion

                LogHelper.Info(String.Format("Mod {0} imported successfully!", rowNames[i]));
            }
            this.Close();
        }
        #endregion

        #region Setup
        private string[] GetComboBoxStrings(string[] input, bool isModel)
        {
            string[] output = new string[input.Length + 1];
            output[0] = "None";
            for (int i = 0; i < input.Length; ++i)
            {
                output[i + 1] = input[i].Replace(_BaseFolderPath, string.Empty);
                if (isModel) output[i + 1] = output[i+1].Replace(Path.GetFileName(input[i]), string.Empty);
            }
            return output;
        }

        private void SetupColumnVisibility()
        {
            if (modelPartsCount > 1) { ColumnModel2.HeaderText = _CurrentFighter.modelParts[1]; ColumnModel2.Visible = true; }
            if (modelPartsCount > 2) { ColumnModel3.HeaderText = _CurrentFighter.modelParts[2]; ColumnModel3.Visible = true; }
            if (modelPartsCount > 3) { ColumnModel4.HeaderText = _CurrentFighter.modelParts[3]; ColumnModel4.Visible = true; }
            if (modelPartsCount > 4) { ColumnModel5.HeaderText = _CurrentFighter.modelParts[4]; ColumnModel5.Visible = true; }
            if (modelPartsCount > 5) { ColumnModel6.HeaderText = _CurrentFighter.modelParts[5]; ColumnModel6.Visible = true; }
            if (modelPartsCount > 6) { ColumnModel7.HeaderText = _CurrentFighter.modelParts[6]; ColumnModel7.Visible = true; }
            if (modelPartsCount > 7) { ColumnModel8.HeaderText = _CurrentFighter.modelParts[7]; ColumnModel8.Visible = true; }
            if (modelPartsCount > 8) { ColumnModel9.HeaderText = _CurrentFighter.modelParts[8]; ColumnModel9.Visible = true; }
            if (modelPartsCount > 9) { ColumnModel10.HeaderText = _CurrentFighter.modelParts[9]; ColumnModel10.Visible = true; }
            if (modelPartsCount > 10) { ColumnModel11.HeaderText = _CurrentFighter.modelParts[10]; ColumnModel11.Visible = true; }
            if (modelPartsCount > 11) { ColumnModel12.HeaderText = _CurrentFighter.modelParts[11]; ColumnModel12.Visible = true; }
            if (modelPartsCount > 12) { ColumnModel13.HeaderText = _CurrentFighter.modelParts[12]; ColumnModel13.Visible = true; }
            if (modelPartsCount > 13) { ColumnModel14.HeaderText = _CurrentFighter.modelParts[13]; ColumnModel14.Visible = true; }
            if (modelPartsCount > 14) { ColumnModel15.HeaderText = _CurrentFighter.modelParts[14]; ColumnModel15.Visible = true; }
            if (modelPartsCount > 15) { ColumnModel16.HeaderText = _CurrentFighter.modelParts[15]; ColumnModel16.Visible = true; }
            if (modelPartsCount > 16) { ColumnModel17.HeaderText = _CurrentFighter.modelParts[16]; ColumnModel17.Visible = true; }
            if (modelPartsCount > 17) { ColumnModel18.HeaderText = _CurrentFighter.modelParts[17]; ColumnModel18.Visible = true; }
            if (modelPartsCount > 18) { ColumnModel19.HeaderText = _CurrentFighter.modelParts[18]; ColumnModel19.Visible = true; }
            if (modelPartsCount > 19) { ColumnModel20.HeaderText = _CurrentFighter.modelParts[19]; ColumnModel20.Visible = true; }
            if (_CurrentFighter.lowPolySlots != Fighter.LowPolySlots.None) { Columnlxx.Visible = true; }
        }

        private void BindComboBoxColumns()
        {
            (dataGridView1.Columns[1] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[2] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[3] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[4] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[5] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[6] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[7] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[8] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[9] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[10] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[11] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[12] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[13] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[14] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[15] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[16] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[17] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[18] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[19] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[20] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[21] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_ModelNutDirectories;
            (dataGridView1.Columns[22] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_chr_00;
            (dataGridView1.Columns[23] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_chr_11;
            (dataGridView1.Columns[24] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_chr_13;
            (dataGridView1.Columns[25] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_stock_90;
            (dataGridView1.Columns[26] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_chrn_11;
            (dataGridView1.Columns[27] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_Sound_Nus3bank;
            (dataGridView1.Columns[28] as DataGridViewComboBoxColumn).DataSource = ComboBoxList_Files_Voice_Nus3bank;
        }
        #endregion

        #region Processing
        private string GetFilenameFromComboBoxString(string[] filenameList, string[] comboboxList, string value)
        {
            int index = -1;
            for (int i = 0; i < comboboxList.Length; ++i)
            {
                if (comboboxList[i].Equals(value))
                {
                    index = i;
                    break;
                }
            }
            if (index < 1) return "";
            else
            {
                return filenameList[index - 1];
            }
        }
        #endregion

        #endregion
    }
}
