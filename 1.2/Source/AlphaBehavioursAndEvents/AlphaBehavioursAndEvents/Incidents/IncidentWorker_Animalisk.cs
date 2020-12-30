using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
   
    public class IncidentWorker_Animalisk : IncidentWorker
    {
        

       
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return ModsConfig.RoyaltyActive && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("AA_Animalisk")) && this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Mod.settings.flagAnimalisk;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef pawnKindDef = PawnKindDef.Named("AA_Animalisk");
            IntVec3 intVec;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal))
            {
                return false;
            }

            Rot4 rot = Rot4.FromAngleFlat((map.Center - intVec).AngleFlat);

            IntVec3 loc2 = CellFinder.RandomClosewalkCellNear(intVec, map, 1, null);
            Pawn newThing = PawnGenerator.GeneratePawn(pawnKindDef, null);
            
            GenSpawn.Spawn(newThing, loc2, map, WipeMode.Vanish);




            Find.LetterStack.ReceiveLetter("AA_LetterLabelAnimaliskEnters".Translate(), "AA_LetterAnimaliskEnters".Translate(), LetterDefOf.ThreatBig, newThing, null, null);





            return true;
        }
    }
}

