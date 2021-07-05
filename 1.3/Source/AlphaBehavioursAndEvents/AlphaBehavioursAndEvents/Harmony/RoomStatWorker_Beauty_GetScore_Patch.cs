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
    /*This Harmony Postfix makes Pedigreed Raptors increase the beauty of a room by 15%
    */
    [HarmonyPatch(typeof(RoomStatWorker_Beauty))]
    [HarmonyPatch("GetScore")]
    public static class AlphaAnimals_RoomStatWorker_Beauty_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void IncreaseRoomBeautyIfPedigreedRaptorDetected(Room room, ref float __result)

        {
            
            if (room.ContainsThing(ThingDef.Named("AA_PedigreedRaptor")))
            {
                if (__result>0) {
                    __result *= 1.15f;
                }
                else {
                    __result = __result +(-__result * 0.15f);
                }
                
            }

        }
    }
}
