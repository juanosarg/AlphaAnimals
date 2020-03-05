using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Regeneration : CompProperties
    {

        public int rateInTicks = 1000;

        public CompProperties_Regeneration()
        {
            this.compClass = typeof(CompRegeneration);
        }


    }
}