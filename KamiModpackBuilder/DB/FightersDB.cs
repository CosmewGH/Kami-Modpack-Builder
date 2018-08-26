using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiModpackBuilder.DB
{
    public class FightersDB
    {
        #region Members
        private static List<Fighter> _FightersDB;
        #endregion

        #region Properties
        public static List<Fighter> Fighters { get { return _FightersDB; } }
        #endregion

        #region Main Methods
        internal static void InitializeFightersDB(int gameVersion, bool isSwitch)
        {
            if (isSwitch)
                InitializeFightersSwitchDB(gameVersion);
            else
                InitializeFightersWiiUDB(gameVersion);
        }
        private static void InitializeFightersWiiUDB(int gameVersion)
        {
            if (gameVersion < 208)
                throw new Exception(string.Format("FightersDB: Version {0} for Wii U not supported.", gameVersion));
            _FightersDB = GenerateFightersDatabaseWiiU208();
        }
        private static void InitializeFightersSwitchDB(int gameVersion)
        {
            throw new Exception(string.Format("FightersDB: Version {0} for Ultimate not supported.", gameVersion));
        }
        public static Fighter GetFighterFromID(int id)
        {
            for (int i = 0; i < _FightersDB.Count; ++i)
            {
                if (_FightersDB[i].id == id) return _FightersDB[i];
            }
            return null;
        }
        public static Fighter GetFighterFromName(string name)
        {
            for (int i = 0; i < _FightersDB.Count; ++i)
            {
                if (_FightersDB[i].name.Equals(name)) return _FightersDB[i];
            }
            return null;
        }
        #endregion

        #region Wii U
        private static List<Fighter> GenerateFightersDatabaseWiiU208()
        {
            List<Fighter> fighters = new List<Fighter>
            {
                new Fighter(0x03, "Mario", "Mario"),
                new Fighter(0x04, "Donkey", "Donkey Kong", false, Fighter.LowPolySlots.All),
                new Fighter(0x05, "Link", "Link", false, Fighter.LowPolySlots.All),
                new Fighter(0x06, "Samus", "Samus", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","gun" }),
                new Fighter(0x07, "Yoshi", "Yoshi", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","tamago" }),
                new Fighter(0x08, "Kirby", "Kirby", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","fitkirbydonkey","fitkirbyfalco","fitkirbypacman","fitkirbypikmin","fitkirbywiifit","mewtwocap" }),
                new Fighter(0x09, "Fox", "Fox", false, Fighter.LowPolySlots.All),
                new Fighter(0x0A, "Pikachu", "Pikachu"),
                new Fighter(0x0B, "Luigi", "Luigi"),
                new Fighter(0x0C, "Captain", "Captain Falcon", false, Fighter.LowPolySlots.All),
                new Fighter(0x0D, "Ness", "Ness", false, Fighter.LowPolySlots.All),
                new Fighter(0x0E, "Peach", "Peach", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","kassar" }),
                new Fighter(0x0F, "Koopa", "Bowser"),
                new Fighter(0x10, "Zelda", "Zelda", false, Fighter.LowPolySlots.All),
                new Fighter(0x11, "Sheik", "Sheik", false, Fighter.LowPolySlots.All),
                new Fighter(0x12, "Marth", "Marth", false, Fighter.LowPolySlots.All),
                new Fighter(0x13, "Gamewatch", "Mr. Game & Watch", false, Fighter.LowPolySlots.None,Fighter.SoundPackSlots.All,Fighter.VoicePackSlots.All,16,8,1,null,new List<string> { "body","breath","entry","fire","fish","friedprawns","kame","manhole","octopus","oil","panel","parachute","rescue","sausage","spray","steak","tropicalfish" }),
                new Fighter(0x14, "Ganon", "Ganondorf", false, Fighter.LowPolySlots.All),
                new Fighter(0x15, "Falco", "Falco", false, Fighter.LowPolySlots.All),
                new Fighter(0x16, "Wario", "Wario", false, Fighter.LowPolySlots.All),
                new Fighter(0x17, "Metaknight", "Meta Knight", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","mantle" }),
                new Fighter(0x18, "Pit", "Pit", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","three" }),
                new Fighter(0x19, "Szerosuit", "Zero Suit Samus", false, Fighter.LowPolySlots.All),
                new Fighter(0x1A, "Pikmin", "Olimar", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.Two, 16, 8, 2, new Dictionary<int, int> { [4] = 1, [5] = 1, [6] = 1, [7] = 1 }),
                new Fighter(0x1B, "Diddy", "Diddy Kong", false, Fighter.LowPolySlots.All),
                new Fighter(0x1C, "Dedede", "Dedede"),
                new Fighter(0x1D, "Ike", "Ike", false, Fighter.LowPolySlots.All),
                new Fighter(0x1E, "Lucario", "Lucario"),
                new Fighter(0x1F, "Robot", "R.O.B.", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 8, 8, 1, null, new List<string>{ "body","gyro","gyroholder" }),
                new Fighter(0x20, "Toonlink", "Toon Link", false, Fighter.LowPolySlots.All),
                new Fighter(0x21, "Lizardon", "Charizard"),
                new Fighter(0x22, "Sonic", "Sonic"),
                new Fighter(0x23, "Purin", "Jigglypuff"),
                new Fighter(0x24, "Drmario", "Dr. Mario", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string>{ "body","stethoscope" }),
                new Fighter(0x25, "Lucina", "Lucina", false, Fighter.LowPolySlots.All),
                new Fighter(0x26, "Pitb", "Dark Pit", false, Fighter.LowPolySlots.All),
                new Fighter(0x27, "Rosetta", "Rosalina & Luma", false, Fighter.LowPolySlots.All),
                new Fighter(0x28, "Wiifit", "Wii Fit Trainer", false, Fighter.LowPolySlots.All),
                new Fighter(0x29, "Littlemac", "Little Mac", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.Two, 16, 16, 1, null, new List<string>{ "body","championbelt","throwsweat" }),
                new Fighter(0x2A, "Murabito", "Villager", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 2, new Dictionary<int, int> { [1] = 1, [3] = 1, [5] = 1, [7] = 1 }),
                new Fighter(0x2B, "Palutena", "Palutena", false, Fighter.LowPolySlots.All),
                new Fighter(0x2C, "Reflet", "Robin", false, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.Two),
                new Fighter(0x2D, "Duckhunt", "Dunk Hunt"),
                new Fighter(0x2E, "KoopaJr", "Bowser Jr.", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 8, new Dictionary<int, int> { [0] = 0, [1] = 1, [2] = 2, [3] = 3, [4] = 4, [5] = 5, [6] = 6, [7] = 7 }, new List<string> { "body","kart","remainclown" }),
                new Fighter(0x2F, "Shulk", "Shulk", false, Fighter.LowPolySlots.All),
                new Fighter(0x30, "Gekkouga", "Greninja"),
                new Fighter(0x31, "Pacman", "Pac-man"),
                new Fighter(0x32, "Rockman", "Mega Man", false, Fighter.LowPolySlots.None, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.All, 16, 8, 1, null, new List<string> { "body","hardknuckle","leftarm","rightarm" }),
                new Fighter(0x33, "Mewtwo", "Mewtwo", true),
                new Fighter(0x34, "Ryu", "Ryu", true, Fighter.LowPolySlots.All),
                new Fighter(0x35, "Lucas", "Lucas", true, Fighter.LowPolySlots.All),
                new Fighter(0x36, "Roy", "Roy", true, Fighter.LowPolySlots.All),
                new Fighter(0x37, "Cloud", "Cloud", true, Fighter.LowPolySlots.EvenSlots, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.Two),
                new Fighter(0x38, "Bayonetta", "Bayonetta", true, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.Two),
                new Fighter(0x39, "Kamui", "Corrin", true, Fighter.LowPolySlots.All, Fighter.SoundPackSlots.All, Fighter.VoicePackSlots.Two, 16, 8, 1, null, new List<string> { "body","dragonhand","spearhand","waterdragon" })
            };

            return fighters;
        }
        #endregion
    }

    public class Fighter
    {
        #region Members
        private int _ID = 0x0;
        private string _Name = "";
        private string _NameHuman = "";
        private LowPolySlots _LowPolySlots = LowPolySlots.None;
        private SoundPackSlots _SoundPackSlots = SoundPackSlots.All;
        private VoicePackSlots _VoicePackSlots = VoicePackSlots.All;
        private int _MaxSlots = 16;
        private int _DefaultSlots = 8;
        private int _DefaultNameplateSlots = 1;
        private Dictionary<int, int> _NameplateSlots = null;
        private bool _IsDLC = false;
        private List<string> _ModelParts = null;
        #endregion

        #region Properties
        public int id { get { return _ID; } }
        public string name { get { return _Name; } }
        public string nameHuman { get { return _NameHuman; } }
        public LowPolySlots lowPolySlots { get { return _LowPolySlots; } }
        public SoundPackSlots soundPackSlots { get { return _SoundPackSlots; } }
        public VoicePackSlots voicePackSlots { get { return _VoicePackSlots; } }
        public int maxSlots { get { return _MaxSlots; } }
        public int defaultSlots { get { return _DefaultSlots; } }
        public int defaultNameplateSlots { get { return _DefaultNameplateSlots; } }
        public Dictionary<int, int> nameplateSlots { get { return _NameplateSlots; } }
        public bool isDLC { get { return _IsDLC; } }
        public List<string> modelParts { get { return _ModelParts; } }
        #endregion

        #region Types
        public enum LowPolySlots { None, All, EvenSlots, OddSlots}
        public enum SoundPackSlots { All, One, Two }
        public enum VoicePackSlots { All, One, Two }
        #endregion

        public Fighter(int a_id, string a_name, string a_nameHuman, bool a_isDLC = false, 
            LowPolySlots a_lowPolySlots = LowPolySlots.None, SoundPackSlots a_soundPackSlots = SoundPackSlots.All, 
            VoicePackSlots a_voicePackSlots = VoicePackSlots.All, int a_maxSlots = 16, int a_defaultSlots = 8, 
            int a_defaultNameplateSlots = 1, Dictionary<int, int> a_nameplateSlots = null, List<string> a_modelParts = null)
        {
            _ID = a_id;

            _Name = a_name;
            _NameHuman = a_nameHuman;
            _LowPolySlots = a_lowPolySlots;
            _SoundPackSlots = a_soundPackSlots;
            _VoicePackSlots = a_voicePackSlots;
            _MaxSlots = a_maxSlots;
            _DefaultSlots = a_defaultSlots;
            _DefaultNameplateSlots = a_defaultNameplateSlots;
            _NameplateSlots = a_nameplateSlots;
            _IsDLC = a_isDLC;
            _ModelParts = a_modelParts;

            if (_NameplateSlots == null) _NameplateSlots = new Dictionary<int, int>
            {
                [0] = 0
            };
            if (_ModelParts == null) _ModelParts = new List<string>
            {
                "body"
            };
        }
    }
}
