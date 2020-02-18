using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompFertilizer : ThingComp
    {

        public int extraFertCounter = 500;
      



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
                if (pawn.Faction != null) {

                   
                    if (pawn.Faction.IsPlayer && (pawn.Position.GetTerrain(pawn.Map) == TerrainDef.Named("Sand")))
                    {
                        pawn.Map.terrainGrid.SetTerrain(pawn.Position, TerrainDef.Named("AA_FertilizedSand"));
                    }

                    extraFertCounter--;
                    if (extraFertCounter <= 0)
                    {
                        if (pawn.Faction.IsPlayer && pawn.training.HasLearned(TrainableDefOf.Obedience) && ((pawn.Position.GetTerrain(pawn.Map) == TerrainDef.Named("AA_FertilizedSand"))))
                        {
                            pawn.Map.terrainGrid.SetTerrain(pawn.Position, TerrainDef.Named("AA_SuperiorFertilizedSand"));
                        }
                        extraFertCounter = 500;
                    }

                }
               
                

            }
        }

       




    }
}

