using Harmony;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using RimWorld.Planet;


// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace AlphaBehavioursAndEvents
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
  

    /*This Harmony Prefix makes the creature carry more weight
    */
    [HarmonyPatch(typeof(MassUtility))]
    [HarmonyPatch("Capacity")]
    public static class MassUtility_Capacity_Patch
    {
        [HarmonyPostfix]
        public static void MakeGigantelopesCarryMore(Pawn p, ref float __result)

        {
            bool flagIsCreatureMine = p.Faction != null && p.Faction.IsPlayer;
            bool flagDoesCreatureHaveTheHediffs = (p.TryGetComp<CompInitialHediff>() != null);
            bool flagCanCreatureCarryMore = false;
            if (flagDoesCreatureHaveTheHediffs)
            {
                flagCanCreatureCarryMore = (p.TryGetComp<CompInitialHediff>().Props.hediffname == "AA_CarryWeight");
            }
            

            
                               

            if (flagIsCreatureMine && flagCanCreatureCarryMore)
            {
                int factor = p.TryGetComp<CompInitialHediff>().phase;
                __result = p.BodySize * 35f + factor*factor -5;
            }

        }
    }



   




}
