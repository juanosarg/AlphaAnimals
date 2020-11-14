using System;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class HediffCompProperties_Mime : HediffCompProperties
    {
        public int malnutritionTrigger = 1;
        public string turnTo = "";

        public HediffCompProperties_Mime()
        {
            this.compClass = typeof(HediffComp_Mime);
        }
    }
}