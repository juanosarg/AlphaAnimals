using Verse;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_HediffEffecter : CompProperties
    {


        public int radius = 1;
        public float severity = 1.0f;
        public int tickInterval = 1000;
        public string hediff = "Plague";



        public CompProperties_HediffEffecter()
        {
            this.compClass = typeof(CompHediffEffecter);
        }


    }
}
