using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBehavioursAndEvents
{
    class AlphaAnimals_Settings: ModSettings
        
    {

       
            public bool flagBlackHiveRaids = true;
            public bool flagStalkingLions = true;
            public bool flagCactipineDroppods = true;
            public bool flagSpiderClutchMothers = true;
            public bool flagAerofleets = true;
            public bool flagGallatross = true;
            public bool flagSummitCrab = true;







        public override void ExposeData()
            {
                base.ExposeData();
                Scribe_Values.Look(ref this.flagBlackHiveRaids, "flagBlackHiveRaids", true);
                Scribe_Values.Look(ref this.flagStalkingLions, "flagStalkingLions", true);
                Scribe_Values.Look(ref this.flagCactipineDroppods, "flagCactipineDroppods", true);
                Scribe_Values.Look(ref this.flagSpiderClutchMothers, "flagSpiderClutchMothers", true);
                Scribe_Values.Look(ref this.flagAerofleets, "flagAerofleets", true);
                Scribe_Values.Look(ref this.flagGallatross, "flagGallatross", true);
                Scribe_Values.Look(ref this.flagSummitCrab, "flagSummitCrab", true);


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
            ls.CheckboxLabeled("allowCactipineDroppods".Translate(), ref settings.flagCactipineDroppods, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowSpiderClutchMothers".Translate(), ref settings.flagSpiderClutchMothers, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowAerofleet".Translate(), ref settings.flagAerofleets, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowGallatross".Translate(), ref settings.flagGallatross, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowSummitCrab".Translate(), ref settings.flagSummitCrab, null);
            ls.Gap(12f);
            settings.Write();
            ls.End();


        }
    }
}
