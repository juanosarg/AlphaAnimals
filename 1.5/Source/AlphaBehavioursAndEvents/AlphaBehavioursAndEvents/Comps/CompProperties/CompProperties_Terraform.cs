using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using VFEInsectoids;

namespace AlphaBehavioursAndEvents
{
     public class CompProperties_Terraform : CompProperties
    {
        public float radius;
        public IntRange spawnTickRate;
        public TerrainDef terrainToLookFor;
        public TerrainDef terrainToSet;
       public CompProperties_Terraform()
        {
            this.compClass = typeof(CompTerraform);
        }
    }
}