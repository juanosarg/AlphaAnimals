using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
  /*  [HarmonyPatch(typeof(StockGenerator_Animals)), HarmonyPatch("PawnKindAllowed")]
    class TraderFilter
    {
        [HarmonyPostfix]
        public static void Listener(PawnKindDef kind, ref bool __result)
        {
            // Toggling the spawn in the options menu will dynamically allow these to spawn / not spawn
            // No idea why kind could be null
            if (kind != null && SpawnFilterPatchUtility.ShouldBeFiltered(kind.defName))
                __result = false;
        }
    }*/
}