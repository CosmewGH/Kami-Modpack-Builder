Kami Modpack Builder
===========
Smash Bros. For Wii U modpack creation made simple!

Kami Modpack Builder is a (sort-of) fork of [Sm4shExplorer](https://github.com/Deinonychus71/Sm4shExplorer), built to expand its functionality, and to improve the efficiency of Modpack Creation.

Kami can support it's own database of imported mods for Character Skins, Stages, etc. Mods can be enabled and disabled on the fly, skins can be moved around and have appropriate database files be fixed automatically. It keeps track of which mods are enabled to rebuild the patch file and pack folders appropriately. The explorer view of Sm4sh Explorer is also supported, but does not allow for any dynamic functionality.

==What does it do?==
- Supports multiple projects. Each project can point to it's own folders for the source game files, workspace, extract, and export folders.
- KamiMod common mod setup system
	- Supports character skins, general character mods (not pertaining to individual slots), stage mods, and misc mods.
	- Can be enabled, disabled, renamed, support adding notes, credits.
- Import folders and zip files to convert them into KamiMods by drag-dropping into appropriate windows.
- Automatic TextureID Fixing of character skins
- Automatic String Database Building for character skin names
- Automatic MTB Fixing for skin voice and sound packs
- Built-in Sm4shMusic support to manage the list of musics and assign them to different stages (use [vgmstream from kode54](https://github.com/kode54/vgmstream)
- Native Sm4shExplorer functionality
	- It will let you see the whole filesystem of Smash (main data + regions) in a treeview using [DTLSExtractor from Sammi-Husky](https://github.com/Sammi-Husky/Sm4sh-Tools/tree/master/DTLS). The treeview will show the updated version of the game (core files in black + patch files in blue)
	- Give you a few informations about any file (name, path, size, flags, source...)
	- Extract any file/folder to a specific "extract" folder
	- Replace/Add files. If a file is modified/added in a packed file, the whole packed file will be rebuilt automatically during export. A modified "mod file will appear in green.
	- For region folders, "unlocalize" a folder or a file so that the game loads the unlocalized (english) file instead.
	- Let you "remove" original resources from the game (experimental, works on stage models)
	- Repack files and rebuild resources and patchlist into a specific "export" folder.
 
==What do I need?==
- An extraction of the game on your computer (folders "content", "code" and "meta"). The folder will have to remain untouched at all times to avoid issues.
- The latest patch, unmodified, in the same directory (so that "content" > "patch")
- On your SD: Backup your 'patch' folder before doing ANY CHANGE.
- Visual Studio 2015 and .NET Framework 4.5
- Libs zlib32.dll/zlib64.dll/zlibnet.dll/DTLS.exe for the main soft
- Libs libg719_decode.dll/libg7221_decode.dll/libmpg123-0.dll/libvorbis.dll/NAudio.dll and libvgmstream.dll from [this repo](https://github.com/Deinonychus71/vgmstream) for Sm4shMusic

==Future plans==
- Portrait previewing and automatic packing from .PNGs
- Help text to be littered around the application
- Build progress bar
- Other possible suggestions


Huge thanks to all the creators and contributors of Sm4shExplorer. Without them, this wouldn't be possible. I have simply stacked functionality on top of the huge codebase that was made by them. If you have contributed to any of the code used in this project and feel I haven't given proper credits/gotten permissions, please contact me. My intention is not to steal, but to improve modpack creation for myself and the community. If you have any suggestions or concerns, let me know as well.