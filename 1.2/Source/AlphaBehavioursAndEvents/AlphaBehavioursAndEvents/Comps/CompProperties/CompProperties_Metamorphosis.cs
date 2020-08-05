
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Metamorphosis : CompProperties
    {

        public float timeInYears;
        public string pawnToTurnInto;

        public CompProperties_Metamorphosis()
        {
            this.compClass = typeof(CompMetamorphosis);
        }

    }
}
