using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class JobDriver_PlantAnimaTree : JobDriver
    {
        

        private Thing Item
        {
            get
            {
                return this.job.GetTarget(TargetIndex.B).Thing;
            }
        }

        private IntVec3 TargetPosition
        {
            get
            {
                return this.job.GetTarget(TargetIndex.A).Cell;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.Item, this.job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            Map map = Item.Map;
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, false, false);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            Toil toil = Toils_General.Wait(600, TargetIndex.None);
            toil.WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            toil.FailOnDespawnedOrNull(TargetIndex.A);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            yield return toil;
            Toil createAnimaTree = new Toil();
            createAnimaTree.initAction = delegate ()
            {
                Thing thing = ThingMaker.MakeThing(ThingDef.Named("Plant_TreeAnima"), null);
                thing.stackCount = 1;
                Thing t;
                GenPlace.TryPlaceThing(thing, TargetPosition, map, ThingPlaceMode.Direct, out t, null, null, default(Rot4));
                Item.Destroy();
            };
            yield return createAnimaTree;
            yield break;
        }

    }
}
