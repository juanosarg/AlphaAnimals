
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_ChangeDef : CompProperties
    {


        public string defToChangeTo = "";
        public FactionDef factionToChangeTo = FactionDefOf.Mechanoid;


        public CompProperties_ChangeDef()
        {
            this.compClass = typeof(CompChangeDef);
        }


    }
}