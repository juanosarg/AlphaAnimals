﻿

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Text;

namespace AlphaBehavioursAndEvents
{
    class HediffCompProperties_Resurrect : HediffCompProperties
    {

       
        public int livesLeft = 1;

        public HediffCompProperties_Resurrect()
        {
            this.compClass = typeof(HediffComp_Resurrect);
        }
    }
}
