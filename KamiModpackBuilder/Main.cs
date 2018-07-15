using DamienG.Security.Cryptography;
using KamiModpackBuilder.Globals;
using KamiModpackBuilder.Objects;
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

namespace KamiModpackBuilder
{
    public partial class Main : Form
    {

        #region Members
        ConsoleRedirText _ConsoleText = null;
        ConsoleRedirProgress _ConsoleProgress = null;
        bool _MainLoaded = false;
        bool _UserControlsLoaded = false;
        SmashProjectManager _ProjectManager = null;
        #endregion

        #region Properties
        internal bool MainLoaned { get { return _MainLoaded; } }
        #endregion

        #region Constructors
        public Main()
        {
            InitializeComponent();

            //Version
            this.Text += " v." + GlobalConstants.VERSION;

            //Loading ProjectManager
            _ProjectManager = new SmashProjectManager();
            _ProjectManager.LoadConfig();
            
            //Loading configuration
            if (!File.Exists(_ProjectManager._Config.LastProject))
            {
                if (!CreateProject())
                {
                    this.Close();
                    return;
                }
            }
            
            //Console Redirection
            _ConsoleText = new ConsoleRedirText(textConsole);
            _ConsoleProgress = new ConsoleRedirProgress(backgroundWorker);

            _MainLoaded = true;
        }
        #endregion
        
        #region Save/Load
        public bool CreateProject()
        {
            MessageBox.Show(this, UIStrings.CREATE_PROJECT_FIND_FOLDER, UIStrings.CAPTION_CREATE_PROJECT);
            while (true)
            {
                DialogResult result = folderBrowserDialog.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string gameFolder = folderBrowserDialog.SelectedPath + Path.DirectorySeparatorChar;
                    if (!PathHelper.IsItSmashFolder(gameFolder))
                    {
                        MessageBox.Show(this, UIStrings.ERROR_LOADING_GAME_FOLDER, UIStrings.CAPTION_ERROR_LOADING_GAME_FOLDER);
                        continue;
                    }
                    if (!PathHelper.DoesItHavePatchFolder(gameFolder))
                    {
                        MessageBox.Show(this, UIStrings.ERROR_LOADING_GAME_PATCH_FOLDER, UIStrings.CAPTION_ERROR_LOADING_GAME_FOLDER);
                        continue;
                    }

                    MessageBox.Show(this, UIStrings.CREATE_PROJECT_CHOOSE_NAME_LOCATION, UIStrings.CAPTION_CREATE_PROJECT);
                    DialogResult saveResult = saveFileDialog.ShowDialog(this);
                    if (saveResult == DialogResult.OK)
                    {
                        LogHelper.Info("Creating project file...");
                        SmashMod newProject = _ProjectManager.CreateNewProject(saveFileDialog.FileName, gameFolder);
                        new Forms.CreationProjectInfo(newProject, _ProjectManager).ShowDialog(this);
                        MessageBox.Show(this, UIStrings.CREATE_PROJECT_SUCCESS, UIStrings.CAPTION_CREATE_PROJECT);

                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public void LoadProject()
        {
            _ProjectManager.LoadProject(_ProjectManager._Config.LastProject);
            if (_ProjectManager.CurrentProject == null)
            {
                MessageBox.Show(this, UIStrings.ERROR_LOADING_PROJECT, UIStrings.CAPTION_ERROR_LOADING_GAME_FOLDER);
                Application.Exit();
                return;
            }
            //_Options = new Options(_ProjectManager.CurrentProject);
        }

        public void LoadProjectCompleted()
        {
            if (_ProjectManager.CurrentProject == null)
            {
                MessageBox.Show(UIStrings.ERROR_LOADING_GAME_LOAD_FOLDER, UIStrings.CAPTION_ERROR_LOADING_GAME_FOLDER);
                Application.Exit();
                return;
            }

            this.Enabled = true;
            Console.SetOut(_ConsoleText);

            //Check if project directories exist
            if (!Directory.Exists(PathHelper.FolderExplorer)) Directory.CreateDirectory(PathHelper.FolderExplorer);
            if (!Directory.Exists(PathHelper.FolderStageMods)) Directory.CreateDirectory(PathHelper.FolderStageMods);
            if (!Directory.Exists(PathHelper.FolderGeneralMods)) Directory.CreateDirectory(PathHelper.FolderGeneralMods);
            if (!Directory.Exists(PathHelper.FolderEditorMods)) Directory.CreateDirectory(PathHelper.FolderEditorMods);

            foreach (DB.Fighter fighter in DB.FightersDB.Fighters)
            {
                string path = PathHelper.GetCharacterSlotsFolder(fighter.name);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = PathHelper.GetCharacterGeneralFolder(fighter.name);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }
        }
        #endregion

        #region Events
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadProject();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            textConsole.AppendText(e.UserState.ToString() + Environment.NewLine);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadProjectCompleted();

            if (!_UserControlsLoaded)
            {
                //User Controls in Tabs
                UserControls.CharacterMods characterMods = new UserControls.CharacterMods(_ProjectManager);
                characterMods.Parent = tabPageCharacterMods;
                characterMods.Dock = DockStyle.Fill;
                _ProjectManager._CharacterModsPage = characterMods;

                UserControls.Explorer explorer = new UserControls.Explorer(_ProjectManager);
                explorer.Parent = tabPageExplorer;
                explorer.Dock = DockStyle.Fill;
                _ProjectManager._ExplorerPage = explorer;

                _UserControlsLoaded = true;
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            this.Enabled = false;
            Console.SetOut(_ConsoleProgress);

            backgroundWorker.RunWorkerAsync();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ProjectManager != null)
                _ProjectManager.CleanTempFolder();
        }

        private void SaveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ProjectManager.SaveProject();
            _ProjectManager.SaveConfig();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProject();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                LogHelper.Info("Loading project file...");
                _ProjectManager.LoadProject(openFileDialog.FileName);

                _ProjectManager.RefreshTabViews();
            }
        }

        private void projectSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.ProjectSettings settings = new Forms.ProjectSettings(_ProjectManager.CurrentProject);
            settings.ShowDialog();
            _ProjectManager.SaveProject();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Preferences preferences = new Forms.Preferences(_ProjectManager);
            preferences.ShowDialog();
            _ProjectManager.SaveConfig();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ProjectManager.SaveProject();
            _ProjectManager.SaveConfig();
            this.Close();
        }

        private void aboutKamiModpackBuilderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format(UIStrings.INFO_ABOUT + "\r\n" + UIStrings.INFO_THANKS, GlobalConstants.VERSION));
        }
        #endregion
    }
}
