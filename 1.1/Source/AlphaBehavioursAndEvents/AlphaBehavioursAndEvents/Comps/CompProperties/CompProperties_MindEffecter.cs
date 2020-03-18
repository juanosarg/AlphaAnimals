
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_MindEffecter : CompProperties
    {


        public int radius = 1;
        public int tickInterval = 1000;
        public string mentalState = "Berserk";



        public CompProperties_MindEffecter()
        {
            this.compClass = typeof(CompMindEffecter);
        }


    }
}
