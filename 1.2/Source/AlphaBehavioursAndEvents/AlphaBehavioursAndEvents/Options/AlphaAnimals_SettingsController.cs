using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;


namespace AlphaBehavioursAndEvents
{
   

 
    public class AlphaAnimalsEvents_Mod : Mod
    {

        
        public AlphaAnimalsEvents_Mod(ModContentPack content) : base(content)
        {
            GetSettings<AlphaAnimalsEvents_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals, Events";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            AlphaAnimalsEvents_Settings.DoWindowContents(inRect);
        }
    }

    public class AlphaAnimals_Mod : Mod
    {

        public static AlphaAnimals_Settings settings;

        public AlphaAnimals_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaAnimals_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            var toggleablespawndefs = DefDatabase<ToggleableSpawnDef>.AllDefsListForReading;
           
            foreach (var toggleablespawndef in toggleablespawndefs)
            {
                if (settings.pawnSpawnStates == null) settings.pawnSpawnStates = new Dictionary<string, bool>();
                foreach (string defName in toggleablespawndef.toggleablePawns)
                {
                    if (!settings.pawnSpawnStates.ContainsKey(defName))
                    {
                        settings.pawnSpawnStates[defName] = false;
                    }
                }
                    
            }

            settings.DoWindowContents(inRect);


        }
    }
}
