using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompFertilizer : ThingComp
    {


      



        public CompProperties_Fertilizer Props
        {
            get
            {
                return (CompProperties_Fertilizer)this.props;
            }
        }


        public override void CompTick()
        {
            Pawn pawn = this.parent as Pawn;

            if (pawn.Spawned)
            {
              if (pawn.Position.GetTerrain(pawn.Map) == TerrainDef.Named("Sand"))
                {
                    pawn.Map.terrainGrid.SetTerrain(pawn.Position, TerrainDef.Named("AA_FertilizedSand"));
                }
                

            }
        }


    }
}

