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
    [HarmonyPatch(typeof(BiomeDef))]
    [HarmonyPatch("CommonalityOfAnimal")]
    public static class AlphaAnimals_BiomeDef_CommonalityOfAnimal_Patch
    {
        [HarmonyPostfix]
        public static void MultiplyAlphaAnimalCommonality(PawnKindDef animalDef, ref float __result)

        {

            if (animalDef.defName.Contains("AA_"))
            {
                float TotalMultiplier = AlphaAnimalsEvents_Mod.settings.alphaAnimalSpawnMultiplier * 0.5f;
                __result *= TotalMultiplier;

            }
            

        }
    }
}
