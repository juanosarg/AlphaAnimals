using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace AlphaBehavioursAndEvents
{
    internal class Recipe_ShutDown : RecipeWorker
    {
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            BodyPartRecord brain = pawn.health.hediffSet.GetBrain();
            if (brain != null)
            {
                yield return brain;
            }
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            pawn.Kill(null);
            ThoughtUtility.GiveThoughtsForPawnExecuted(pawn, null,PawnExecutionKind.GenericHumane);
        }
    }
}