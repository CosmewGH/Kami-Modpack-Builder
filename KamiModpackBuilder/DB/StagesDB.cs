using System;
using System.Collections.Generic;
using System.IO;

namespace KamiModpackBuilder.DB
{
    public class StagesDB
    {
        #region Members
        private static List<Stage> _StagesDB;
        private static string[] _StagesDBNames;
        #endregion

        #region Properties
        public static List<Stage> Stages { get { return _StagesDB; } }
        public static string[] StageNames { get { return _StagesDBNames; } }
        #endregion

        #region Main Methods
        internal static void InitializeStageDB(int gameVersion, bool isSwitch)
        {
            if (isSwitch)
                InitializeStagesSwitchDB(gameVersion);
            else
                InitializeStagesWiiUDB(gameVersion);
        }
        private static void InitializeStagesWiiUDB(int gameVersion)
        {
            if (gameVersion < 208)
                throw new Exception(string.Format("StageDB: Version {0} for Wii U not supported.", gameVersion));
            GenerateStagesDatabaseWiiU208();
        }
        private static void InitializeStagesSwitchDB(int gameVersion)
        {
            throw new Exception(string.Format("StageDB: Version {0} for Ultimate not supported.", gameVersion));
        }
        public static Stage GetStageFromID(int id)
        {
            for (int i = 0; i < _StagesDB.Count; ++i)
            {
                if (_StagesDB[i].ID == id) return _StagesDB[i];
            }
            return null;
        }
        #endregion

        #region Wii U
        private static void GenerateStagesDatabaseWiiU208()
        {
            _StagesDB = new List<Stage>
            {
                new Stage(0x0, "BattleField_f", "Battlefield", StageType.Melee),
                new Stage(0x1, "End_f", "Final Destination", StageType.Melee),
                new Stage(0x2, "Galaxy", "Mario Galaxy", StageType.Melee),
                new Stage(0x3, "MarioU", "Mushroom Kingdom U", StageType.Melee),
                new Stage(0x4, "MarioKart", "Mario Circuit", StageType.Melee),
                new Stage(0x5, "Kalos", "Kalos Pokémon League", StageType.Melee),
                new Stage(0x6, "Skyward", "Skyloft", StageType.Melee),
                new Stage(0x7, "DKReturns", "Jungle Hijinks", StageType.Melee),
                new Stage(0x8, "Metroid", "Pyrosphere", StageType.Melee),
                new Stage(0x9, "YoshiU", "Woolly World", StageType.Melee),
                new Stage(0xa, "Cave", "The Great Cabe Offensive", StageType.Melee),
                new Stage(0xb, "Village2", "Town and City", StageType.Melee),
                new Stage(0xc, "Assault", "Orbital Gate Assault", StageType.Melee),
                new Stage(0xd, "Colloseum_f", "Coliseum", StageType.Melee),
                new Stage(0xe, "PikminU", "Garden of Hope", StageType.Melee),
                new Stage(0xf, "Angeland", "Palutena's Temple", StageType.Melee),
                new Stage(0x10, "Gamer", "Gamer", StageType.Melee),
                new Stage(0x11, "DuckHunt", "Duck Hunt", StageType.Melee),
                new Stage(0x12, "Wrecking", "Wrecking Crew", StageType.Melee),
                new Stage(0x13, "XenoBlade", "Gaur Plain", StageType.Melee),
                new Stage(0x14, "PunchOut", "Boxing Ring", StageType.Melee),
                new Stage(0x15, "PunchOut2", "Boxing Ring 2", StageType.Melee),
                new Stage(0x16, "Wufu", "Wuhu Island", StageType.Melee),
                new Stage(0x17, "Miiverse", "Miiverse", StageType.Melee),
                new Stage(0x18, "WiiFit", "Wii Fit Studio", StageType.Melee),
                new Stage(0x19, "Pilotwings", "Pilotwings", StageType.Melee),
                new Stage(0x1a, "Windyhill", "Windy Hill Zone", StageType.Melee),
                new Stage(0x1b, "Pacland", "Pac-Land", StageType.Melee),
                new Stage(0x1c, "Wily", "Wily Castle", StageType.Melee),
                new Stage(0x1d, "Gw3", "Flatzone X", StageType.Melee),
                new Stage(0x1e, "XDolpic", "Delfino Plaza", StageType.Melee),
                new Stage(0x1f, "XKart", "Mario Circuit (Brawl)", StageType.Melee),
                new Stage(0x20, "XStadium", "Pokémon Stadium 2", StageType.Melee),
                new Stage(0x21, "XOldin", "Bridge of Eldin", StageType.Melee),
                new Stage(0x22, "64Jungle", "Kongo Jungle (64)", StageType.Melee),
                new Stage(0x23, "XNorfair", "Norfair", StageType.Melee),
                new Stage(0x24, "DxYorster", "Yoshi's Island", StageType.Melee),
                new Stage(0x25, "XHalberd", "Halberd", StageType.Melee),
                new Stage(0x26, "XVillage", "Smashville", StageType.Melee),
                new Stage(0x27, "XStarfox", "Lylat Cruise", StageType.Melee),
                new Stage(0x28, "XEmblem", "Castle Siege", StageType.Melee),
                new Stage(0x29, "XPalutena", "Skyworld", StageType.Melee),
                new Stage(0x2a, "XFzero", "Port Town Aero Dive", StageType.Melee),
                new Stage(0x2b, "DxOnett", "Onett", StageType.Melee),
                new Stage(0x2c, "XDonkey", "75m", StageType.Melee),
                new Stage(0x2d, "DxShrine", "Temple", StageType.Melee),
                new Stage(0x2e, "XMansion", "Luigi's Mansion", StageType.Melee),
                new Stage(0x2f, "BattleField_f", "Battlefield (Omega)", StageType.End),
                new Stage(0x30, "Galaxy", "Mario Galaxy (Omega)", StageType.End),
                new Stage(0x31, "MarioU", "Mushroom Kingdom U (Omega)", StageType.End),
                new Stage(0x32, "MarioKart", "Mario Circuit (Omega)", StageType.End),
                new Stage(0x33, "Kalos", "Kalos Pokémon League (Omega)", StageType.End),
                new Stage(0x34, "Skyward", "Skyloft (Omega)", StageType.End),
                new Stage(0x35, "DKReturns", "Jungle Hijinks (Omega)", StageType.End),
                new Stage(0x36, "Metroid", "Pyrosphere (Omega)", StageType.End),
                new Stage(0x37, "YoshiU", "Woolly World (Omega)", StageType.End),
                new Stage(0x38, "Cave", "The Great Cabe Offensive (Omega)", StageType.End),
                new Stage(0x39, "Village2", "Town and City (Omega)", StageType.End),
                new Stage(0x3a, "Assault", "Orbital Gate Assault (Omega)", StageType.End),
                new Stage(0x3b, "Colloseum_f", "Coliseum (Omega)", StageType.End),
                new Stage(0x3c, "PikminU", "Garden of Hope (Omega)", StageType.End),
                new Stage(0x3d, "Angeland", "Palutena's Temple (Omega)", StageType.End),
                new Stage(0x3e, "Gamer", "Gamer (Omega)", StageType.End),
                new Stage(0x3f, "DuckHunt", "Duck Hunt (Omega)", StageType.End),
                new Stage(0x40, "Wrecking", "Wrecking Crew (Omega)", StageType.End),
                new Stage(0x41, "XenoBlade", "Gaur Plain (Omega)", StageType.End),
                new Stage(0x42, "PunchOut", "Boxing Ring (Omega)", StageType.End),
                new Stage(0x43, "PunchOut2", "Boxing Ring 2 (Omega)", StageType.End),
                new Stage(0x44, "Wufu", "Wuhu Island (Omega)", StageType.End),
                new Stage(0x45, "Miiverse", "Miiverse (Omega)", StageType.End),
                new Stage(0x46, "WiiFit", "Wii Fit Studio (Omega)", StageType.End),
                new Stage(0x47, "Pilotwings", "Pilotwings (Omega)", StageType.End),
                new Stage(0x48, "Windyhill", "Windy Hill Zone (Omega)", StageType.End),
                new Stage(0x49, "Pacland", "Pac-Land (Omega)", StageType.End),
                new Stage(0x4a, "Wily", "Wily Castle (Omega)", StageType.End),
                new Stage(0x4b, "Gw3", "Flat Zone X (Omega)", StageType.End),
                new Stage(0x4c, "XDolpic", "Delfino Plaza (Omega)", StageType.End),
                new Stage(0x4d, "XKart", "Mario Circuit (Brawl) (Omega)", StageType.End),
                new Stage(0x4e, "XStadium", "Pokémon Stadium 2 (Omega)", StageType.End),
                new Stage(0x4f, "XOldin", "Bridge of Eldin (Omega)", StageType.End),
                new Stage(0x50, "64Jungle", "Kongo Jungle (64) (Omega)", StageType.End),
                new Stage(0x51, "XNorfair", "Norfair (Omega)", StageType.End),
                new Stage(0x52, "DxYorster", "Yoshi's Island (Omega)", StageType.End),
                new Stage(0x53, "XHalberd", "Halberd (Omega)", StageType.End),
                new Stage(0x54, "XVillage", "Smashville (Omega)", StageType.End),
                new Stage(0x55, "XStarfox", "Lylat Cruise (Omega)", StageType.End),
                new Stage(0x56, "XEmblem", "Castle Siege (Omega)", StageType.End),
                new Stage(0x57, "XPalutena", "Skyworld (Omega)", StageType.End),
                new Stage(0x58, "XFzero", "Port Town Aero Dive (Omega)", StageType.End),
                new Stage(0x59, "DxOnett", "Onett (Omega)", StageType.End),
                new Stage(0x5a, "XDonkey", "75m (Omega)", StageType.End),
                new Stage(0x5b, "DxShrine", "Temple (Omega)", StageType.End),
                new Stage(0x5c, "XMansion", "Luigi's Mansion (Omega)", StageType.End),
                new Stage(0x5d, "Battlefieldk_f", "Multi-Man Smash", StageType.Other),
                new Stage(0x5e, "Allstar_f", "All-Star Waiting Area", StageType.Other),
                new Stage(0x5f, "Homerun_f", "Homerun Contest", StageType.Other),
                new Stage(0x60, "Bomb_f", "Target Blast", StageType.Other),
                new Stage(0x61, "Rush_f", "Trophy Rush", StageType.Other),
                new Stage(0x62, "OnlineTraining_f", "Onling Waiting Room", StageType.Other),
                new Stage(0x63, "PrePlay_f", "Controls Testing Room", StageType.Other),
                new Stage(0x64, "StageEdit", "Custom Stage", StageType.Other),
                new Stage(0x65, "Playable_roll_f", "Playable_roll_f", StageType.Other),
                new Stage(0x66, "fig_get", "Trophy Studio Standard Night", StageType.Other),
                new Stage(0x67, "fig_disp_f", "Trophy Studio Standard Day", StageType.Fig),
                new Stage(0x68, "fig_photo1", "Trophy Studio Garden", StageType.Fig),
                new Stage(0x69, "fig_photo2", "Trophy Studio Target Blast", StageType.Fig),
                new Stage(0x6a, "fig_photo3", "Trophy Studio Apartment", StageType.Fig),
                new Stage(0x6b, "fig_photo4", "Trophy Studio Galleries", StageType.Fig),
                new Stage(0x6c, "BattleFieldL_f", "Big Battlefield", StageType.Melee),
                new Stage(0x6d, "RushL_f", "Trophy Rush", StageType.Other),
                new Stage(0x6e, "StreetFighter_f", "Suzaku Castle", StageType.Melee),
                new Stage(0x6f, "StreetFighter_f", "Suzaku Castle (Omega)", StageType.End),
                new Stage(0x70, "Pupupuland64_f", "Dream Land (64)", StageType.Melee),
                new Stage(0x71, "Pupupuland64_f", "Dream Land (64) (Omega)", StageType.End),
                new Stage(0x72, "PeachCastle64_f", "Peach's Castle (64)", StageType.Melee),
                new Stage(0x73, "PeachCastle64_f", "Peach's Castle (64) (Omega)", StageType.End),
                new Stage(0x74, "Hyrule64_f", "Hyrule Castle (64)", StageType.Melee),
                new Stage(0x75, "Hyrule64_f", "Hyrule Castle (64) (Omega)", StageType.End),
                new Stage(0x76, "MarioMaker_f", "Super Mario Maker", StageType.Melee),
                new Stage(0x77, "MarioMaker_f", "Super Mario Maker (Omega)", StageType.End),
                new Stage(0x78, "XPirates_f", "Pirate Ship", StageType.Melee),
                new Stage(0x79, "XPirates_f", "Pirate Ship (Omega)", StageType.End),
                new Stage(0x7a, "", "", StageType.Other),
                new Stage(0x7b, "", "", StageType.Other),
                new Stage(0x7c, "Midgar_f", "Midgar", StageType.Melee),
                new Stage(0x7d, "Midgar_f", "Midgar (Omega)", StageType.End),
                new Stage(0x7e, "Umbra_f", "Umbra Clock Tower", StageType.Melee),
                new Stage(0x7f, "Umbra_f", "Umbra Clock Tower (Omega)", StageType.End)
            };
            List<string>  names = new List<string>();
            foreach (Stage st in _StagesDB)
            {
                if (st.Type == StageType.End) names.Add("end" + Path.DirectorySeparatorChar + st.Label);
                if (st.Type == StageType.Melee) names.Add("melee" + Path.DirectorySeparatorChar + st.Label);
            }
            _StagesDBNames = names.ToArray();
        }
        #endregion
    }

    public enum StageType
    {
        Melee = 0,
        End = 1,
        Other = 3,
        Fig = 4
    }

    public class Stage
    {
        private int _StageID;
        private StageType _StageType;
        private string _StageLabel;
        private string _StageLabelHuman;

        public int ID { get { return _StageID; } }
        public StageType Type { get { return _StageType; } }
        public string Label { get { return _StageLabel; } }
        public string LabelHuman { get { return _StageLabelHuman; } }

        public Stage(int id, string label, string labelHuman, StageType type)
        {
            _StageID = id;
            _StageLabel = label;
            _StageLabelHuman = labelHuman;
            _StageType = type;
        }
    }
}
