using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace AnimalRangeAttack
{
	class JobDriver_AnimalRangeAttack : JobDriver
	{
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			//Log.Warning("Trying to reserve animal shoot");
			IAttackTarget thing = this.job.targetA.Thing as IAttackTarget;
			if (thing != null)
				this.pawn.Map.attackTargetReservationManager.Reserve(this.pawn, this.job, thing);
			return true;
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			//Log.Warning("Trying to make new toil");
			yield return this.Fire(this.TargetThingA).FailOnDespawnedNullOrForbidden(TargetIndex.A);
			yield break;
		}

		private Toil Fire(Thing target)
		{
			//Log.Warning("Trying to make fire toil");
			if (target == null)
				return null;

			Toil toil = new Toil();

			//Log.Message("Pawn: " + pawn + ", target: " + target);

			//Beware of verb to use
			
			toil.initAction = delegate
			{
				Pawn pawn = this.pawn;

				this.GetActor().CurJob.verbToUse.TryStartCastOn(target);
			};
			
			toil.defaultCompleteMode = ToilCompleteMode.Instant;
			return toil;
		}
	}
}