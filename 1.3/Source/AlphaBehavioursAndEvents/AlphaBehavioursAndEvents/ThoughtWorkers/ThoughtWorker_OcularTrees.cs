// RimWorld.ThoughtWorker_TreesDesired
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{

    public class ThoughtWorker_OcularTrees : ThoughtWorker_Precept
    {


        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            return true;
        }

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!base.CurrentStateInternal(p).Active)
            {
                return ThoughtState.Inactive;
            }
            return ThoughtState.ActiveAtStage(ThoughtStageIndex(p));
        }

        private int ThoughtStageIndex(Pawn p)
        {
            int numberOfOcularTrees = 0;
            if (p.Map != null) {
                numberOfOcularTrees = p.Map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamedSilentFail("AA_AlienTree")).Count;
                if (DefDatabase<ThingDef>.GetNamedSilentFail("AB_AlienTree") != null)
                {
                    numberOfOcularTrees += p.Map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamedSilentFail("AB_AlienTree")).Count;

                }
            }
  

            if (numberOfOcularTrees == 0)
            {
                return 0;
            } else if (numberOfOcularTrees < 50)
            {
                return 1;
            }
            else if (numberOfOcularTrees < 500)
            {
                return 2;
            }
            else if (numberOfOcularTrees < 1000)
            {
                return 3;
            }
            else
            {
                return 4;
            }

           

        }
    }
}
