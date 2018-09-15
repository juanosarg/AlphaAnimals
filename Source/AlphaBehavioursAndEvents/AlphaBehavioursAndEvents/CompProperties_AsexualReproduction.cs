using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_AsexualReproduction : CompProperties
    {

        public int reproductionIntervalDays = 1;
        public string customString ="";

        public CompProperties_AsexualReproduction()
        {
            this.compClass = typeof(CompAsexualReproduction);
        }


    }
}