using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_HighlyFlammable : CompProperties
    {

        public string hediffToInflict = "";
        public int tickInterval = 50;

        public CompProperties_HighlyFlammable()
        {
            this.compClass = typeof(CompHighlyFlammable);
        }


    }
}