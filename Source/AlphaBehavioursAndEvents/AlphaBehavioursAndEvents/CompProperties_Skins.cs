using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Skins : CompProperties
    {

        public int numberOfSkins = 1;
        public string skinBaseString ="";

        public CompProperties_Skins()
        {
            this.compClass = typeof(CompSkins);
        }


    }
}