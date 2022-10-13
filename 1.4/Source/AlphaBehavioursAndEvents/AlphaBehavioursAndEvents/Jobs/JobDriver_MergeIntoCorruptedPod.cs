using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
	public class JobDriver_MergeIntoCorruptedPod : JobDriver
	{
		private CompTreeConnection TreeComp
		{
			get
			{
				return this.job.targetA.Thing.TryGetComp<CompTreeConnection>();
			}
		}

		private CompCorruptedPod GaumakerPod
		{
			get
			{
				return this.job.targetB.Thing.TryGetComp<CompCorruptedPod>();
			}
		}

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return true;
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			
			this.FailOnDespawnedOrNull(TargetIndex.A);
			this.FailOnDespawnedOrNull(TargetIndex.B);
			this.FailOn(() => this.GaumakerPod.Full);
			yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch);
			yield return Toils_General.WaitWith(TargetIndex.B, WaitTicks, true, false);
			yield return Toils_General.Do(delegate
			{
				this.GaumakerPod.TryAcceptPawn(this.pawn);
			});
			yield break;
		}

		private const int WaitTicks = 120;
	}
}
