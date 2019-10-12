using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;

namespace NocturnalAnimals
{

    public static class Patch_ThingDef
    {
        
        [HarmonyPatch(typeof(ThingDef))]
        [HarmonyPatch(nameof(ThingDef.SpecialDisplayStats))]
        public static class Patch_SpecialDisplayStats
        {

            public static void Postfix(ThingDef __instance, ref IEnumerable<StatDrawEntry> __result)
            {
                if (__instance.race is RaceProperties raceProps && raceProps.Animal)
                {
                    // Body clock
                    BodyClock bodyClock = BodyClock.Diurnal;
                    var extendedRaceProps = __instance.GetModExtension<ExtendedRaceProperties>();
                    if (extendedRaceProps != null)
                        bodyClock = extendedRaceProps.bodyClock;
                    __result = __result.Add(new StatDrawEntry(StatCategoryDefOf.BasicsPawn, "NocturnalAnimals.BodyClock".Translate(), $"NocturnalAnimals.BodyClock_{bodyClock}".Translate(),
                        overrideReportText: "NocturnalAnimals.BodyClock_Description".Translate()));
                }
            }

        }

    }

}
