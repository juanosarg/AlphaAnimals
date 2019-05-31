using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBehavioursAndEvents
{
    class AlphaAnimals_Settings: ModSettings
        
    {
          

            public bool AA_AerofleetFlag = false;
            public bool AA_AnimusVoxFlag = false;
            public bool AA_ArcticLionFlag = false;
            public bool AA_BarbslingerFlag = false;
            public bool AA_BedBugFlag = false;
            public bool AA_BlizzariskFlag = false;
            public bool AA_BoulderMitFlag = false;
            public bool AA_BumbledroneFlag = false;
            public bool AA_CactipineFlag = false;
            public bool AA_DecayDrakeFlag = false;
            public bool AA_DunealiskFlag = false;
            public bool AA_FeraliskFlag = false;
            public bool AA_FissionMouseFlag = false;
            public bool AA_FrostmiteFlag = false;
            public bool AA_GigantelopeFlag = false;
            public bool AA_GreatDevourerFlag = false;
            public bool AA_GroundrunnerFlag = false;
            public bool AA_LockjawFlag = false;
            public bool AA_MammothWormFlag = false;
            public bool AA_MantrapFlag = false;
            public bool AA_MeadowAveFlag = false;
            public bool AA_MegaLouseFlag = false;
            public bool AA_NeedlepostFlag = false;
            public bool AA_NeedlerollFlag = false;
            public bool AA_NightlingFlag = false;
            public bool AA_OcularJellyFlag = false;
            public bool AA_OvergrownColossusFlag = false;
            public bool AA_PebbleMitFlag = false;
            public bool AA_RadyakFlag = false;
            public bool AA_RaptorShrimpFlag = false;
            public bool AA_SandLionFlag = false;
            public bool AA_SandSquidFlag = false;
            public bool AA_ShockGoatFlag = false;
            public bool AA_SlurrypedeFlag = false;
            public bool AA_TetraSlugFlag = false;
            public bool AA_ThermadonFlag = false;
            public bool AA_WildpawnFlag = false;
            public bool AA_WildpodFlag = false;
           

        public override void ExposeData()
            {
                base.ExposeData();
               
                Scribe_Values.Look(ref this.AA_AerofleetFlag, "AA_AerofleetFlag", false);
                Scribe_Values.Look(ref this.AA_AnimusVoxFlag, "AA_AnimusVoxFlag", false);
                Scribe_Values.Look(ref this.AA_ArcticLionFlag, "AA_ArcticLionFlag", false);
                Scribe_Values.Look(ref this.AA_BarbslingerFlag, "AA_BarbslingerFlag", false);
                Scribe_Values.Look(ref this.AA_BedBugFlag, "AA_BedBugFlag", false);
                Scribe_Values.Look(ref this.AA_BlizzariskFlag, "AA_BlizzariskFlag", false);
                Scribe_Values.Look(ref this.AA_BoulderMitFlag, "AA_BoulderMitFlag", false);
                Scribe_Values.Look(ref this.AA_BumbledroneFlag, "AA_BumbledroneFlag", false);
                Scribe_Values.Look(ref this.AA_CactipineFlag, "AA_CactipineFlag", false);
                Scribe_Values.Look(ref this.AA_DecayDrakeFlag, "AA_DecayDrakeFlag", false);
                Scribe_Values.Look(ref this.AA_DunealiskFlag, "AA_DunealiskFlag", false);
                Scribe_Values.Look(ref this.AA_FeraliskFlag, "AA_FeraliskFlag", false);
                Scribe_Values.Look(ref this.AA_FissionMouseFlag, "AA_FissionMouseFlag", false);
                Scribe_Values.Look(ref this.AA_FrostmiteFlag, "AA_FrostmiteFlag", false);
                Scribe_Values.Look(ref this.AA_GigantelopeFlag, "AA_GigantelopeFlag", false);
                Scribe_Values.Look(ref this.AA_GreatDevourerFlag, "AA_GreatDevourerFlag", false);
                Scribe_Values.Look(ref this.AA_GroundrunnerFlag, "AA_GroundrunnerFlag", false);
                Scribe_Values.Look(ref this.AA_LockjawFlag, "AA_LockjawFlag", false);
                Scribe_Values.Look(ref this.AA_MammothWormFlag, "AA_MammothWormFlag", false);
                Scribe_Values.Look(ref this.AA_MantrapFlag, "AA_MantrapFlag", false);
                Scribe_Values.Look(ref this.AA_MeadowAveFlag, "AA_MeadowAveFlag", false);
                Scribe_Values.Look(ref this.AA_MegaLouseFlag, "AA_MegaLouseFlag", false);
                Scribe_Values.Look(ref this.AA_NeedlepostFlag, "AA_NeedlepostFlag", false);
                Scribe_Values.Look(ref this.AA_NeedlerollFlag, "AA_NeedlerollFlag", false);
                Scribe_Values.Look(ref this.AA_NightlingFlag, "AA_NightlingFlag", false);
                Scribe_Values.Look(ref this.AA_OcularJellyFlag, "AA_OcularJellyFlag", false);
                Scribe_Values.Look(ref this.AA_OvergrownColossusFlag, "AA_OvergrownColossusFlag", false);
                Scribe_Values.Look(ref this.AA_PebbleMitFlag, "AA_PebbleMitFlag", false);
                Scribe_Values.Look(ref this.AA_RadyakFlag, "AA_RadyakFlag", false);
                Scribe_Values.Look(ref this.AA_RaptorShrimpFlag, "AA_RaptorShrimpFlag", false);
                Scribe_Values.Look(ref this.AA_SandLionFlag, "AA_SandLionFlag", false);
                Scribe_Values.Look(ref this.AA_SandSquidFlag, "AA_SandSquidFlag", false);
                Scribe_Values.Look(ref this.AA_ShockGoatFlag, "AA_ShockGoatFlag", false);
                Scribe_Values.Look(ref this.AA_SlurrypedeFlag, "AA_SlurrypedeFlag", false);
                Scribe_Values.Look(ref this.AA_TetraSlugFlag, "AA_TetraSlugFlag", false);
                Scribe_Values.Look(ref this.AA_ThermadonFlag, "AA_ThermadonFlag", false);
                Scribe_Values.Look(ref this.AA_WildpawnFlag, "AA_WildpawnFlag", false);
                Scribe_Values.Look(ref this.AA_WildpodFlag, "AA_WildpodFlag", false);


        }


    }

    class AlphaAnimalsEvents_Settings : ModSettings

    {


        public bool flagBlackHiveRaids = true;
        public bool flagStalkingLions = true;
        public bool flagCactipineDroppods = true;
        public bool flagSpiderClutchMothers = true;
        public bool flagAerofleets = true;
        public bool flagGallatross = true;
        public bool flagSummitCrab = true;
        public bool flagBumbledrones = true;
        public bool flagFrostmites = true;
        public bool flagAsteroids = true;


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
            Scribe_Values.Look(ref this.flagBumbledrones, "flagBumbledrones", true);
            Scribe_Values.Look(ref this.flagFrostmites, "flagFrostmites", true);
            Scribe_Values.Look(ref this.flagAsteroids, "flagAsteroid", true);

          

        }


    }

    class AlphaAnimalsEvents_Mod : Mod
    {

        public static AlphaAnimalsEvents_Settings settings;
        public AlphaAnimalsEvents_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaAnimalsEvents_Settings>();
        }
        public override string SettingsCategory() => "Alpha Animals, Events";

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
            ls.CheckboxLabeled("allowBumbledrones".Translate(), ref settings.flagBumbledrones, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowFrostmiteCorpses".Translate(), ref settings.flagFrostmites, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowAsteroids".Translate(), ref settings.flagAsteroids, null);
           
            settings.Write();
            ls.End();


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
            ls.ColumnWidth = inRect.width / 2.05f;
           
            ls.CheckboxLabeled("disableAerofleet".Translate(), ref settings.AA_AerofleetFlag, null);
           
            ls.CheckboxLabeled("disableAnimusVox".Translate(), ref settings.AA_AnimusVoxFlag, null);
           
            ls.CheckboxLabeled("disableArcticLion".Translate(), ref settings.AA_ArcticLionFlag, null);
         
            ls.CheckboxLabeled("disableBarbslinger".Translate(), ref settings.AA_BarbslingerFlag, null);
           
            ls.CheckboxLabeled("disableBedBug".Translate(), ref settings.AA_BedBugFlag, null);
           
            ls.CheckboxLabeled("disableBlizzarisk".Translate(), ref settings.AA_BlizzariskFlag, null);
            
            ls.CheckboxLabeled("disableBoulderMit".Translate(), ref settings.AA_BoulderMitFlag, null);
            
            ls.CheckboxLabeled("disableBumbledrone".Translate(), ref settings.AA_BumbledroneFlag, null);
           
            ls.CheckboxLabeled("disableCactipine".Translate(), ref settings.AA_CactipineFlag, null);
           
            ls.CheckboxLabeled("disableDecayDrake".Translate(), ref settings.AA_DecayDrakeFlag, null);
           
            ls.CheckboxLabeled("disableDunealisk".Translate(), ref settings.AA_DunealiskFlag, null);
            
            ls.CheckboxLabeled("disableFeralisk".Translate(), ref settings.AA_FeraliskFlag, null);
           
            ls.CheckboxLabeled("disableFissionMouse".Translate(), ref settings.AA_FissionMouseFlag, null);
            
            ls.CheckboxLabeled("disableFrostmite".Translate(), ref settings.AA_FrostmiteFlag, null);
           
            ls.CheckboxLabeled("disableGigantelope".Translate(), ref settings.AA_GigantelopeFlag, null);
            
            ls.CheckboxLabeled("disableGreatDevourer".Translate(), ref settings.AA_GreatDevourerFlag, null);
           
            ls.CheckboxLabeled("disableGroundrunner".Translate(), ref settings.AA_GroundrunnerFlag, null);
            
            ls.CheckboxLabeled("disableLockjaw".Translate(), ref settings.AA_LockjawFlag, null);
         
            ls.CheckboxLabeled("disableMammothWorm".Translate(), ref settings.AA_MammothWormFlag, null);
           
            ls.CheckboxLabeled("disableMantrap".Translate(), ref settings.AA_MantrapFlag, null);
            ls.NewColumn();
            ls.CheckboxLabeled("disableMeadowAve".Translate(), ref settings.AA_MeadowAveFlag, null);
           
            ls.CheckboxLabeled("disableMegaLouse".Translate(), ref settings.AA_MegaLouseFlag, null);
           
            ls.CheckboxLabeled("disableNeedlepost".Translate(), ref settings.AA_NeedlepostFlag, null);
            
            ls.CheckboxLabeled("disableNeedleroll".Translate(), ref settings.AA_NeedlerollFlag, null);
            
            ls.CheckboxLabeled("disableNightling".Translate(), ref settings.AA_NightlingFlag, null);
         
            ls.CheckboxLabeled("disableOcularJelly".Translate(), ref settings.AA_OcularJellyFlag, null);
           
            ls.CheckboxLabeled("disableOvergrownColossus".Translate(), ref settings.AA_OvergrownColossusFlag, null);
           
            ls.CheckboxLabeled("disablePebbleMit".Translate(), ref settings.AA_PebbleMitFlag, null);
           
            ls.CheckboxLabeled("disableRadyak".Translate(), ref settings.AA_RadyakFlag, null);
            
            ls.CheckboxLabeled("disableRaptorShrimp".Translate(), ref settings.AA_RaptorShrimpFlag, null);
            
            ls.CheckboxLabeled("disableSandLion".Translate(), ref settings.AA_SandLionFlag, null);
         
            ls.CheckboxLabeled("disableSandSquid".Translate(), ref settings.AA_SandSquidFlag, null);
            
            ls.CheckboxLabeled("disableShockGoat".Translate(), ref settings.AA_ShockGoatFlag, null);
           
            ls.CheckboxLabeled("disableSlurrypede".Translate(), ref settings.AA_SlurrypedeFlag, null);
            
            ls.CheckboxLabeled("disableTetraSlug".Translate(), ref settings.AA_TetraSlugFlag, null);
            
            ls.CheckboxLabeled("disableThermadon".Translate(), ref settings.AA_ThermadonFlag, null);
           
            ls.CheckboxLabeled("disableWildpawn".Translate(), ref settings.AA_WildpawnFlag, null);
        
            ls.CheckboxLabeled("disableWildpod".Translate(), ref settings.AA_WildpodFlag, null);
           




            settings.Write();
            ls.End();


        }
    }
}
