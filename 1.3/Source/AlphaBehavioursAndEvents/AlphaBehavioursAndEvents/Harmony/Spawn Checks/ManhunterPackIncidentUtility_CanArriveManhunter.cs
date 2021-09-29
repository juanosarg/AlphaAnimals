using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    [HarmonyPatch(typeof(ManhunterPackIncidentUtility)), HarmonyPatch("CanArriveManhunter")]
    class ManhunterFilter
    {
        [HarmonyPostfix]
        public static void Listener(PawnKindDef kind, ref bool __result)
        {
            // Toggling the spawn in the options menu will dynamically allow these to spawn / not spawn
            if (SpawnFilterPatchUtility.ShouldBeFiltered(kind.defName))
                __result = false;
        }
    }
}