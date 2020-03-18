using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBehavioursAndEvents.Settings
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
      
        
        public AlphaAnimals_Mod(ModContentPack content) : base(content)
        {
           GetSettings<AlphaAnimals_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            AlphaAnimals_Settings.DoWindowContents(inRect);


        }
    }
}
