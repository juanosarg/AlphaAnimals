using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_FungalHusk : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return 
                 
                this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Mod.settings.flagFungalHusk;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            if (!this.TryFindEntryCell(map, out intVec))
            {
                return false;
            }
            PawnKindDef husk = PawnKindDef.Named("AA_FungalHusk");
            
            float num = StorytellerUtility.DefaultThreatPointsNow(map);
            int num2 = GenMath.RoundRandom(num / husk.combatPower);           
            int max = Rand.RangeInclusive(2, 10);          
            num2 = Mathf.Clamp(num2, 1, max);
           
           
            Pawn pawn = null;
            for (int i = 0; i < num2; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                pawn = PawnGenerator.GeneratePawn(husk, null);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                pawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("ManhunterPermanent", true), null, true, false, null, false);
                
            }
          
            Find.LetterStack.ReceiveLetter("AA_LetterLabelFungalHusk".Translate(husk.label.CapitalizeFirst()), "AA_LetterFungalHusk".Translate(husk.label), LetterDefOf.NegativeEvent, pawn, null, null);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }
    }
}
