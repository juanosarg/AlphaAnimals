using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBehavioursAndEvents
{
    class SpawnFilterPatchUtility
    {
        public static bool ShouldBeFiltered(string defName)
        {
            if (defName == null)
                return false;

            if (defName == "AA_Skyeel" || defName == "AA_SkyeelRavenous") // Either this or additional switches for these
                return AlphaAnimalsEvents_Mod.settings.flagAsteroids;

            if (defName == "AA_FissionMouseSecond" || defName == "AA_FissionMouseThird")
                return AlphaAnimals_Mod.settings.pawnSpawnStates.ContainsKey("AA_FissionMouse");

            // Not sure if these are needed, but might as well
            if (defName == "AA_AcanthamoebaGiganteaLarge" || defName == "AA_AcanthamoebaGiganteaHuge")
                return AlphaAnimals_Mod.settings.pawnSpawnStates.ContainsKey("AA_AcanthamoebaGiganteaSmall");

            if (defName == "AA_Atispec")
                return AlphaAnimals_Mod.settings.pawnSpawnStates.ContainsKey("AA_LarvalAtispec");

            if (defName == "AA_ColossalAerofleet")
                return AlphaAnimals_Mod.settings.pawnSpawnStates.ContainsKey("AA_Aerofleet");

            // Only 'early' return for non-AA animals now, because the previous ones aren't in the dictionary and need special treatment
            if (!AlphaAnimals_Mod.settings.pawnSpawnStates.ContainsKey(defName))
                return false;

            return AlphaAnimals_Mod.settings.pawnSpawnStates[defName];
        }
    }
}
