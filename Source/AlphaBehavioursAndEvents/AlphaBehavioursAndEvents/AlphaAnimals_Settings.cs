using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBehavioursAndEvents
{
    public class AlphaAnimals_Settings: ModSettings
        
    {
          

            public static bool AA_AerofleetFlag = false;
            public static bool AA_AnimusVoxFlag = false;
            public static bool AA_ArcticLionFlag = false;
            public static bool AA_BarbslingerFlag = false;
            public static bool AA_BedBugFlag = false;
            public static bool AA_BlizzariskFlag = false;
            public static bool AA_BoulderMitFlag = false;
            public static bool AA_BumbledroneFlag = false;
            public static bool AA_CactipineFlag = false;
            public static bool AA_DecayDrakeFlag = false;
            public static bool AA_DunealiskFlag = false;
            public static bool AA_FeraliskFlag = false;
            public static bool AA_FissionMouseFlag = false;
            public static bool AA_FrostmiteFlag = false;
            public static bool AA_GigantelopeFlag = false;
            public static bool AA_GreatDevourerFlag = false;
            public static bool AA_GroundrunnerFlag = false;
            public static bool AA_LockjawFlag = false;
            public static bool AA_MammothWormFlag = false;
            public static bool AA_MantrapFlag = false;
            public static bool AA_MeadowAveFlag = false;
            public static bool AA_MegaLouseFlag = false;
            public static bool AA_NeedlepostFlag = false;
            public static bool AA_NeedlerollFlag = false;
            public static bool AA_NightlingFlag = false;
            public static bool AA_OcularJellyFlag = false;
            public static bool AA_OvergrownColossusFlag = false;
            public static bool AA_PebbleMitFlag = false;
            public static bool AA_RadyakFlag = false;
            public static bool AA_RaptorShrimpFlag = false;
            public static bool AA_SandLionFlag = false;
            public static bool AA_SandSquidFlag = false;
            public static bool AA_ShockGoatFlag = false;
            public static bool AA_SlurrypedeFlag = false;
            public static bool AA_TetraSlugFlag = false;
            public static bool AA_ThermadonFlag = false;
            public static bool AA_WildpawnFlag = false;
            public static bool AA_WildpodFlag = false;
           

        public override void ExposeData()
            {
                base.ExposeData();
               
            Scribe_Values.Look(ref AA_AerofleetFlag, "AA_AerofleetFlag", false, true);
            Scribe_Values.Look(ref AA_AnimusVoxFlag, "AA_AnimusVoxFlag", false, true);
            Scribe_Values.Look(ref AA_ArcticLionFlag, "AA_ArcticLionFlag", false, true);
            Scribe_Values.Look(ref AA_BarbslingerFlag, "AA_BarbslingerFlag", false, true);
            Scribe_Values.Look(ref AA_BedBugFlag, "AA_BedBugFlag", false, true);
            Scribe_Values.Look(ref AA_BlizzariskFlag, "AA_BlizzariskFlag", false, true);
            Scribe_Values.Look(ref AA_BoulderMitFlag, "AA_BoulderMitFlag", false, true);
            Scribe_Values.Look(ref AA_BumbledroneFlag, "AA_BumbledroneFlag", false, true);
            Scribe_Values.Look(ref AA_CactipineFlag, "AA_CactipineFlag", false, true);
            Scribe_Values.Look(ref AA_DecayDrakeFlag, "AA_DecayDrakeFlag", false, true);
            Scribe_Values.Look(ref AA_DunealiskFlag, "AA_DunealiskFlag", false, true);
            Scribe_Values.Look(ref AA_FeraliskFlag, "AA_FeraliskFlag", false, true);
            Scribe_Values.Look(ref AA_FissionMouseFlag, "AA_FissionMouseFlag", false, true);
            Scribe_Values.Look(ref AA_FrostmiteFlag, "AA_FrostmiteFlag", false, true);
            Scribe_Values.Look(ref AA_GigantelopeFlag, "AA_GigantelopeFlag", false, true);
            Scribe_Values.Look(ref AA_GreatDevourerFlag, "AA_GreatDevourerFlag", false, true);
            Scribe_Values.Look(ref AA_GroundrunnerFlag, "AA_GroundrunnerFlag", false, true);
            Scribe_Values.Look(ref AA_LockjawFlag, "AA_LockjawFlag", false, true);
            Scribe_Values.Look(ref AA_MammothWormFlag, "AA_MammothWormFlag", false, true);
            Scribe_Values.Look(ref AA_MantrapFlag, "AA_MantrapFlag", false, true);
            Scribe_Values.Look(ref AA_MeadowAveFlag, "AA_MeadowAveFlag", false, true);
            Scribe_Values.Look(ref AA_MegaLouseFlag, "AA_MegaLouseFlag", false, true);
            Scribe_Values.Look(ref AA_NeedlepostFlag, "AA_NeedlepostFlag", false, true);
            Scribe_Values.Look(ref AA_NeedlerollFlag, "AA_NeedlerollFlag", false, true);
            Scribe_Values.Look(ref AA_NightlingFlag, "AA_NightlingFlag", false, true);
            Scribe_Values.Look(ref AA_OcularJellyFlag, "AA_OcularJellyFlag", false, true);
            Scribe_Values.Look(ref AA_OvergrownColossusFlag, "AA_OvergrownColossusFlag", false, true);
            Scribe_Values.Look(ref AA_PebbleMitFlag, "AA_PebbleMitFlag", false, true);
            Scribe_Values.Look(ref AA_RadyakFlag, "AA_RadyakFlag", false, true);
            Scribe_Values.Look(ref AA_RaptorShrimpFlag, "AA_RaptorShrimpFlag", false, true);
            Scribe_Values.Look(ref AA_SandLionFlag, "AA_SandLionFlag", false, true);
            Scribe_Values.Look(ref AA_SandSquidFlag, "AA_SandSquidFlag", false, true);
            Scribe_Values.Look(ref AA_ShockGoatFlag, "AA_ShockGoatFlag", false, true);
            Scribe_Values.Look(ref AA_SlurrypedeFlag, "AA_SlurrypedeFlag", false, true);
            Scribe_Values.Look(ref AA_TetraSlugFlag, "AA_TetraSlugFlag", false, true);
            Scribe_Values.Look(ref AA_ThermadonFlag, "AA_ThermadonFlag", false, true);
            Scribe_Values.Look(ref AA_WildpawnFlag, "AA_WildpawnFlag", false, true);
            Scribe_Values.Look(ref AA_WildpodFlag, "AA_WildpodFlag", false, true);


        }
        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.ColumnWidth = inRect.width / 2.05f;

            ls.CheckboxLabeled("disableAerofleet".Translate(), ref AA_AerofleetFlag, null);

            ls.CheckboxLabeled("disableAnimusVox".Translate(), ref AA_AnimusVoxFlag, null);

            ls.CheckboxLabeled("disableArcticLion".Translate(), ref AA_ArcticLionFlag, null);

            ls.CheckboxLabeled("disableBarbslinger".Translate(), ref AA_BarbslingerFlag, null);

            ls.CheckboxLabeled("disableBedBug".Translate(), ref AA_BedBugFlag, null);

            ls.CheckboxLabeled("disableBlizzarisk".Translate(), ref AA_BlizzariskFlag, null);

            ls.CheckboxLabeled("disableBoulderMit".Translate(), ref AA_BoulderMitFlag, null);

            ls.CheckboxLabeled("disableBumbledrone".Translate(), ref AA_BumbledroneFlag, null);

            ls.CheckboxLabeled("disableCactipine".Translate(), ref AA_CactipineFlag, null);

            ls.CheckboxLabeled("disableDecayDrake".Translate(), ref AA_DecayDrakeFlag, null);

            ls.CheckboxLabeled("disableDunealisk".Translate(), ref AA_DunealiskFlag, null);

            ls.CheckboxLabeled("disableFeralisk".Translate(), ref AA_FeraliskFlag, null);

            ls.CheckboxLabeled("disableFissionMouse".Translate(), ref AA_FissionMouseFlag, null);

            ls.CheckboxLabeled("disableFrostmite".Translate(), ref AA_FrostmiteFlag, null);

            ls.CheckboxLabeled("disableGigantelope".Translate(), ref AA_GigantelopeFlag, null);

            ls.CheckboxLabeled("disableGreatDevourer".Translate(), ref AA_GreatDevourerFlag, null);

            ls.CheckboxLabeled("disableGroundrunner".Translate(), ref AA_GroundrunnerFlag, null);

            ls.CheckboxLabeled("disableLockjaw".Translate(), ref AA_LockjawFlag, null);

            ls.CheckboxLabeled("disableMammothWorm".Translate(), ref AA_MammothWormFlag, null);

            ls.CheckboxLabeled("disableMantrap".Translate(), ref AA_MantrapFlag, null);
            ls.NewColumn();
            ls.CheckboxLabeled("disableMeadowAve".Translate(), ref AA_MeadowAveFlag, null);

            ls.CheckboxLabeled("disableMegaLouse".Translate(), ref AA_MegaLouseFlag, null);

            ls.CheckboxLabeled("disableNeedlepost".Translate(), ref AA_NeedlepostFlag, null);

            ls.CheckboxLabeled("disableNeedleroll".Translate(), ref AA_NeedlerollFlag, null);

            ls.CheckboxLabeled("disableNightling".Translate(), ref AA_NightlingFlag, null);

            ls.CheckboxLabeled("disableOcularJelly".Translate(), ref AA_OcularJellyFlag, null);

            ls.CheckboxLabeled("disableOvergrownColossus".Translate(), ref AA_OvergrownColossusFlag, null);

            ls.CheckboxLabeled("disablePebbleMit".Translate(), ref AA_PebbleMitFlag, null);

            ls.CheckboxLabeled("disableRadyak".Translate(), ref AA_RadyakFlag, null);

            ls.CheckboxLabeled("disableRaptorShrimp".Translate(), ref AA_RaptorShrimpFlag, null);

            ls.CheckboxLabeled("disableSandLion".Translate(), ref AA_SandLionFlag, null);

            ls.CheckboxLabeled("disableSandSquid".Translate(), ref AA_SandSquidFlag, null);

            ls.CheckboxLabeled("disableShockGoat".Translate(), ref AA_ShockGoatFlag, null);

            ls.CheckboxLabeled("disableSlurrypede".Translate(), ref AA_SlurrypedeFlag, null);

            ls.CheckboxLabeled("disableTetraSlug".Translate(), ref AA_TetraSlugFlag, null);

            ls.CheckboxLabeled("disableThermadon".Translate(), ref AA_ThermadonFlag, null);

            ls.CheckboxLabeled("disableWildpawn".Translate(), ref AA_WildpawnFlag, null);

            ls.CheckboxLabeled("disableWildpod".Translate(), ref AA_WildpodFlag, null);

            ls.End();

        }




    }

    public class AlphaAnimalsEvents_Settings : ModSettings

    {


        public static bool flagBlackHiveRaids = true;
        public static bool flagStalkingLions = true;
        public static bool flagCactipineDroppods = true;
        public static bool flagSpiderClutchMothers = true;
        public static bool flagAerofleets = true;
        public static bool flagGallatross = true;
        public static bool flagSummitCrab = true;
        public static bool flagBumbledrones = true;
        public static bool flagFrostmites = true;
        public static bool flagAsteroids = true;


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref flagBlackHiveRaids, "flagBlackHiveRaids", true,true);
            Scribe_Values.Look(ref flagStalkingLions, "flagStalkingLions", true, true);
            Scribe_Values.Look(ref flagCactipineDroppods, "flagCactipineDroppods", true, true);
            Scribe_Values.Look(ref flagSpiderClutchMothers, "flagSpiderClutchMothers", true, true);
            Scribe_Values.Look(ref flagAerofleets, "flagAerofleets", true, true);
            Scribe_Values.Look(ref flagGallatross, "flagGallatross", true, true);
            Scribe_Values.Look(ref flagSummitCrab, "flagSummitCrab", true, true);
            Scribe_Values.Look(ref flagBumbledrones, "flagBumbledrones", true, true);
            Scribe_Values.Look(ref flagFrostmites, "flagFrostmites", true, true);
            Scribe_Values.Look(ref flagAsteroids, "flagAsteroid", true, true);



        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowBlackHive".Translate(), ref flagBlackHiveRaids, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowStalkingLions".Translate(), ref flagStalkingLions, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowCactipineDroppods".Translate(), ref flagCactipineDroppods, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowSpiderClutchMothers".Translate(), ref flagSpiderClutchMothers, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowAerofleet".Translate(), ref flagAerofleets, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowGallatross".Translate(), ref flagGallatross, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowSummitCrab".Translate(), ref flagSummitCrab, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowBumbledrones".Translate(), ref flagBumbledrones, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowFrostmiteCorpses".Translate(), ref flagFrostmites, null);
            ls.Gap(12f);
            ls.CheckboxLabeled("allowAsteroids".Translate(), ref flagAsteroids, null);

            ls.End();
        }



      }


        


   
            

        
    
}
