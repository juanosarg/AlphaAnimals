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
  
    [HarmonyPatch(typeof(ThingDefGenerator_Meat))]
    [HarmonyPatch("ImpliedMeatDefs")]
    public static class ThingDefGenerator_Meat_ImpliedMeatDefs_Patch
    {
        [HarmonyPostfix]
        static void ChangeMeatGraphic(ref IEnumerable<ThingDef> __result)
        {
            Log.Message("Beginning meat search", false);

            IEnumerable<ThingDef> meatsList = __result; 
            foreach (ThingDef sourceDef in meatsList)
            {
                if (sourceDef.defName == "Meat_AA_Aerofleet") {
                    Log.Message("Found the correct meat", false);
                    sourceDef.graphicData.texPath = "Things/Item/Resource/MeatFoodRaw/Meat_Human";

                }
            }

            __result = meatsList;



        }
    }

    [HarmonyPatch(typeof(ThingDefGenerator_Meat))]
    [HarmonyPatch("ImpliedMeatDefs")]
    public static class ThingDefGenerator_Meat_ImpliedMeatDefs_Patch2
    {
        [HarmonyPrefix]
        static void ChangeMeatGraphic2()
        {
            Log.Message("Beginning meat search", false);

          



        }
    }








}
