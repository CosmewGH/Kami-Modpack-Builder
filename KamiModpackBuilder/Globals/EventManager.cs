using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiModpackBuilder.Globals
{
    static class EventManager
    {
        public delegate void EmptyDelegate();
        public delegate void ModRowDelegate(UserControls.ModRow modRowSelected);

        public static ModRowDelegate OnCharSlotModSelectionChanged = null;
        public static void CharSlotModSelectionChanged(UserControls.ModRow modRowSelected) { OnCharSlotModSelectionChanged?.Invoke(modRowSelected); }

        public static ModRowDelegate OnCharGeneralModSelectionChanged = null;
        public static void CharGeneralModSelectionChanged(UserControls.ModRow modRowSelected) { OnCharGeneralModSelectionChanged?.Invoke(modRowSelected); }

        public static ModRowDelegate OnStageModSelectionChanged = null;
        public static void StageModSelectionChanged(UserControls.ModRow modRowSelected) { OnStageModSelectionChanged?.Invoke(modRowSelected); }

        public static ModRowDelegate OnMiscModSelectionChanged = null;
        public static void MiscModSelectionChanged(UserControls.ModRow modRowSelected) { OnMiscModSelectionChanged?.Invoke(modRowSelected); }

    }
}
