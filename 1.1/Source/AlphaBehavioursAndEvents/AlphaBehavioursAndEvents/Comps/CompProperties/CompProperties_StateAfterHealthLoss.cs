
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_StateAfterHealthLoss : CompProperties
    {


        public int healthPercent = 50;
        public int tickInterval = 1000;
        public string mentalState = "PanicFlee";


        public CompProperties_StateAfterHealthLoss()
        {
            this.compClass = typeof(CompStateAfterHealthLoss);
        }


    }
}