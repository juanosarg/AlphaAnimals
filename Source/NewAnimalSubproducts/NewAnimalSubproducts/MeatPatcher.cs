using Harmony;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;


// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace NewAlphaAnimalSubproducts
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.alphaanimals");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }
  
    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("ButcherProducts")]
    public static class Thing_ButcherProducts_Patch
    {
        [HarmonyPostfix]
        static void ChangeMeatAmountByAge(Thing __instance, float efficiency, ref IEnumerable<Thing> __result)
        {

            if (__instance.GetType()==typeof(Pawn)) {

                var thingies = __result.ToList();
                var pawn = (Pawn)__instance;

                if ((__instance.def.butcherProducts != null) && ((__instance.def.defName == "AA_Aerofleet") || (__instance.def.defName == "AA_ColossalAerofleet") || (__instance.def.defName == "AA_Cactipine")))
                {
                    Log.Message("Adding meat butcher products", false);


                    ThingDefCountClass ta = __instance.def.butcherProducts[0];
                    float num = pawn.health.hediffSet.GetCoverageOfNotMissingNaturalParts(pawn.RaceProps.body.corePart);
                    int count = GenMath.RoundRandom((pawn.BodySize * 90 * efficiency * num));
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
