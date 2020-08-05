
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_ThoughtEffecter : CompProperties
    {


        public int radius = 1;
        public int tickInterval = 1000;
        public string thoughtDef = "AteWithoutTable";
        public bool showEffect = false;



        public CompProperties_ThoughtEffecter()
        {
            this.compClass = typeof(CompThoughtEffecter);
        }


    }
}

