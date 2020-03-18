using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_SummonOnSpawn : CompProperties
    {


        public string pawnDef = "Pig";
        public List<int> groupMinMax;


        public CompProperties_SummonOnSpawn()
        {
            this.compClass = typeof(CompSummonOnSpawn);
        }


    }
}
