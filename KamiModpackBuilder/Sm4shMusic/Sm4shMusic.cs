using KamiModpackBuilder;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.Sm4shMusic.DB;
using KamiModpackBuilder.Sm4shMusic.Forms;
using KamiModpackBuilder.Sm4shMusic.Globals;
using KamiModpackBuilder.Sm4shMusic.Objects;
using KamiModpackBuilder.Sm4shMusic.UserControls;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using KamiModpackBuilder.Objects;
using System;

namespace KamiModpackBuilder.Sm4shMusic
{
    public class Sm4shMusic
    {
        public const string VERSION = "0.6";

        #region Members
        private SoundEntryCollection _SoundEntryCollection;
        private PropertyFile _PropertyFile;
        private UISoundDBFile _UISoundDBFile;
        private MyMusicFile _MyMusicFile;
        private List<SoundMSBTFile> _SoundMSBTFiles;
        private SmashProjectManager _SmashProjectManager;

        private SmashProjectManager _Sm4shProjectManager;
        private BGMManagement _BGMManagement;
        private MyMusicManagement _MyMusicManagement;
        #endregion

        #region Properties
        public SoundEntryCollection SoundEntryCollection
        {
            get { return _SoundEntryCollection; }
            set
            {
                _SoundEntryCollection = value;
            }
        }
        #endregion

        public Sm4shMusic(SmashProjectManager project)
        {
            _SmashProjectManager = project;
            _BGMManagement = _SmashProjectManager._BGMManagementPage;
            _MyMusicManagement = _SmashProjectManager._MyMusicPage;
            Load();
        }

        private void Load()
        {
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
            Initialize();
        }

        private void Initialize()
        {
            //Init sound files
            RefreshBGMFilesList();

            SoundEntryCollection sEntryCollectionOriginal = (SoundEntryCollection)_SoundEntryCollection.Clone();
            _BGMManagement.SoundEntryCollection = _SoundEntryCollection;
            _BGMManagement.SoundEntryCollectionBackup = sEntryCollectionOriginal;
            _MyMusicManagement.SoundEntryCollection = _SoundEntryCollection;
            _MyMusicManagement.SoundEntryCollectionBackup = sEntryCollectionOriginal;

            _BGMManagement.Initialize(_SmashProjectManager);
            _MyMusicManagement.Initialize(_SmashProjectManager);
        }

        private void _Main_XMLLoaded(object sender, EventArgs e)
        {
            _PropertyFile.SoundEntryCollection = _SoundEntryCollection;
            _MyMusicFile.SoundEntryCollection = _SoundEntryCollection;
            _UISoundDBFile.SoundEntryCollection = _SoundEntryCollection;
            foreach(SoundMSBTFile msbtFile in _SoundMSBTFiles)
                msbtFile.SoundEntryCollection = _SoundEntryCollection;
        }
        
        private void Save()
        {
            _SoundEntryCollection.CleanBGMDatabase(true);
            _PropertyFile.BuildFile();
            foreach (SoundMSBTFile soundMSBTFile in _SoundMSBTFiles)
                soundMSBTFile.BuildFile();
            _UISoundDBFile.BuildFile();
            _MyMusicFile.BuildFile();

            LogHelper.Info(Strings.INFO_COMPILED);
        }

        #region Buttons
        public void GenerateCSVForSoundDBAndMSBT()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV files|*.csv";
            save.DefaultExt = "csv";
            save.FileName = "SoundDB.csv";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string pathToSave = save.FileName;
                Debug.WriteDebugSoundMSBTCSV(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format(Strings.DEBUG_EXPORT, pathToSave));
            }
        }

        public void GenerateCSVForBGMEntries()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV files|*.csv";
            save.DefaultExt = "csv";
            save.FileName = "SoundEntriesBGMs.csv";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string pathToSave = save.FileName;
                Debug.WriteDebugBGMEntriesCSV(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format(Strings.DEBUG_EXPORT, pathToSave));
            }
        }

        public void GenerateCSVForMyMusic()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV files|*.csv";
            save.DefaultExt = "csv";
            save.FileName = "MyMusic.csv";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string pathToSave = save.FileName;
                Debug.WriteDebugMyMusicCSV(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format(Strings.DEBUG_EXPORT, pathToSave));
            }
        }

        public void SaveConfiguration()
        {
            _BGMManagement.ValidateChildren();
            _MyMusicManagement.ValidateChildren();
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "XML files|*.xml";
            save.DefaultExt = "xml";
            save.FileName = "sm4shsound_config.xml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string pathToSave = save.FileName;
                Serializer.SerializeObjectToXml(_SoundEntryCollection, pathToSave);
                LogHelper.Info(string.Format("Sm4shMusic: Configuration '{0}' saved.", pathToSave));
            }
        }

        public void LoadConfiguration()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML files|*.xml";
            open.DefaultExt = "xml";
            open.FileName = "sm4shsound_config.xml";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string pathToOpen = open.FileName;
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
                catch (Exception ex)
                {
                    LogHelper.Error("Error parsing string variables.");
                }

                LogHelper.Info(string.Format("Sm4shMusic: Configuration '{0}' loaded.", pathToOpen));
                Initialize();
            }
        }

        public void ListAllOrphanBGMs()
        {
            LogHelper.Info(SoundEntryCollection.CleanBGMDatabase(false));
        }

        public void CompileTheModifications()
        {
            _BGMManagement.ValidateChildren();
            _MyMusicManagement.ValidateChildren();
            Save();
        }

        public void RefreshBGMFilesList()
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
        #endregion 

        ~Sm4shMusic()
        {
            VGMStreamPlayer.StopCurrentVGMStreamPlayback();
            _BGMManagement.Clean();
            _MyMusicManagement.Clean();
        }
    }
}
