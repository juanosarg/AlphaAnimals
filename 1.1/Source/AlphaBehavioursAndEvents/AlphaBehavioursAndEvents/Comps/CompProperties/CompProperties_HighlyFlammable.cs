using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_HighlyFlammable : CompProperties
    {

        public string hediffToInflict = "";

        public CompProperties_HighlyFlammable()
        {
            this.compClass = typeof(CompHighlyFlammable);
        }


    }
}