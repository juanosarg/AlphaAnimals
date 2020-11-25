
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Metamorphosis : CompProperties
    {

        //A comp class that makes an animal change into another animal after a given time

        public float timeInYears;
        public string pawnToTurnInto;
        public string reportString = "AA_TimeToMetamorphosis";

        public CompProperties_Metamorphosis()
        {
            this.compClass = typeof(CompMetamorphosis);
        }

    }
}
