using System;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class HediffCompProperties_AnoleGrown : HediffCompProperties
    {
        public int rateInTicks = 100;

        public HediffCompProperties_AnoleGrown()
        {
            this.compClass = typeof(HediffComp_AnoleGrown);
        }
    }
}
