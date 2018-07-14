Kami Modpack Builder
===========
Kami Modpack Builder is a fork of Sm4shExplorer built to expand it's functionality, and to improve the efficiency of Modpack Creation.
It can support it's own database of imported mods for Character Skins, Stages, etc. Mods can be enabled and disabled on the fly, skins can be moved around and have appropriate database files be fixed automatically. It keeps track of which mods are enabled to rebuild the patch file and pack folders appropriately. The explorer view of Sm4sh Explorer is also supported, but does not allow for any dynamic functionality.

This is still very much work in progress, and is not fully working yet even.

##What does it do?##
- Native Sm4shExplorer functionality
	- It will let you see the whole filesystem of Smash (main data + regions) in a treeview using [DTLSExtractor from Sammi-Husky](https://github.com/Sammi-Husky/Sm4sh-Tools/tree/master/DTLS). The treeview will show the updated version of the game (core files in black + patch files in blue)
	- Give you a few informations about any file (name, path, size, flags, source...)
	- Extract any file/folder to a specific "extract" folder
	- Replace/Add files. If a file is modified/added in a packed file, the whole packed file will be rebuilt automatically during export. A modified "mod file will appear in green.
	- For region folders, "unlocalize" a folder or a file so that the game loads the unlocalized (english) file instead.
	- Let you "remove" original resources from the game (experimental, works on stage models)
	- Repack files and rebuild resources and patchlist into a specific "export" folder.
- Supports multiple projects. Each project can point to it's own folders for the source game files, workspace, extract, and export folders.
- KamiMod common mod setup system
	- Supports character skins, general character mods (not pertaining to individual slots), stage mods, and misc mods.
	- Can be enabled, disabled, renamed, support adding notes, credits.
- Import folders and zip files to convert them into KamiMods by drag-dropping into appropriate windows.
- Automatic TextureID Fixing of character skins
- Automatic String Database Building for character skin names
- Automatic MTB Fixing for skin voice and sound packs
 
##What do I need?##
- An extration of the game on your computer (folders "content", "code" and "meta"). The will folder will have to remain untouched at all time to avoid issues.
- The latest patch, unmodified (current is v288, you need at least v208) in the same directory (so that "content" > "patch")
- On your SD: Backup your 'content/patch' folder before doing ANY CHANGE.
- Visual Studio 2015 and .NET Framework 4.5
- Libs zlib32.dll/zlib64.dll/zlibnet.dll/DTLS.exe for the main soft
- Libs libg719_decode.dll/libg7221_decode.dll/libmpg123-0.dll/libvorbis.dll/NAudio.dll and libvgmstream.dll from my repo for Sm4shMusic

##Future plans##
- CSS (Character Select Screen) editing tab
- SSS (Stage Select Screen) editing tab
- Integrating Sm4shMusic
- Other possible suggestions