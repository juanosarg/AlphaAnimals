using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_Gallatross : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) &&
                map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("AA_Gallatross")) &&
                this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Mod.settings.flagGallatross;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            if (!this.TryFindEntryCell(map, out intVec))
            {
                return false;
            }
            PawnKindDef gallatross = PawnKindDef.Named("AA_Gallatross");
            
            float num = StorytellerUtility.DefaultThreatPointsNow(map);
            int num2 = GenMath.RoundRandom(num / gallatross.combatPower);
            int max = Rand.RangeInclusive(2, 4);
            num2 = Mathf.Clamp(num2, 1, max);
            int num3 = Rand.RangeInclusive(90000, 150000);
            IntVec3 invalid = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out invalid))
            {
                invalid = IntVec3.Invalid;
            }
            Pawn pawn = null;
            for (int i = 0; i < num2; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                pawn = PawnGenerator.GeneratePawn(gallatross, null);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num3;
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                }
            }
           
            Find.LetterStack.ReceiveLetter("LetterLabelGallatrossPasses".Translate(gallatross.label.CapitalizeFirst()), "LetterGallatrossPasses".Translate(gallatross.label), LetterDefOf.PositiveEvent, pawn, null, null);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }
    }
}
