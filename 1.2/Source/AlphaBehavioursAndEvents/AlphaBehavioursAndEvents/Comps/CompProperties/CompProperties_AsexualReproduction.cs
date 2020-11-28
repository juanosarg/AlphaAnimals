using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_AsexualReproduction : CompProperties
    {
        //A comp class that allows animals to reproduce without needing to have two of them.
        //This class supports fission and sporulation
        public int reproductionIntervalDays = 1;
        public string customString ="";
        //produceEggs selects whether this creature will lay fertilized eggs / spores asexually
        public bool produceEggs = false;
        public string eggDef = "";
        //Green goo creatures just do fission after reproductionIntervalDays, even if they are not part of
        //the player faction, and they'll stop when reaching a total map count
        public bool isGreenGoo = false;
        public int GreenGooLimit = 0;
        public string GreenGooTarget = "";
        //Some creatures need to spawn a different def
        public bool convertsIntoAnotherDef = false;     
        public string newDef = "";

        public CompProperties_AsexualReproduction()
        {
            this.compClass = typeof(CompAsexualReproduction);
        }


    }
}