
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_GasProducer : CompProperties
    {

        public string gasType = "";
        public float rate = 0f;
        public int radius = 0;



        public CompProperties_GasProducer()
        {
            
            this.compClass = typeof(CompGasProducer);
        }
    }
}
