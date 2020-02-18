using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.AI;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class JobDriver_PsionicNuzzle : JobDriver
    {
        private const int NuzzleDuration = 100;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnNotCasualInterruptible(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_Interpersonal.WaitToBeAbleToInteract(this.pawn);
            Toil gotoTarget = Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            gotoTarget.socialMode = RandomSocialMode.Off;
            Toil wait = Toils_General.WaitWith(TargetIndex.A, 100, false, true);
            wait.socialMode = RandomSocialMode.Off;
            yield return Toils_General.Do(delegate
            {
                Pawn recipient = (Pawn)this.pawn.CurJob.targetA.Thing;
                this.pawn.interactions.TryInteractWith(recipient, DefDatabase<InteractionDef>.GetNamed("AA_PsionicNuzzleInteraction", true));
            });
        }
    }
}