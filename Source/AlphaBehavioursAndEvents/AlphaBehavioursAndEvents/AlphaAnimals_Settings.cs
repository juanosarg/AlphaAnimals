using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBehavioursAndEvents
{
    class AlphaAnimals_Settings: ModSettings
        
    {

       
            public bool flagBlackHiveRaids = true;
            public bool flagStalkingLions = true;


        public override void ExposeData()
            {
                base.ExposeData();
                Scribe_Values.Look(ref this.flagBlackHiveRaids, "flagBlackHiveRaids", true);
                Scribe_Values.Look(ref this.flagStalkingLions, "flagStalkingLions", true);

        }


    }
    class AlphaAnimals_Mod : Mod
    {
        public static AlphaAnimals_Settings settings;
        public AlphaAnimals_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaAnimals_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(inRect);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowBlackHive".Translate(), ref settings.flagBlackHiveRaids, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowStalkingLions".Translate(), ref settings.flagStalkingLions, null);
            ls.Gap(12f);
            settings.Write();
            ls.End();


        }
    }
}
