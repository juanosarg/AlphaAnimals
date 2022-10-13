using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_Bumbledrones : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) &&
                map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("AA_Bumbledrone")) && 
                this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Mod.settings.flagBumbledrones;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            if (!this.TryFindEntryCell(map, out intVec))
            {
                return false;
            }
            PawnKindDef bumbledrone = PawnKindDef.Named("AA_Bumbledrone");
            PawnKindDef bumbledronequeen = PawnKindDef.Named("AA_BumbledroneQueen");

            float num = StorytellerUtility.DefaultThreatPointsNow(map);
            int num2 = 1;
            int num2smaller = GenMath.RoundRandom(num / bumbledrone.combatPower);
            int maxsmaller = Rand.RangeInclusive(10, 25);
          
            num2smaller = Mathf.Clamp(num2smaller, 1, maxsmaller);
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
                pawn = PawnGenerator.GeneratePawn(bumbledronequeen, null);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num3;
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                }
            }
            for (int i = 0; i < num2smaller; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                pawn = PawnGenerator.GeneratePawn(bumbledrone, null);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num3;
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                }
            }
            Find.LetterStack.ReceiveLetter("LetterLabelBumbledroneMigration".Translate(bumbledronequeen.label.CapitalizeFirst()), "LetterBumbledroneMigration".Translate(bumbledronequeen.label), LetterDefOf.PositiveEvent, pawn, null, null);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }
    }
}
