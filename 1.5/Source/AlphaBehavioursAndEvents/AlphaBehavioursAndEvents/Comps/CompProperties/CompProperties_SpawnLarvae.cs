using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    class CompProperties_SpawnLarvae : CompProperties
    {
        public CompProperties_SpawnLarvae()
        {
            this.compClass = typeof(CompSpawnLarvae);
        }

        public int ticksBetweenSpawn = 10000;     

        public int maxNumber = 4;
    }
}
