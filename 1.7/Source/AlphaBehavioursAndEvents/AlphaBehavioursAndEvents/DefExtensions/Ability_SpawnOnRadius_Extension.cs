using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class Ability_SpawnOnRadius_Extension : DefModExtension
    {
        public ThingDef thingToSpawn;
        public float probability;
        public bool allowAnAmount = false;
        public int allowedAmount;
    }
}
