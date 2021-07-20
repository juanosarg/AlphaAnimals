using System;
using Verse;
using Verse.AI;
using RimWorld;
using System.Reflection;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
	public class JobGiver_MergeIntoGaumakerPodCorruptor : ThinkNode_JobGiver
	{
		public Thing podthing;
		protected override Job TryGiveJob(Pawn pawn)
		{
			if (!ModsConfig.IdeologyActive)
			{
				return null;
			}
			if (pawn.connections == null || pawn.connections.ConnectedThings.NullOrEmpty<Thing>())
			{
				return null;
			}
			foreach (Thing thing in pawn.connections.ConnectedThings)
			{
				CompTreeConnection compTreeConnection = thing.TryGetComp<CompTreeConnection>();
				if (compTreeConnection != null && this.ShouldEnterCorruptedPod(compTreeConnection,pawn) && pawn.CanReach(podthing, PathEndMode.Touch, Danger.Deadly, false, false, TraverseMode.ByPawn))
				{
					return JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("AA_MergeIntoCorruptedPod"), thing, podthing);
				}
			}
			return null;
		}

		public bool ShouldEnterCorruptedPod(CompTreeConnection compTreeConnection, Pawn dryad)
        {
			
			List<Pawn> dryads = (List<Pawn>)typeof(CompTreeConnection).GetField("dryads", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(compTreeConnection);
			
			List<Pawn> tmpDryads = (List<Pawn>)typeof(CompTreeConnection).GetField("tmpDryads", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(compTreeConnection);

			if (compTreeConnection.parent.Map.listerThings.ThingsOfDef(ThingDef.Named("AA_CorruptedGaumakerCocoon")).Count==0)
			{
				IntVec3 loc;
				if (CellFinder.TryFindRandomCellNear(compTreeConnection.parent.Position, compTreeConnection.parent.Map, 10, delegate (IntVec3 x)
				{
					Thing thing;
					return ThingDefOf.Plant_PodGauranlen.CanEverPlantAt(x, compTreeConnection.parent.Map, out thing, true, false).Accepted;
				}, out loc, -1))
				{
					podthing= GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_CorruptedGaumakerCocoon"), null), loc, compTreeConnection.parent.Map, WipeMode.Vanish);
					
				}
				
			}
			if (dryads.NullOrEmpty<Pawn>() || dryads.Count < 3 || !dryads.Contains(dryad))
			{
				
				return false;
			}
			tmpDryads.Clear();
			for (int i = 0; i < dryads.Count; i++)
			{
				if (dryads[i].kindDef == PawnKindDef.Named("AA_Dryad_Corruptor"))
				{
					tmpDryads.Add(dryads[i]);
				}
			}
			if (tmpDryads.Count < 3)
			{
				tmpDryads.Clear();
				
				return false;
			}
			tmpDryads.SortBy((Pawn x) => -x.ageTracker.AgeChronologicalTicks);
			for (int j = 0; j < 3; j++)
			{
				if (tmpDryads[j] == dryad)
				{
					tmpDryads.Clear();
					
					return true;
				}
			}
			tmpDryads.Clear();
			
			return false;
		}

	}
}