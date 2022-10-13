using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_BehemothPasses : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(InternalDefOf.AA_Behemoth.race) && AlphaAnimalsEvents_Mod.settings.flagBehemoth && this.TryFindEntryCell(map, out intVec);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            bool flag = !this.TryFindEntryCell(map, out intVec);
            bool flag2 = flag;
            bool result;
            if (flag2)
            {
                result = false;
            }
            else
            {
                PawnKindDef fO_Behemoth = InternalDefOf.AA_Behemoth;
                float num = StorytellerUtility.DefaultThreatPointsNow(map);
                int num2 = GenMath.RoundRandom(num / fO_Behemoth.combatPower);
                int num3 = Rand.RangeInclusive(1, 4);
                num2 = Mathf.Clamp(num2, 1, num3);
                int num4 = Rand.RangeInclusive(90000, 150000);
                IntVec3 invalid = IntVec3.Invalid;
                bool flag3 = !RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out invalid);
                bool flag4 = flag3;
                if (flag4)
                {
                    invalid = IntVec3.Invalid;
                }
                Pawn pawn = null;
                for (int i = 0; i < num2; i++)
                {
                    IntVec3 intVec2 = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                    pawn = PawnGenerator.GeneratePawn(fO_Behemoth, null);
                    GenSpawn.Spawn(pawn, intVec2, map, Rot4.Random, 0, false);
                    pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num4;
                    bool isValid = invalid.IsValid;
                    bool flag5 = isValid;
                    if (flag5)
                    {
                        pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                    }
                }
                map.weatherManager.TransitionTo(InternalDefOf.DryThunderstorm);
                Find.LetterStack.ReceiveLetter("AA_LetterLabelBehemothPasses".Translate(fO_Behemoth.label), "AA_LetterBehemothPasses".Translate(fO_Behemoth.label), LetterDefOf.PositiveEvent, pawn, null, null);
                result = true;
            }
            return result;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, null);
        }
    }
}

