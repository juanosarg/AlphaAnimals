using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaBehavioursAndEvents
{
    
    [HarmonyPatch(typeof(PawnUtility))]
    [HarmonyPatch("IsFighting")]
   
    public static class AlphaAnimals_PawnUtility_IsFighting_Patch
    {

        public static List<PawnKindDef> blachHive = new List<PawnKindDef>() { InternalDefOf.AA_BlackScarab,InternalDefOf.AA_BlackSpelopede,
        InternalDefOf.AA_BlackSpider,InternalDefOf.AA_MegaLouse,InternalDefOf.AA_MammothWorm};


        [HarmonyPostfix]
        public static void DisableBlackHive(Pawn pawn, ref bool __result)

        {
            if(pawn!=null && blachHive.Contains(pawn.kindDef) && pawn.CurJob != null) { __result = true; }
                


        }
    }
}
