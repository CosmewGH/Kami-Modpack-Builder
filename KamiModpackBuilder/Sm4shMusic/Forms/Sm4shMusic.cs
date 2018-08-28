using KamiModpackBuilder;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.Sm4shMusic.Globals;
using KamiModpackBuilder.Sm4shMusic.Objects;
using KamiModpackBuilder.Sm4shMusic.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KamiModpackBuilder.Sm4shMusic.Forms
{
    public delegate void XMLLoaded(object sender, EventArgs e);

    public partial class Main : Form
    {
        private BGMManagement _BGMManagement;
        private SmashProjectManager _SmashProjectManager;
        private MyMusicManagement _MyMusicManagement;
        private SoundEntryCollection _SoundEntryCollection;

        public event XMLLoaded XMLLoaded;

        public SoundEntryCollection SoundEntryCollection
        {
            get { return _SoundEntryCollection; }
            set
            {
                _SoundEntryCollection = value;
            }
        }

        public Main()
        {
            InitializeComponent();
            _BGMManagement = new BGMManagement();
            tabBGMManagement.Controls.Add(_BGMManagement);
            _BGMManagement.Dock = DockStyle.Fill;
            _MyMusicManagement = new MyMusicManagement();
            tabStage.Controls.Add(_MyMusicManagement);
            _MyMusicManagement.Dock = DockStyle.Fill;
        }

        public void Initialize()
        {
            //Init sound files
            refreshBGMFilesListToolStripMenuItem_Click(this, new EventArgs());

           SoundEntryCollection sEntryCollectionOriginal = (SoundEntryCollection)_SoundEntryCollection.Clone();
                _BGMManagement.SoundEntryCollection = _SoundEntryCollection;
                _BGMManagement.SoundEntryCollectionBackup = sEntryCollectionOriginal;
                _MyMusicManagement.SoundEntryCollection = _SoundEntryCollection;
                _MyMusicManagement.SoundEntryCollectionBackup = sEntryCollectionOriginal;
            _BGMManagement.Initialize(sm4shProject);
            _MyMusicManagement.Initialize(sm4shProject);
        }

        public void Start(SmashProjectManager project)
        {
            _SmashProjectManager = project;

            _SoundEntryCollection = new SoundEntryCollection();

            BGMFilesDB.InitializeBGMDB(_SmashProjectManager.CurrentProject.GameVersion, _SmashProjectManager.CurrentProject.IsSwitch);
            BGMStageDB.InitializeBGMMyMusicDB(_SmashProjectManager.CurrentProject.GameVersion, _SmashProjectManager.CurrentProject.IsSwitch);
            _PropertyFile = new PropertyFile(_SmashProjectManager, _SoundEntryCollection, "data/sound/config/bgm_property.mpb");
            _SoundEntryCollection.GenerateSoundEntriesBGMDictionary();
            _UISoundDBFile = new UISoundDBFile(_SmashProjectManager, _SoundEntryCollection, "data/param/ui/ui_sound_db.bin");
            _SoundEntryCollection.GenerateSoundEntriesDictionary();
            _MyMusicFile = new MyMusicFile(_SmashProjectManager, _SoundEntryCollection, "data/sound/config/bgm_mymusic.mmb");

            //Generation SoundMSBT dictionaries
            _SoundMSBTFiles = new List<SoundMSBTFile>();
            foreach (ResourceCollection resCol in _SmashProjectManager.ResourceDataCollection)
            {
                if (!resCol.CachedFilteredResources.ContainsKey("ui/message/sound.msbt"))
                    continue;
                SoundMSBTFile soundMSBTFile = new SoundMSBTFile(_SmashProjectManager, _SoundEntryCollection, resCol.PartitionName + "/ui/message/sound.msbt");
                _SoundMSBTFiles.Add(soundMSBTFile);
            }

            //Generate Dictionaries
            _SoundEntryCollection.GenerateStagesDictionaries();

            //Check stage sound integrity between MyMusic Stages and SoundDB Stages.
            //General rule: All BGMS present in MyMusic stages must be in SoundDB, the opposite isnt true.
            foreach (MyMusicStage myMusicStage in _SoundEntryCollection.MyMusicStages)
            {
                if (myMusicStage.BGMStage.BGMDBID != null)
                {
                    //From MyMusic
                    List<BGMEntry> bgmsMyMusic = new List<BGMEntry>();
                    foreach (MyMusicStageBGM myMusicStageBGM in myMusicStage.BGMs)
                    {
                        //if(myMusicStageBGM.unk4 == 0x0) //Filter songs
                        bgmsMyMusic.Add(myMusicStageBGM.BGMEntry);
                    }

                    //From SoundDB
                    List<BGMEntry> bgmsSoundDB = new List<BGMEntry>();
                    foreach (SoundDBStageSoundEntry sDBStageSoundEntry in _SoundEntryCollection.SoundDBStagesPerID[(int)myMusicStage.BGMStage.BGMDBID].SoundEntries)
                    {
                        if (sDBStageSoundEntry.SoundEntry == null)
                        {
                            LogHelper.Warning(string.Format("SOUNDID '{0}' have an issue with one of its associated stages.", sDBStageSoundEntry.SoundID));
                            continue;
                        }
                        foreach (SoundEntryBGM sEntryBGM in sDBStageSoundEntry.SoundEntry.BGMFiles)
                            bgmsSoundDB.Add(sEntryBGM.BGMEntry);
                    }
                    //HACK FOR KK
                    foreach (SoundEntry sEntry in _SoundEntryCollection.SoundEntries)
                    {
                        if (!sEntry.InSoundTest)
                        {
                            foreach (SoundEntryBGM sEntryBGM in sEntry.BGMFiles)
                            {
                                if (sEntryBGM.BGMEntry != null)
                                    bgmsSoundDB.Add(sEntryBGM.BGMEntry);
                            }
                        }
                    }

                    //Compare
                    foreach (BGMEntry sEntryBGM in bgmsMyMusic)
                    {
                        if (!bgmsSoundDB.Contains(sEntryBGM))
                        {
                            //throw new Exception(string.Format("Error sound integrity between MyMusic Stages and SoundDB Stages for stage '{0}': '{1}' was not present.", myMusicStage.BGMStage.Label, sEntryBGM.BGMTitle));
                            LogHelper.Error(string.Format("Error sound integrity between MyMusic Stages and SoundDB Stages for stage '{0}': '{1}' was not present.", myMusicStage.BGMStage.Label, sEntryBGM.BGMTitle));
                        }
                    }
                }
            }

            //Launch Form
            Initialize();
        }

        #region Buttons
        private void generateCSVForSoundDBAndMSBTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "CSV files|*.csv";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.FileName = "SoundDB.csv";
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string pathToSave = saveFileDialog.FileName;
                Debug.WriteDebugSoundMSBTCSV(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format(Strings.DEBUG_EXPORT, pathToSave));
            }
        }

        private void generateCSVForBGMEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "CSV files|*.csv";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.FileName = "SoundEntriesBGMs.csv";
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string pathToSave = saveFileDialog.FileName;
                Debug.WriteDebugBGMEntriesCSV(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format(Strings.DEBUG_EXPORT, pathToSave));
            }
        }

        private void generateCSVForMyMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "CSV files|*.csv";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.FileName = "MyMusic.csv";
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string pathToSave = saveFileDialog.FileName;
                Debug.WriteDebugMyMusicCSV(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format(Strings.DEBUG_EXPORT, pathToSave));
            }
        }

        private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            saveFileDialog.Filter = "XML files|*.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = "sm4shsound_config.xml";
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string pathToSave = saveFileDialog.FileName;
                Serializer.SerializeObjectToXml(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format("Sm4shMusic: Configuration '{0}' saved.", pathToSave));
            }
        }

        private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML files|*.xml";
            openFileDialog.DefaultExt = "xml";
            openFileDialog.FileName = "sm4shsound_config.xml";
            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string pathToOpen = openFileDialog.FileName;
                SoundEntryCollection = (SoundEntryCollection)Serializer.DeserializeXmlToObject<SoundEntryCollection>(pathToOpen).Clone();

                //Hack for textarea
                try
                {
                    foreach (SoundEntry sEntry in SoundEntryCollection.SoundEntries)
                    {
                        if (!string.IsNullOrEmpty(sEntry.Title))
                            sEntry.Title = sEntry.Title.Replace("\n", Environment.NewLine);
                        else
                            sEntry.Title = string.Empty;

                        if (!string.IsNullOrEmpty(sEntry.SoundTestTitle))
                            sEntry.SoundTestTitle = sEntry.SoundTestTitle.Replace("\n", Environment.NewLine);
                        else
                            sEntry.SoundTestTitle = string.Empty;

                        if (!string.IsNullOrEmpty(sEntry.Description))
                            sEntry.Description = sEntry.Description.Replace("\n", Environment.NewLine);
                        else
                            sEntry.Description = string.Empty;

                        if (!string.IsNullOrEmpty(sEntry.Description2))
                            sEntry.Description2 = sEntry.Description2.Replace("\n", Environment.NewLine);
                        else
                            sEntry.Description2 = string.Empty;

                        if (!string.IsNullOrEmpty(sEntry.Source))
                            sEntry.Source = sEntry.Source.Replace("\n", Environment.NewLine);
                        else
                            sEntry.Source = string.Empty;
                    }
                }
                catch(Exception ex)
                {
                    LogHelper.Error("Error parsing string variables.");
                }

                if (XMLLoaded != null)
                    this.XMLLoaded(this, new EventArgs());

                LogHelper.Info(string.Format("Sm4shMusic: Configuration '{0}' loaded.", pathToOpen));
                Initialize(_SmashProjectManager);
            }
        }

        private void listAllOrphanBGMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Info(SoundEntryCollection.CleanBGMDatabase(false));
        }

        private void compileTheModificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void exitWillCancelAllChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshBGMFilesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sm4shSoundTools.RefreshSoundFiles();
            List<string> soundFiles = Sm4shSoundTools.SoundFiles;

            SoundEntryCollection.CleanBGMDatabase(true);
            foreach (string soundFile in soundFiles)
            {
                if (_SoundEntryCollection.SoundEntriesBGMsPerName.ContainsKey(soundFile))
                    continue;
                SoundEntryCollection.CreateBGMEntry(soundFile);
            }

            _BGMManagement.RefreshBGMTextArea();

            LogHelper.Info("Sm4shMusic: BGM List refreshed.");
        }

        private void thanksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _About.ShowDialog(this);
        }
        #endregion 

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            VGMStreamPlayer.StopCurrentVGMStreamPlayback();
            _BGMManagement.Clean();
            _MyMusicManagement.Clean();
        }
    }
}
