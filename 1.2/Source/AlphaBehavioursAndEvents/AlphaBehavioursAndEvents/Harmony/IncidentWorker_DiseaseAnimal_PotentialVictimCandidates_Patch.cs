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
    /*This Harmony Postfix removes mechanoid animals from the IncidentWorker_DiseaseAnimal
    */
    [HarmonyPatch(typeof(IncidentWorker_DiseaseAnimal))]
    [HarmonyPatch("PotentialVictimCandidates")]
    public static class AlphaAnimals_IncidentWorker_DiseaseAnimal_PotentialVictimCandidates_Patch
    {
        [HarmonyPostfix]
        public static void DontApplyDiseasesToMechsPlease(ref IEnumerable<Pawn> __result)

        {
            List<Pawn> list = __result.ToList();
            foreach (Pawn p in list)
            {
                if (p.def.defName.Contains("AA_WaywardMobileAssembler"))
                {
                    list.Remove(p);
                }
            }

            __result = list;
           


        }
    }
}
