
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_GasProducer : CompProperties
    {

        public string gasType = "";


        public CompProperties_GasProducer()
        {
            
            this.compClass = typeof(CompGasProducer);
        }
    }
}
