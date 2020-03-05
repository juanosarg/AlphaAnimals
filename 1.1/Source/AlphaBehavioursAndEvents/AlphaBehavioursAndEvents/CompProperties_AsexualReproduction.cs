using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_AsexualReproduction : CompProperties
    {

        public int reproductionIntervalDays = 1;
        public string customString ="";
        public bool produceEggs = false;
        public string eggDef = "";
        public bool isGreenGoo = false;
        public int GreenGooLimit = 0;
        public string GreenGooTarget = "";

        public CompProperties_AsexualReproduction()
        {
            this.compClass = typeof(CompAsexualReproduction);
        }


    }
}