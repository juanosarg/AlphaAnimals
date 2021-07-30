using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
	public class StageEndTrigger_PawnDeliveredOrNotValidDebug : StageEndTrigger
	{
		public override Trigger MakeTrigger(LordJob_Ritual ritual, TargetInfo spot, IEnumerable<TargetInfo> foci, RitualStage stage)
		{
			return new Trigger_TickCondition(delegate ()
			{
				foreach (TargetInfo targetInfo in foci)
				{
					Pawn pawn = targetInfo.Thing as Pawn;
					IntVec3 c = (spot.Thing != null) ? spot.Thing.OccupiedRect().CenterCell : spot.Cell;
					if (pawn.Position!=c && !pawn.Dead)
					{
						
						return false;
					}
				}
				
				return true;
			}, 1);
		}

		public override void ExposeData()
		{
		}
	}
}
