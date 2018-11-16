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
        protected string[] ComboBoxList_ModelNutDirectories;
        protected string[] ComboBoxList_Files_chr_00;
        protected string[] ComboBoxList_Files_chr_11;
        protected string[] ComboBoxList_Files_chr_13;
        protected string[] ComboBoxList_Files_stock_90;
        protected string[] ComboBoxList_Files_chrn_11;
        protected string[] ComboBoxList_Files_Sound_Nus3bank;
        protected string[] ComboBoxList_Files_Voice_Nus3bank;

        private int slotCount = 0;
        private bool haslxx;

        private List<SlotColumn> slotColumns = new List<SlotColumn>();

        private bool importSuccessful = false;
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
            flowLayoutPanelNames.HorizontalScroll.Enabled = true;
            flowLayoutPanelNames.HorizontalScroll.Maximum = 10000;
            flowLayoutPanelNames.HorizontalScroll.Visible = false;

            haslxx = _CurrentFighter.lowPolySlots != Fighter.LowPolySlots.None;
            int haslxxNum = haslxx ? 1 : 0;
            int modelPartsCount = _CurrentFighter.modelParts.Count;
            slotCount = (int)Math.Ceiling((float)ModelNutDirectories.Length / (float)(modelPartsCount + haslxxNum));

            ComboBoxList_ModelNutDirectories = GetComboBoxStrings(ModelNutDirectories, true);
            ComboBoxList_Files_chr_00 = GetComboBoxStrings(Files_chr_00, false);
            ComboBoxList_Files_chr_11 = GetComboBoxStrings(Files_chr_11, false);
            ComboBoxList_Files_chr_13 = GetComboBoxStrings(Files_chr_13, false);
            ComboBoxList_Files_stock_90 = GetComboBoxStrings(Files_stock_90, false);
            ComboBoxList_Files_chrn_11 = GetComboBoxStrings(Files_chrn_11, false);
            ComboBoxList_Files_Sound_Nus3bank = GetComboBoxStrings(Files_Sound_Nus3bank, false);
            ComboBoxList_Files_Voice_Nus3bank = GetComboBoxStrings(Files_Voice_Nus3bank, false);

            //Add rows and fill in starting data
            for (int i = 0; i < slotCount; ++i)
            {
                SlotColumn newSlot = new SlotColumn(this, "New Mod", haslxx, _CurrentFighter.modelParts.ToArray());
                slotColumns.Add(newSlot);
                newSlot.comboBox_body.SelectedIndex = 1 + i < ComboBoxList_ModelNutDirectories.Length ? 1 + i : 0;
                for (int k = 1; k < modelPartsCount; ++k)
                {
                    newSlot.comboBox_parts[k-1].SelectedIndex = (i + 1 + ((k + (k < 1 ? 0 : haslxxNum)) * slotCount) < ComboBoxList_ModelNutDirectories.Length) ? i + 1 + ((k + (k < 1 ? 0 : haslxxNum)) * slotCount) : 0;
                }
                if (haslxx) newSlot.comboBox_body_lxx.SelectedIndex = (i + 1 + slotCount < ComboBoxList_ModelNutDirectories.Length) ? i + 1 + slotCount : 0;

                newSlot.comboBox_chr_00.SelectedIndex = (i + 1 < ComboBoxList_Files_chr_00.Length) ? i + 1 : 0;
                newSlot.comboBox_chr_11.SelectedIndex = (i + 1 < ComboBoxList_Files_chr_11.Length) ? i + 1 : 0;
                newSlot.comboBox_chr_13.SelectedIndex = (i + 1 < ComboBoxList_Files_chr_13.Length) ? i + 1 : 0;
                newSlot.comboBox_stock_90.SelectedIndex = (i + 1 < ComboBoxList_Files_stock_90.Length) ? i + 1 : 0;
                newSlot.comboBox_chrn_11.SelectedIndex = (i + 1 < ComboBoxList_Files_chrn_11.Length) ? i + 1 : 0;
                newSlot.comboBox_sound.SelectedIndex = (i + 1 < ComboBoxList_Files_Sound_Nus3bank.Length) ? i + 1 : 0;
                newSlot.comboBox_voice.SelectedIndex = (i + 1 < ComboBoxList_Files_Voice_Nus3bank.Length) ? i + 1 : 0;
            }
            HelpBox.Show(6);
        }
        #endregion

        #region Private Methods

        #region Events
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void buttonRemoveRow_Click(object sender, EventArgs e)
        {
            int index = -1;
            for (int i = 0; i < slotCount; ++i)
            {
                if (slotColumns[i].buttonRemove == sender)
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                slotColumns[index].textBox_name.Parent = null;
                slotColumns[index].panel.Parent = null;
                slotColumns.RemoveAt(index);
                --slotCount;
            }
        }

        private void flowLayoutPanelContents_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                flowLayoutPanelNames.HorizontalScroll.Value = e.NewValue <= flowLayoutPanelNames.HorizontalScroll.Maximum ? e.NewValue : flowLayoutPanelNames.HorizontalScroll.Maximum;
            }
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            SlotColumn newSlot = new SlotColumn(this, "New Mod", haslxx, _CurrentFighter.modelParts.ToArray());
            slotColumns.Add(newSlot);
            ++slotCount;
        }

        private void ModImportCharacterSlot_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            //Validate that the rows are set up correctly
            bool keepDefaultName = false;
            if (slotCount < 1)
            {
                MessageBox.Show("There are no slots to import!");
                return;
            }
            for (int i = 0; i < slotCount; ++i)
            {
                string rowName = slotColumns[i].textBox_name.Text;
                if (rowName.Length <= 0)
                {
                    MessageBox.Show("At least one row is not named! Make sure all the skins have a name before importing.");
                    return;
                }
                if (!keepDefaultName)
                {
                    if (rowName.Contains("New Mod"))
                    {
                        DialogResult result = MessageBox.Show("It looks like at least one row has the default name. Are you sure you want to keep that?", "Default name warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                            keepDefaultName = true;
                        else
                            return;
                    }
                }
                CharacterSlotModXML xmlTest = Utils.OpenCharacterSlotKamiModFile(_CurrentFighter.name, rowName);
                if (xmlTest != null)
                {
                    MessageBox.Show("At least one row shares a name with an already existing mod!");
                    return;
                }
                for (int j = 0; j < i; ++j)
                {
                    if (slotColumns[j].textBox_name.Text.Equals(rowName))
                    {
                        MessageBox.Show("At least 2 mods you are importing share the same name. Make sure they are all unique!");
                        return;
                    }
                }

                bool pass = false;

                if (slotColumns[i].comboBox_body.SelectedIndex != 0) pass = true;
                if (haslxx) if (slotColumns[i].comboBox_body_lxx.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_chr_00.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_chr_11.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_chr_13.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_stock_90.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_chrn_11.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_sound.SelectedIndex != 0) pass = true;
                if (slotColumns[i].comboBox_voice.SelectedIndex != 0) pass = true;

                if (slotColumns[i].comboBox_parts != null) for (int j = 0; j < slotColumns[i].comboBox_parts.Count(); ++j)
                {
                    if (slotColumns[i].comboBox_parts[j].SelectedIndex != 0)
                    {
                        pass = true;
                        break;
                    }
                }
                if (!pass)
                {
                    MessageBox.Show("A least one row doesn't have any mod files selected! You can't have a mod with no files!");
                    return;
                }
            }
            //Import the mod
            string baseCharacterPath = PathHelper.FolderCharSlotsMods + (_CurrentFighter.name.ToLower()) + Path.DirectorySeparatorChar;
            for (int i = 0; i < slotCount; ++i)
            {
                string baseModPath = baseCharacterPath + slotColumns[i].textBox_name.Text + Path.DirectorySeparatorChar;
                #region XML File Creation
                CharacterSlotModXML xml = new CharacterSlotModXML();

                xml.DisplayName = slotColumns[i].textBox_name.Text;
                xml.chr_00 = (slotColumns[i].comboBox_chr_00.SelectedIndex < 1) ? false : true;
                xml.chr_11 = (slotColumns[i].comboBox_chr_11.SelectedIndex < 1) ? false : true;
                xml.chr_13 = (slotColumns[i].comboBox_chr_13.SelectedIndex < 1) ? false : true;
                xml.stock_90 = (slotColumns[i].comboBox_stock_90.SelectedIndex < 1) ? false : true;
                xml.chrn_11 = (slotColumns[i].comboBox_chrn_11.SelectedIndex < 1) ? false : true;
                xml.Sound = (slotColumns[i].comboBox_sound.SelectedIndex < 1) ? false : true;
                xml.Voice = (slotColumns[i].comboBox_voice.SelectedIndex < 1) ? false : true;
                xml.Haslxx = (_CurrentFighter.lowPolySlots == Fighter.LowPolySlots.None) ? false : (slotColumns[i].comboBox_body_lxx.SelectedIndex < 1 ? false : true);
                xml.TextureID = -1;//Is recalculated lower
                xml.MetalModel = CharacterSlotModXML.MetalModelStatus.Works;
                xml.WifiSafe = true; //Assuming wifi-safe
                xml.UseCustomName = xml.chrn_11;
                xml.CharacterName = xml.UseCustomName ? slotColumns[i].textBox_name.Text : "";
                #endregion

                #region Model Files
                int modelPartsCount = _CurrentFighter.modelParts.Count() - 1;
                string baseModelPath = baseModPath + "model" + Path.DirectorySeparatorChar;
                if (slotColumns[i].comboBox_body.SelectedIndex >= 1)
                {
                    xml.MetalModel = CharacterSlotModXML.MetalModelStatus.Unknown;
                    string foldername = ModelNutDirectories[slotColumns[i].comboBox_body.SelectedIndex - 1];
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
                    Utils.CopyAllValidFilesBetweenDirectories(foldername, baseModelPath + "body" + Path.DirectorySeparatorChar);
                }
                for (int k = 0; k < modelPartsCount; ++k)
                {
                    if (slotColumns[i].comboBox_parts[k].SelectedIndex < 1) continue;
                    xml.MetalModel = CharacterSlotModXML.MetalModelStatus.Unknown;
                    string foldername = ModelNutDirectories[slotColumns[i].comboBox_parts[k].SelectedIndex - 1];
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
                    Utils.CopyAllValidFilesBetweenDirectories(foldername, baseModelPath + _CurrentFighter.modelParts[k + 1] + Path.DirectorySeparatorChar);
                }
                if (xml.Haslxx)
                {
                    xml.MetalModel = CharacterSlotModXML.MetalModelStatus.Unknown;
                    string foldername = ModelNutDirectories[slotColumns[i].comboBox_body_lxx.SelectedIndex - 1];
                    foldername = foldername.Replace(Path.GetFileName(foldername), string.Empty);
                    Utils.CopyAllValidFilesBetweenDirectories(foldername, baseModelPath + "lxx" + Path.DirectorySeparatorChar);
                }
                #endregion

                #region Chr Files
                if (xml.chr_00 || xml.chr_11 || xml.chr_13 || xml.stock_90 || xml.chrn_11)
                {
                    string baseChrPath = baseModPath + "chr" + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(baseChrPath);
                    if (xml.chr_00) File.Copy(Files_chr_00[slotColumns[i].comboBox_chr_00.SelectedIndex - 1], baseChrPath + "chr_00_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.chr_11) File.Copy(Files_chr_11[slotColumns[i].comboBox_chr_11.SelectedIndex - 1], baseChrPath + "chr_11_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.chr_13) File.Copy(Files_chr_13[slotColumns[i].comboBox_chr_13.SelectedIndex - 1], baseChrPath + "chr_13_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.stock_90) File.Copy(Files_stock_90[slotColumns[i].comboBox_stock_90.SelectedIndex - 1], baseChrPath + "stock_90_" + _CurrentFighter.name + "_XX.nut");
                    if (xml.chrn_11) File.Copy(Files_chrn_11[slotColumns[i].comboBox_chrn_11.SelectedIndex - 1], baseChrPath + "chrn_11_" + _CurrentFighter.name + "_XX.nut");
                }
                #endregion

                #region Sound Files
                if (xml.Sound || xml.Voice)
                {
                    string baseSoundPath = baseModPath + "sound" + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(baseSoundPath);
                    string fighterName = _CurrentFighter.name;
                    if (_CurrentFighter.id == 0x19 && !_SmashProjectManager.CurrentProject.IsSwitch)
                        fighterName = fighterName.Replace("Szerosuit", "SZerosuit");
                    if (_CurrentFighter.id == 0x24 && !_SmashProjectManager.CurrentProject.IsSwitch)
                        fighterName = fighterName.Replace("Drmario", "MarioD");
                    if (_CurrentFighter.id == 0x26 && !_SmashProjectManager.CurrentProject.IsSwitch)
                        fighterName = fighterName.Replace("Pitb", "PitB");
                    if (_CurrentFighter.id == 0x13 && !_SmashProjectManager.CurrentProject.IsSwitch)
                        fighterName = fighterName.Replace("Gamewatch", "GameWatch");
                    if (xml.Sound) File.Copy(Files_Sound_Nus3bank[slotColumns[i].comboBox_sound.SelectedIndex - 1], baseSoundPath + "snd_se_" + _CurrentFighter.name + "_cxx.nus3bank");
                    if (xml.Voice) File.Copy(Files_Voice_Nus3bank[slotColumns[i].comboBox_voice.SelectedIndex - 1], baseSoundPath + "snd_vc_" + _CurrentFighter.name + "_cxx.nus3bank");
                }
                #endregion

                Utils.SerializeXMLToFile<CharacterSlotModXML>(xml, baseModPath + "kamimod.xml");
                LogHelper.Info(String.Format("Mod {0} imported successfully!", slotColumns[i].textBox_name.Text));
            }
            importSuccessful = true;
            this.Close();
        }

        private void ModImportCharacterSlot_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (importSuccessful) return;
                if (MessageBox.Show("Are you sure you want to cancel the import?", "Cancel Import", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
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
        #endregion

        #endregion

        private class SlotColumn
        {
            public TextBox textBox_name = new TextBox();
            public FlowLayoutPanel panel = new FlowLayoutPanel();
            public Label label_chr_00 = new Label();
            public Label label_chr_11 = new Label();
            public Label label_chr_13 = new Label();
            public Label label_stock_90 = new Label();
            public Label label_chrn_11 = new Label();
            public Label label_sound = new Label();
            public Label label_voice = new Label();
            public Label label_body = new Label();
            public Label label_body_lxx = null;
            public Label[] label_parts = null;
            public ComboBox comboBox_chr_00;
            public ComboBox comboBox_chr_11;
            public ComboBox comboBox_chr_13;
            public ComboBox comboBox_stock_90;
            public ComboBox comboBox_chrn_11;
            public ComboBox comboBox_sound;
            public ComboBox comboBox_voice;
            public ComboBox comboBox_body;
            public ComboBox comboBox_body_lxx = null;
            public ComboBox[] comboBox_parts = null;
            public Button buttonRemove = new Button();

            public SlotColumn(ModImportCharacterSlot parent, string defaultName, bool has_lxx, string[] parts = null)
            {
                int maxWidth = 150;
                foreach (string str in parent.ComboBoxList_ModelNutDirectories) {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_chr_00)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_chr_11)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_chr_13)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_stock_90)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_chrn_11)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_Sound_Nus3bank)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                foreach (string str in parent.ComboBoxList_Files_Voice_Nus3bank)
                {
                    Size s = TextRenderer.MeasureText(str, label_chr_00.Font);
                    if (s.Width > maxWidth) maxWidth = s.Width;
                }
                maxWidth += 20;

                panel.AutoSize = true;
                panel.FlowDirection = FlowDirection.TopDown;
                panel.WrapContents = false;

                textBox_name.Text = defaultName;
                //textBox_name.Size = new Size(maxWidth + 4, 30);

                textBox_name.Parent = parent.flowLayoutPanelNames;
                panel.Parent = parent.flowLayoutPanelContents;

                label_body.Text = UIStrings.MOD_IMPORT_BODY;
                label_body.AutoSize = true;
                label_body.Parent = panel;
                comboBox_body = new ComboBox {
                    DataSource = parent.ComboBoxList_ModelNutDirectories.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                if (has_lxx)
                {
                    label_body_lxx = new Label();
                    label_body_lxx.Text = UIStrings.MOD_IMPORT_BODY_LXX;
                    label_body_lxx.AutoSize = true;
                    label_body_lxx.Parent = panel;
                    comboBox_body_lxx = new ComboBox {
                        DataSource = parent.ComboBoxList_ModelNutDirectories.Clone(),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        BindingContext = new BindingContext(),
                        Width = maxWidth,
                        Parent = panel
                    };
                }
                if (parts != null)
                {
                    if (parts.Length > 1)
                    {
                        label_parts = new Label[parts.Length - 1];
                        comboBox_parts = new ComboBox[parts.Length - 1];
                        for (int i = 0; i < parts.Length - 1; ++i)
                        {
                            label_parts[i] = new Label();
                            label_parts[i].Text = parts[i + 1] + " " + UIStrings.MOD_IMPORT_EXTRA_PART;
                            label_parts[i].AutoSize = true;
                            label_parts[i].Parent = panel;
                            comboBox_parts[i] = new ComboBox {
                                DataSource = parent.ComboBoxList_ModelNutDirectories.Clone(),
                                DropDownStyle = ComboBoxStyle.DropDownList,
                                BindingContext = new BindingContext(),
                                Width = maxWidth,
                                Parent = panel
                            };
                        }
                    }
                }

                label_chr_00.Text = UIStrings.MOD_IMPORT_CHR_00;
                label_chr_00.AutoSize = true;
                label_chr_00.Parent = panel;
                comboBox_chr_00 = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_chr_00.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                label_chr_11.Text = UIStrings.MOD_IMPORT_CHR_11;
                label_chr_11.AutoSize = true;
                label_chr_11.Parent = panel;
                comboBox_chr_11 = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_chr_11.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                label_chr_13.Text = UIStrings.MOD_IMPORT_CHR_13;
                label_chr_13.AutoSize = true;
                label_chr_13.Parent = panel;
                comboBox_chr_13 = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_chr_13.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                label_stock_90.Text = UIStrings.MOD_IMPORT_STOCK_90;
                label_stock_90.AutoSize = true;
                label_stock_90.Parent = panel;
                comboBox_stock_90 = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_stock_90.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                label_chrn_11.Text = UIStrings.MOD_IMPORT_CHRN_11;
                label_chrn_11.AutoSize = true;
                label_chrn_11.Parent = panel;
                comboBox_chrn_11 = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_chrn_11.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                label_sound.Text = UIStrings.MOD_IMPORT_SOUND;
                label_sound.AutoSize = true;
                label_sound.Parent = panel;
                comboBox_sound = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_Sound_Nus3bank.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                label_voice.Text = UIStrings.MOD_IMPORT_VOICE;
                label_voice.AutoSize = true;
                label_voice.Parent = panel;
                comboBox_voice = new ComboBox {
                    DataSource = parent.ComboBoxList_Files_Voice_Nus3bank.Clone(),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BindingContext = new BindingContext(),
                    Width = maxWidth,
                    Parent = panel
                };

                buttonRemove.Text = "Remove Slot";
                buttonRemove.Click += parent.buttonRemoveRow_Click;
                buttonRemove.Dock = DockStyle.Top;
                buttonRemove.Parent = panel;

                textBox_name.Size = new Size(panel.Size.Width, 30);
            }
        }
    }
}
