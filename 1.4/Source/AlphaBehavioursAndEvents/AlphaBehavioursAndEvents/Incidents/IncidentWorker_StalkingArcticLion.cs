using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
   
    public class IncidentWorker_StalkingArcticLion : IncidentWorker
    {
        

        private const int AnimalsStayDurationMin = 60000;

        private const int AnimalsStayDurationMax = 120000;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("AA_ArcticLion")) && this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Mod.settings.flagStalkingLions ;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef pawnKindDef = PawnKindDef.Named("AA_ArcticLion");
            IntVec3 intVec;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal))
            {
                return false;
            }
            List<Pawn> list = ManhunterPackIncidentUtility.GenerateAnimals(pawnKindDef, map.Tile, 300,1);
            Rot4 rot = Rot4.FromAngleFlat((map.Center - intVec).AngleFlat);
            for (int i = 0; i < list.Count; i++)
            {
                Pawn pawn = list[i];
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                GenSpawn.Spawn(pawn, loc, map, rot, WipeMode.Vanish, false);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, false, false, null, false);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(60000, 120000);
                pawn.health.AddHediff(HediffDef.Named("AA_InvisibleArcticLion"));
            }
            Find.LetterStack.ReceiveLetter("LetterLabelManhuntingArcticLion".Translate(), "ManhuntingArcticLion".Translate(), LetterDefOf.ThreatBig, null, null, null);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
           
            return true;
        }
    }
}

