using System;
using UnityEngine;
using Verse;
using RimWorld;
using Verse.AI;


namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_MechanoidSappers : IncidentWorker
    {
       
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return base.CanFireNowSub(parms) &&
                this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Settings.flagAlphaMechanoidsSappers;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }


        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
           
            Thing t = this.SpawnAssembler(map, parms);
            Find.LetterStack.ReceiveLetter("AA_LetterLabelMechanoidSappers".Translate(), "AA_LetterMechanoidSappers".Translate(), LetterDefOf.ThreatBig, t, null, null);

            return true;
        }

        private Thing SpawnAssembler(Map map, IncidentParms parms)
        {
            IntVec3 loc;
            IntVec3 loc2;

            if (!TryFindEntryCell(map, out loc))
            {
                return null;
            }

            if (CellFinder.TryFindRandomCellNear(loc, map, 8, (IntVec3 c) => c.Standable(map) && IntVec3Utility.ManhattanDistanceFlat(c,loc)>5, out loc2, -1))
            {

                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_AncientAssembler"), null), loc2, map, WipeMode.FullRefund);
                Building_AncientAssembler building = thing as Building_AncientAssembler;
               
               
                return thing;
            }
            else return null;

           
        }
    }
}


