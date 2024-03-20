using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    class CompProperties_LarvaeToCocoon : CompProperties
    {
        public CompProperties_LarvaeToCocoon()
        {
            this.compClass = typeof(CompLarvaeToCocoon);
        }

        public int ticksBeforeBecomingCocoon = 5000;
    }
}
