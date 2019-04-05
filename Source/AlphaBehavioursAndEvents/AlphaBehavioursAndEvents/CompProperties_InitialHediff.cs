using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_InitialHediff : CompProperties
    {

        public string hediffname = "";
        public float hediffseverity = 0f;




        public CompProperties_InitialHediff()
        {
            this.compClass = typeof(CompInitialHediff);
        }


    }
}
