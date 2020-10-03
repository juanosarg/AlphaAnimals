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

                   
                    if (pawn.Faction.IsPlayer && (pawn.Position.GetTerrain(pawn.Map) == TerrainDef.Named(Props.FirstStageTerrain)))
                    {
                        pawn.Map.terrainGrid.SetTerrain(pawn.Position, TerrainDef.Named(Props.SecondStageTerrain));
                        pawn.health.AddHediff(HediffDef.Named("AA_FertilizedTerrain"));
                    }

                    extraFertCounter--;
                    if (extraFertCounter <= 0)
                    {
                        if (pawn.Faction.IsPlayer && pawn.training.HasLearned(TrainableDefOf.Obedience) && ((pawn.Position.GetTerrain(pawn.Map) == TerrainDef.Named(Props.SecondStageTerrain))))
                        {
                            pawn.Map.terrainGrid.SetTerrain(pawn.Position, TerrainDef.Named(Props.ThirdStageTerrain));
                            pawn.health.AddHediff(HediffDef.Named("AA_FertilizedTerrain"));
                        }
                        extraFertCounter = 500;
                    }
                    

                }
               
                

            }
        }

       




    }
}

