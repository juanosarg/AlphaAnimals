
using RimWorld;
using Verse;
using Verse.Noise;

namespace AlphaBehavioursAndEvents
{
    public class ThoughtWorker_PhotosensitiveExoskeleton : ThoughtWorker
    {
      

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!ModsConfig.BiotechActive)
            {
                return ThoughtState.Inactive;
            }
            if (p.genes == null || !p.genes?.HasActiveGene(DefDatabase<GeneDef>.GetNamedSilentFail("AA_Gene_PhotosensitiveExoskeleton"))==true || !InSunlight(p))
            {
                return ThoughtState.Inactive;
            }
            return ThoughtState.ActiveAtStage(0);
        }

        public static bool InSunlight(Pawn pawn)
        {
            if(SanguophageUtility.InSunlight(pawn.Position, pawn.Map))
            {
                return true;
            }
            return false;
        }
    }
}
