
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Exploder : CompProperties
    {


        public float wickTimeSeconds = 1f;
        public int wickTimeVariance = 1;
        public float explosionForce = 1f;


        public CompProperties_Exploder()
        {
          
            this.compClass = typeof(CompExploder);
        }
    }
}
