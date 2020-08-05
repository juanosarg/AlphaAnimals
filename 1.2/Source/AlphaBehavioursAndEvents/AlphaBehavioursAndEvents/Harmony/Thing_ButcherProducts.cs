using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace AlphaBehavioursAndEvents
{
    /*This patch makes some creatures not drop regular meat, and scales the amount of butcherproducts depending on missing body parts*/


    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("ButcherProducts")]
    public static class Thing_ButcherProducts_Patch
    {
        [HarmonyPostfix]
        static void ChangeMeatAmountByAge(Thing __instance, float efficiency, ref IEnumerable<Thing> __result)
        {

            if (__instance.GetType() == typeof(Pawn))
            {

                var thingies = __result.ToList();
                var pawn = (Pawn)__instance;

                if ((__instance.def.butcherProducts != null) && ((__instance.def.defName == "AA_Aerofleet") || (__instance.def.defName == "AA_ColossalAerofleet") || (__instance.def.defName == "AA_Cactipine") || (__instance.def.defName == "AA_Needlepost") || (__instance.def.defName == "AA_Needleroll") || (__instance.def.defName == "AA_Wildpod") || (__instance.def.defName == "AA_Wildpawn") || (__instance.def.defName == "GR_Needlechicken") || (__instance.def.defName == "AA_OcularJelly") || (__instance.def.defName == "AA_Mantrap") || (__instance.def.defName == "AA_Agaripod") || (__instance.def.defName == "AA_MycoidColossus")))
                {
                    //Log.Message("Adding meat butcher products", false);

                    int baseCalculation = 90;
                    if ((__instance.def.defName == "AA_Wildpod") || (__instance.def.defName == "AA_Agaripod"))
                    {
                        baseCalculation = 30;
                    }
                    if (__instance.def.defName == "AA_MycoidColossus")
                    {
                        baseCalculation = 15;
                    }

                    ThingDefCountClass ta = __instance.def.butcherProducts[0];
                    float num = pawn.health.hediffSet.GetCoverageOfNotMissingNaturalParts(pawn.RaceProps.body.corePart);
                    int count = GenMath.RoundRandom((pawn.BodySize * baseCalculation * efficiency * num));
                    if (count > 0)
                    {
                        Thing t = ThingMaker.MakeThing(ta.thingDef, null);
                        t.stackCount = count;
                        thingies.Insert(1, t);

                        __result = thingies;
                    }


                }
            }




        }
    }








}
