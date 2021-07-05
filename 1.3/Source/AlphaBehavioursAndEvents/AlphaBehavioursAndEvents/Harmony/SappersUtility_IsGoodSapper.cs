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
    /*This Harmony Postfix allows Demolishers to use the sapper AI
    */
    [HarmonyPatch(typeof(SappersUtility))]
    [HarmonyPatch("IsGoodSapper")]
    public static class AlphaAnimals_SappersUtility_IsGoodSapper_Patch
    {
        [HarmonyPostfix]
        public static void DemolisherIsAGoodSapper(Pawn p, ref bool __result)

        {
           if (p.def.defName== "AA_Demolisher") {
                __result = true;
            }
            
        }
    }
}