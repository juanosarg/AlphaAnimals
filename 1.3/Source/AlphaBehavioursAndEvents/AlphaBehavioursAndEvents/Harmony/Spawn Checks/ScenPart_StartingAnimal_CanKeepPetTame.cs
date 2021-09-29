using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    [HarmonyPatch(typeof(ScenPart_StartingAnimal)), HarmonyPatch("CanKeepPetTame")]
    class StartingPetFilter
    {
        [HarmonyPostfix]
        public static void Listener(PawnKindDef def, ref bool __result)
        {
            if (SpawnFilterPatchUtility.ShouldBeFiltered(def.defName))
                __result = false;
        }
    }
}