using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace AlphaBehavioursAndEvents
{
   

 
    public class AlphaAnimalsEvents_Mod : Mod
    {
        public static AlphaAnimalsEvents_Settings settings;

        public AlphaAnimalsEvents_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaAnimalsEvents_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals, General Options";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }
    }

    public class AlphaAnimals_Mod : Mod
    {

        public static AlphaAnimals_Settings settings;

        public AlphaAnimals_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaAnimals_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals, Spawn Toggles";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            ToggleableSpawnDef toggleablespawndef = (from k in DefDatabase<ToggleableSpawnDef>.AllDefsListForReading
                                      where k.defName == "AA_ToggleableAnimals"
                                      select k).RandomElement();

            
                if (settings.pawnSpawnStates == null) settings.pawnSpawnStates = new Dictionary<string, bool>();
                foreach (string defName in toggleablespawndef.toggleablePawns)
                {
                    if (!settings.pawnSpawnStates.ContainsKey(defName) && DefDatabase<ThingDef>.GetNamedSilentFail(defName) != null)
                    {
                        settings.pawnSpawnStates[defName] = false;
                    }
                }
                    
            

            settings.DoWindowContents(inRect);


        }
    }
}
