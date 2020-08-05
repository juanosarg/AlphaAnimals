
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Infecter : CompProperties
    {


        public int infectionChance = 10;
       
        public CompProperties_Infecter()
        {
            this.compClass = typeof(CompInfecter);
        }


    }
}
