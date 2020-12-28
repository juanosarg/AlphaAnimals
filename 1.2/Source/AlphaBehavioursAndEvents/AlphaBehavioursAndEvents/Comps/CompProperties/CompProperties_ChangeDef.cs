
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_ChangeDef : CompProperties
    {


        public string defToChangeTo = "";
        public string factionToChangeTo = "Mechanoid";


        public CompProperties_ChangeDef()
        {
            this.compClass = typeof(CompChangeDef);
        }


    }
}