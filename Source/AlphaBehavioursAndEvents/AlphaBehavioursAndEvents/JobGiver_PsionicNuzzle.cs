using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class JobGiver_PsionicNuzzle : ThinkNode_JobGiver
    {
        private const float MaxNuzzleDistance = 40f;

        protected override Job TryGiveJob(Pawn pawn)
        {
            if (pawn.RaceProps.nuzzleMtbHours <= 0f)
            {
                return null;
            }
            List<Pawn> source = pawn.Map.mapPawns.SpawnedPawnsInFaction(pawn.Faction);
            Pawn t;
            if (!(from p in source
                  where !p.NonHumanlikeOrWildMan() && p != pawn && p.Position.InHorDistOf(pawn.Position, 40f) && pawn.GetRoom(RegionType.Set_Passable) == p.GetRoom(RegionType.Set_Passable) && !p.Position.IsForbidden(pawn) && p.CanCasuallyInteractNow(false)
                  select p).TryRandomElement(out t))
            {
                return null;
            }
            return new Job(DefDatabase<JobDef>.GetNamed("AA_PsionicNuzzle", true), t)
            {
                locomotionUrgency = LocomotionUrgency.Walk,
                expiryInterval = 3000
            };
        }
    }
}

