using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class JobGiver_TryQuarryJob : ThinkNode_JobGiver
    {
       
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (!ModLister.HasActiveModWithName("Quarry"))
            {
                return null;
            }

            Building quarry = (Building) QuarriesInMap(pawn).RandomElement();

            if (quarry == null || quarry.IsForbidden(pawn))
            {
                return null;
            }

            IntVec3 cell = IntVec3.Invalid;
            CellRect rect = quarry.OccupiedRect();
            foreach (IntVec3 c in rect.Cells.InRandomOrder())
            {
                if (pawn.Map.reservationManager.CanReserve(pawn, c, 1))
                {
                    cell = c;
                    break;
                }
            }
            // If a cell wasn't found, fail
            if (!cell.IsValid)
            {
                return null;
            }

            return new Job(DefDatabase<JobDef>.GetNamed("QRY_MineQuarry", true), cell)
            {
                locomotionUrgency = LocomotionUrgency.Walk,
                expiryInterval = 8000
            };
        }

        public IEnumerable<Thing> QuarriesInMap(Pawn pawn)
        {
            return pawn.Map.listerBuildings.AllBuildingsColonistOfDef(ThingDef.Named("QRY_Quarry")).Cast<Thing>().Concat(
                                  pawn.Map.listerBuildings.AllBuildingsColonistOfDef(ThingDef.Named("QRY_MiniQuarry")).Cast<Thing>());
        }

    }
}

