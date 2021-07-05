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
    /*This Harmony Postfix applies a hediff when the creature eats a corpse
    */
    [HarmonyPatch(typeof(JobDriver_Ingest))]
    [HarmonyPatch("MakeNewToils")]
    public static class AlphaAnimals_JobDriver_Ingest_MakeNewToils_Patch
    {
        [HarmonyPostfix]
        public static void ApplyHediffIfCorpseEaten(JobDriver_Ingest __instance)

        {
            if (__instance.pawn.def.defName == "AA_Erin")
            {
                if (__instance.job.GetTarget(TargetIndex.A).Thing is Corpse) {
                    __instance.pawn.health.AddHediff(HediffDef.Named("AA_EatenACorpse"));
                }
            } 


        }
    }
}
