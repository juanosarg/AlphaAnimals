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

        public static PawnKindDef AA_Behemoth;      

        public static WeatherDef DryThunderstorm;

        public static ToggleableSpawnDef AA_ToggleableAnimals;
        public static ToggleableSpawnDef AA_VanillaAnimalToggles;

        public static ThingDef AA_CrescendoAnole;


    }
}
