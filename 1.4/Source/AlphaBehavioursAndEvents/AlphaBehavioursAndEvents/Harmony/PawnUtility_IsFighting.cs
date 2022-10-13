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
        [HarmonyPostfix]
        public static void DisableBlackHive(Pawn pawn, ref bool __result)

        {
            if(pawn!=null && (pawn.def.defName.Contains("AA_Black")|| pawn.def.defName== "AA_MegaLouse" || pawn.def.defName == "AA_MammothWorm") && pawn.CurJob != null) { __result = true; }
                


        }
    }
}
