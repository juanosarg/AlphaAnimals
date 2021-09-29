using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    [HarmonyPatch(typeof(BiomeDef)), HarmonyPatch("IsPackAnimalAllowed")]
    class PackAnimalFilter
    {
        [HarmonyPostfix]
        public static void Listener(ThingDef pawn, ref bool __result)
        {
            if (SpawnFilterPatchUtility.ShouldBeFiltered(pawn.defName))
                __result = false;
        }
    }
}