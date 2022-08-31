using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    class CompProperties_Cocoon : CompProperties
    {
        public CompProperties_Cocoon()
        {
            this.compClass = typeof(CompCocoon);
        }
        public int ticksToSpawn=5000;
    }
}
