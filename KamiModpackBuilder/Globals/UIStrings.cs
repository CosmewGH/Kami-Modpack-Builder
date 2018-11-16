using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KamiModpackBuilder.Globals
{
    internal class UIStrings
    {
        public const string CAPTION_ERROR_LOADING_GAME_FOLDER = "Error loading game folder";
        public const string ERROR_LOADING_GAME_FOLDER = "It seems that the game folder is missing a few important element. In order for Kami Modpack Builder to work, make sure the folder 'content' exists. This folder should contain the latest patch as well as the files 'ls' and 'dt00'/'dt01'.";
        public const string ERROR_LOADING_GAME_PATCH_FOLDER = "Warning. It seems that the game folder selected does not contain a patch folder.";
        public const string ERROR_LOADING_GAME_LOAD_FOLDER = "The game path can't be found anymore. Please update the GamePath in your project's XML file. In order for Kami Modpack Builder to work, make sure the folder 'content' exists. This folder should contain the latest patch as well as the files 'ls' and 'dt00'/'dt01'.";
        public const string ERROR_LOADING_PROJECT = "There was an error loading the project, please consult the logs.";

        public const string CAPTION_CREATE_PROJECT = "Create project";
        public const string CREATE_PROJECT_SUCCESS = "Project created!";
        public const string CREATE_PROJECT_FIND_FOLDER = "First, please indicate the folder where Kami Modpack Builder can find the latest version of the game. This folder must contain the folder 'content'. This folder will not be modified by this program.\r\n\r\nPlease understand that this program is meant to be used with the full version of the game. The best setting is a dump of your game + the latest patch folder.";
        public const string CREATE_PROJECT_CHOOSE_NAME_LOCATION = "Next, choose the location you want the project to be created, as well as the name of the project. Individual paths of input and output folders can be changed in the Project Settings.";
        
        public const string CAPTION_FILE_MODIFIED = "File modified";
        public const string INFO_FILE_MODIFIED = "The resource {0} seems to have been modified, do you want to include it to the project?";

        public const string INFO_FILE_HEX = "You must first set up the path to your hex editor.";

        public const string CAPTION_PACK_REBUILD = "Rebuild resources";
        public const string WARN_EXPORT_FOLDER_EXISTS = "Warning. The folder '{0}' already exists. If you wish to continue, all content from this folder will be deleted. Proceed?";
        public const string INFO_PACK_REBUILD = "This feature will rebuild the resource files and patchfile. \r\nThis will ensure that the game takes your changes into account. When it's done, you can find the file in this folder:\r\n{0}";

        public const string WARNING_ERROR_CHECKING_METAL_TEXTURES = "The metal.nud of mod {0} part {1} is missing the following textures: {2}. This may cause errors with the model.";
        public const string WARNING_ERROR_CHECKING_METAL_NUD_MISSING = "Mod '{0}' does not have a {1} metal.nud! If it needs one, it likely wont work properly.";
        public const string WARNING_ERROR_CHECKING_MODEL_TEXTURES = "The model.nud of mod {0} part {1} is missing the following textures: {2}. This may cause errors with the model.";
        public const string WARNING_ERROR_CHECKING_DUPLICATE_TEXTURES = "The model.nut in mod '{0}' part {1} has multiple textures with the same TextureIDs. This is likely not intentional.";
        public const string ERROR_ERROR_CHECKING_MODEL_NUT_MISSING = "Mod '{0}' is missing its {1} model.nut!";
        public const string ERROR_ERROR_CHECKING_MODEL_NUD_MISSING = "Mod '{0}' is missing its {1} model.nud!";
        public const string INFO_ERROR_CHECKING_NO_MODELS = "Mod '{0}' has no models";
        public const string TEXTURE_ID_FIX_NO_IDS_AVAILABLE = "Could not Texture ID Fix all the mods, ran out of texture IDs! That's both impressive and concerning. Use less mods.";

        public const string INFO_ABOUT =
            "Kami Modpack Builder v.{0} by Saiyan197\r\n" +
            "Based largely on Sm4shExplorer by Deinonychus71\r\n";
        public const string INFO_THANKS =
            "Thanks to:\r\n" +
            "- Deinonychus71 for creating Sm4shExplorer\r\n" +
            "- Sammi Husky for his DTLSExtractor and other tools\r\n" +
            "- Soneek for his work with Sm4sh Music\r\n" +
            "- IcySon55 for his MSBT Editor\r\n" +
            "- Zarklord, Muno, G_F_D, and mariosonicds for their contributions for the Auto TexID Fix scripts\r\n" +
            "- Everyone else who has contributed to the Smash Modding Community";

        public const string MOD_IMPORT_CHR_00 = "chr_00 (Battle Portrait)";
        public const string MOD_IMPORT_CHR_11 = "chr_11 (8-Player CSS Portrait)";
        public const string MOD_IMPORT_CHR_13 = "chr_13 (CSS Portrait)";
        public const string MOD_IMPORT_CHRN_11 = "chrn_11 (CSS Nameplate)";
        public const string MOD_IMPORT_STOCK_90 = "stock_90 (Stock Icon)";
        public const string MOD_IMPORT_VOICE = "Voice Pack";
        public const string MOD_IMPORT_SOUND = "Sound Pack";
        public const string MOD_IMPORT_BODY = "body (Primary Model)";
        public const string MOD_IMPORT_BODY_LXX = "body lxx (Primary Low Poly Model)";
        public const string MOD_IMPORT_EXTRA_PART = "(Extra Neccessary Model)";

        public const string CAPTION_HELP = "Help";
        public const string HELPBOX_ERROR = "Helptext index out of bounds! Report to the dev please!";
        public static readonly string[] HELPBOX_TEXT =
        {
            "This is the Character Mods tab. You can add Slot Mods, which are mods which only affect a single skin slot. You can also add Character General Mods, which affect all skins of the character. Turn mods on by moving them to the left.",
            "This is the Stage Mods tab. A Stage Mod goes over either the Normal version OR the Omega version of a stage. Turn mods on by moving them to the left.",
            "This is the General Mods tab. A General Mod can overwrite any of the files found in the Explorer Tab. General mods have lower priority than Character and Stage Mods.",
            "This is the BGM Management tab. Music can be added to the game using this tab. Add .nus3bank files to the BGM folder of this projects Workspace folder to add songs. Song files must start with 'snd_bgm_'",
            "This is the My Music tab. Songs that have been added to the BGM list on the BGM Management tab can be added to stages here. Select the stage on the left, then add the desired song to both lists on the right.",
            "This is the Explorer tab. Source files from the game can be extracted to be used in mods here. Extracted files go into the Extract Folder in your Workspace by default. Mods can also be added by overwriting files here, but they cannot be toggled on/off easily. Mods added in the Explorer tab have lower priority over other mods.",
            "This is the Slot Mod Import Window. Multiple skins can be added at once. Each column is an individual skin. Name each skin, and make sure each data set corresponds to the correct skin you want. Most fields are optional, choose 'None' if you want to use nothing for that data. For more info, see the main project page on Gamebanana or GBATemp."
        };
        public const string HELP_CHARACTERMODS_BUTTONS =
            "--Fighter Dropdown--\r\n" +
            "Use it to select which fighter you are currently working on.\r\n" +
            "Any mods imported will be added as a mod for whichever character is selected here.\r\n" +
            "--TextureID Fix Slots--\r\n" +
            "Will check each active mod and change the Texture IDs of any conflicting mods.\r\n" +
            "Also happens automatically before a build if the setting is on (on by default)\r\n" +
            "--Check Mods for Errors--\r\n" +
            "Will do some simple checks on the mod files to see if there are any problems such as mismatched Texture IDs, duplicate TextureIDs, and missing model files.\r\n" +
            "NOTE: Character Slot Mods should have all required files(such as .mta, .vbn). Kami does not automatically add those when they are missing, and does not check if they are missing either(yet). If a Character Slot Mod is missing those files, the mod won't work on an added slot.";
        public const string HELP_CHARACTERMODS_SLOTMODS =
            "Slot Mods are mods which affect a single skin slot for the fighter.\r\n" +
            "Active Mods are currently turned on.\r\n" +
            "Use the arrows to activate/deactivate the mods and change which slot the mods are on.\r\n" +
            "Default Slots are blank slots.A mod being placed over a default slot will cause that default skin to disappear.\r\n" +
            "A mod can be moved from a default slot to a new slot by clicking the Double Down Arrow button.\r\n" +
            "--Icons--\r\n" +
            "- A red portrait icon will show if the mod doesn't have portrait data, or if it is missing it's Custom Name or Boxing Ring Text (if Use Custom Name is enabled on the mod)\r\n" +
            "- A green ?-block(Metal Box) icon will show if it is unknown if it has a working Metal Model. This is marked in the properties by YOU (or the mod maker). Builds can include/exclude Non-Metal-Model-Safe mods.\r\n" +
            "- A red !-block icon (Metal Box) will show if the Metal Model is known to have errors or crashes. Similar as above, this is marked by YOU (or the mod maker).\r\n" +
            "- A blue speaker icon will show if the mod has a Soundpack or Voicepack.\r\n" +
            "- A blue sign/name-tag icon will show if the mod is set to Use Custom Name. This makes the mod use it's custom nameplate and text in battles.\r\n" +
            "- A red signal icon will show if the mod has been marked by YOU(or the mod maker) as Non-Wifi-Safe. It can be marked safe/unsafe in the properties. Builds can include/exclude Non-Wifi-Safe mods.\r\n" +
            "- A red !-circle icon will show if the mod is missing. If the folder got renamed or deleted but the mod was still activated, you should deactivate it and reactivate it using the Arrow Buttons.\r\n";
        public const string HELP_CHARACTERMODS_GENERALMODS =
            "General Mods are mods which affect all skin slots for the fighter.\r\n" +
            "Active Mods are currently turned on.\r\n" +
            "Use the arrows to activate/deactivate the mods and change the priority.\r\n" +
            "--Icons--\r\n" +
            "- A red signal icon will show if the mod has been marked by YOU(or the mod maker) as Non-Wifi-Safe. It can be marked safe/unsafe in the properties. Builds can include/exclude Non-Wifi-Safe mods.\r\n" +
            "- A red !-circle icon will show if the mod is missing. If the folder got renamed or deleted but the mod was still activated, you should deactivate it and reactivate it using the Arrow Buttons.\r\n";
        public const string HELP_STAGEMODS_MODS =
            "Stage Mods will specifically be for either the Regular variant, or the Omega variant, not both. For a mod that alters both the Regular and Omega variants, you must import it as 2 separate mods.\r\n" +
            "Active Mods are currently turned on.\r\n" +
            "Use the arrows to activate/deactivate the mods\r\n" +
            "--Icons--\r\n" +
            "- A red signal icon will show if the mod has been marked by YOU(or the mod maker) as Non-Wifi-Safe. It can be marked safe/unsafe in the properties. Builds can include/exclude Non-Wifi-Safe mods.\r\n" +
            "- A red !-circle icon will show if the mod is missing. If the folder got renamed or deleted but the mod was still activated, you should deactivate it and reactivate it using the Arrow Buttons.\r\n";
        public const string HELP_GENERALMODS_MODS =
            "General Mods follow the same hierarchy as the Explorer, and can affect any file found in it.\r\n" +
            "Active Mods are currently turned on.\r\n" +
            "Use the arrows to activate/deactivate the mods and change the priority.\r\n" +
            "--Icons--\r\n" +
            "- A red signal icon will show if the mod has been marked by YOU(or the mod maker) as Non-Wifi-Safe. It can be marked safe/unsafe in the properties. Builds can include/exclude Non-Wifi-Safe mods.\r\n" +
            "- A red !-circle icon will show if the mod is missing. If the folder got renamed or deleted but the mod was still activated, you should deactivate it and reactivate it using the Arrow Buttons.\r\n";
    }
}
