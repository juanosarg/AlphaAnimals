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
    [HarmonyPatch(typeof(MapTemperature))]
    [HarmonyPatch("SeasonAcceptableFor")]
    public static class AlphaAnimals_MapTemperature_SeasonAcceptableFor_Patch
    {
        [HarmonyPostfix]
        public static void AllowAnimalSpawns(ThingDef animalRace, ref bool __result)

        {

            if (!AlphaAnimals_Mod.settings.flagVanillaAnimals)
            {

                if (InternalDefOf.AA_VanillaAnimalToggles.toggleablePawns.Contains(animalRace.defName))
                {
                    __result = false;
                }

            }

            if (AlphaAnimals_Mod.settings.pawnSpawnStates != null && AlphaAnimals_Mod.settings.pawnSpawnStates.Keys.Contains(animalRace.defName))
            {
                if (AlphaAnimals_Mod.settings.pawnSpawnStates[animalRace.defName])
                {

                    __result = false;
                }

            }


        }
    }


}
