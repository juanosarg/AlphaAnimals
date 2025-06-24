
using RimWorld;
using Verse;
using Verse.AI;

namespace AlphaBehavioursAndEvents
{
    public class MentalBreakWorker_PhotosensitiveExoskeleton : MentalBreakWorker
    {
        public override bool BreakCanOccur(Pawn pawn)
        {
            if (!pawn.Spawned)
            {
                return false;
            }
            if (!base.BreakCanOccur(pawn))
            {
                return false;
            }
            return ThoughtWorker_PhotosensitiveExoskeleton.InSunlight(pawn);
        }
    }
}
