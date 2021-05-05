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
    /*This Harmony Postfix multiplies commonality of animals in the biome
    */
    [HarmonyPatch(typeof(IncidentWorker_Disease))]
    [HarmonyPatch("CanAddHediffToAnyPartOfDef")]
    public static class AlphaAnimals_IncidentWorker_Disease_CanAddHediffToAnyPartOfDef_Patch
    {
        [HarmonyPostfix]
        public static void DontApplyDiseasesToMechsPlease(Pawn pawn, ref bool __result)

        {

            if (pawn.def.defName.Contains("AA_WaywardMobileAssembler"))
            {
                __result = false;

            }


        }
    }
}
