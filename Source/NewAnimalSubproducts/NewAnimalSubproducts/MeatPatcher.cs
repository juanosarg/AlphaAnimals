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
        static void ChangeMeatAmountByAge(Thing __instance, float efficiency, IEnumerable<Thing> __result)
        {

            if ((__instance.def.butcherProducts != null)&& (__instance.def.defName == "AA_Aerofleet"))
            {
                Log.Message("Beginning meat search", false);

                for (int i = 0; i < __instance.def.butcherProducts.Count; i++)
                {
                    ThingDefCountClass ta = __instance.def.butcherProducts[i];
                    int count = GenMath.RoundRandom((float)ta.count * efficiency);
                    if (count > 0)
                    {
                        Thing t = ThingMaker.MakeThing(ta.thingDef, null);
                        t.stackCount = count;
                        __result =  t;
                    }
                }

            }



        }
    }
    

   /* [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("GetGizmos")]
    public static class ThingDefGenerator_Meat_ImpliedMeatDefs_Patch2
    {
        [HarmonyPrefix]
        static void ChangeMeatGraphic2()
        {
            Log.Message("Beginning meat search", false);

          



        }
    }

    */






}
