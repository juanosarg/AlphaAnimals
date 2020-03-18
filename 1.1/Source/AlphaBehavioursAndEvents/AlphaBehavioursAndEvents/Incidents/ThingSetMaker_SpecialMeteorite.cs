using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using UnityEngine;


namespace AlphaBehavioursAndEvents
{
    public class ThingSetMaker_SpecialMeteorite : ThingSetMaker
    {
        public static void Reset()
        {
            ThingSetMaker_SpecialMeteorite.nonSmoothedMineables.Clear();
            ThingSetMaker_SpecialMeteorite.nonSmoothedMineables.AddRange(from x in DefDatabase<ThingDef>.AllDefsListForReading
                                                                  where x.mineable && x != ThingDefOf.CollapsedRocks && !x.IsSmoothed
                                                                  select x);
        }

        protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
        {
            IntRange? countRange = parms.countRange;
            int randomInRange = ((countRange == null) ? ThingSetMaker_SpecialMeteorite.MineablesCountRange : countRange.Value).RandomInRange;
            ThingDef def = this.FindRandomMineableDef();
            for (int i = 0; i < randomInRange; i++)
            {
                Building building = (Building)ThingMaker.MakeThing(def, null);
                building.canChangeTerrainOnDestroyed = false;
                outThings.Add(building);
            }
            PawnKindDef skyeel = PawnKindDef.Named("AA_Skyeel");
            Map mapPlayerHome = null;
            List<Map> maps = Find.Maps;
            for (int i = 0; i < maps.Count; i++)
            {
                if (maps[i].IsPlayerHome)
                {
                    mapPlayerHome = maps[i];
                }
            }

            float num = StorytellerUtility.DefaultThreatPointsNow(mapPlayerHome);
            int num2 = GenMath.RoundRandom(num / skyeel.combatPower);
            int max = Rand.RangeInclusive(5, 20);
            num2 = Mathf.Clamp(num2, 4, max);

            for (int i = 0; i < num2; i++)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(skyeel, null);
                outThings.Add(pawn);
            }
            

        }

        private ThingDef FindRandomMineableDef()
        {
            return ThingDef.Named("AA_SkySteelClumps");
        }

        protected override IEnumerable<ThingDef> AllGeneratableThingsDebugSub(ThingSetMakerParams parms)
        {
            return ThingSetMaker_SpecialMeteorite.nonSmoothedMineables;
        }

        public static List<ThingDef> nonSmoothedMineables = new List<ThingDef>();

        public static readonly IntRange MineablesCountRange = new IntRange(8, 20);

        private const float PreciousMineableMarketValue = 5f;
    }
}
