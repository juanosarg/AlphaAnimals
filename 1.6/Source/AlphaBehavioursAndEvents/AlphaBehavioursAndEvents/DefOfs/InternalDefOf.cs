using System;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }

        public static PawnKindDef AA_BlackScarab;
        public static PawnKindDef AA_BlackSpelopede;
        public static PawnKindDef AA_BlackSpider;
        public static PawnKindDef AA_MammothWorm;
        public static PawnKindDef AA_MegaLouse;
        public static PawnKindDef AA_Ravager;
        public static PawnKindDef AA_Cactipine;
        public static PawnKindDef AA_FissionMouseSecond;
        public static PawnKindDef AA_FissionMouseThird;
        public static PawnKindDef AA_Aerofleet;
        public static PawnKindDef AA_ColossalAerofleet;
        public static PawnKindDef AA_Animalisk;
        public static PawnKindDef AA_BlizzariskClutchMother;
        public static PawnKindDef AA_DunealiskClutchMother;
        public static PawnKindDef AA_FeraliskClutchMother;
        public static PawnKindDef AA_Bumbledrone;
        public static PawnKindDef AA_BumbledroneQueen;
        public static PawnKindDef AA_FungalHusk;
        public static PawnKindDef AA_Gallatross;
        public static PawnKindDef AA_ArcticLion;
        public static PawnKindDef AA_SummitCrab;
        public static PawnKindDef AA_Skyeel;
        public static PawnKindDef AA_Eyeling;
        public static PawnKindDef AA_RedGoo;
        public static PawnKindDef AA_OcularJelly;
        public static PawnKindDef AA_OcularNightling;
        public static PawnKindDef AA_RedSpore;
        public static PawnKindDef AA_EngorgedTentacularAberration;
        public static PawnKindDef AA_UnblinkingEye;

        public static FactionDef AA_BlackHive;

        public static PawnKindDef AA_Behemoth;      

        public static WeatherDef DryThunderstorm; 

        public static ToggleableSpawnDef AA_ToggleableAnimals;
        public static ToggleableSpawnDef AA_VanillaAnimalToggles;

        public static ThingDef AA_CrescendoAnole;
        public static ThingDef AA_Heat_Ambrosia;
        public static ThingDef AA_AlienTree;
        public static ThingDef AA_AlienGrass;
        public static ThingDef AA_RedLeaves;
        public static ThingDef AA_RedPlantsTall;
        public static ThingDef AA_Filth_Slime;
        public static ThingDef AA_EggFlamingoPhoenixFertilized;
        public static ThingDef AA_FrostPuff;
        public static ThingDef AA_Filth_RedSlime;
        public static ThingDef AA_PedigreedRaptor;
        public static ThingDef AA_BlackHiveMound;
        public static ThingDef AA_Overgrown_DropPod;
        public static ThingDef AA_SkySteelClumps;
        public static ThingDef Gun_Autopistol;
        public static ThingDef Tornado;
        public static ThingDef BurnedTree;

        public static SoundDef AA_Rumbling;
        public static SoundDef AA_GooPop;
        public static SoundDef AA_IceCrash;
        public static SoundDef AA_MatureFleshbeastSwallow;
        public static SoundDef Hive_Spawn;

        public static DamageDef AA_BoulderScratch;
        public static DamageDef AA_AcidSpit;
        public static DamageDef AA_LuciferiumExplosion;

        public static RulePackDef AA_DamageEvent_Crushing;

        public static HediffDef AA_AnoleGrown;
        public static HediffDef AA_AnoleGrownExhausted;
        public static HediffDef AA_EatenACorpse;
        public static HediffDef AA_MimeHediff;
        public static HediffDef AA_InvisibleArcticLion;
        public static HediffDef AA_CrushingEverything;
        public static HediffDef AA_PsionicallyNuzzled;
        public static HediffDef AA_Kamikaze;
        public static HediffDef AA_AcidBuildup_AgainstMechs;

        public static FleckDef PsycastPsychicEffect;

        public static JobDef AA_PlantAnimaTreeJob;
        public static JobDef AA_PsionicNuzzle;

        public static IncidentDef AA_Eclipse;
        public static IncidentDef Flashstorm;

        public static ThingSetMakerDef AA_AlphaResourcePod;
        public static ThingSetMakerDef AA_Meteorite;

        public static ThoughtDef AA_BeenPsionicallyNuzzled;

        public static InteractionDef AA_PsionicNuzzleInteraction;

        public static WorkGiverDef Mine;

        public static BodyPartDef Body;

      
       
        [MayRequire("OskarPotocki.VFE.Insectoid2")]
        public static ThingDef VFEI2_BlackInsectoidCocoon;
       
        public static DamageDef AA_OverpoweringSecondaryAcidBurn;


    }
}
