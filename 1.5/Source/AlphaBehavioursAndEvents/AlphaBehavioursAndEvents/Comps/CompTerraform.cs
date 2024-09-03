using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;


namespace AlphaBehavioursAndEvents
{


    public class CompTerraform : ThingComp
    {
        public CompProperties_Terraform Props => base.props as CompProperties_Terraform;
        public int nextTickEffect;
        protected virtual bool Active => parent.Spawned;
        public int NextTickEffect => Find.TickManager.TicksGame + Props.spawnTickRate.RandomInRange;
     
       

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref nextTickEffect, "nextPlantSpawn");
        }


        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
           
            if (!respawningAfterLoad)
            {
                parent.Map.terrainGrid.SetTerrain(parent.Position, Props.terrainToSet);
            }
        }

        protected bool CellValidator(IntVec3 cell)
        {
            var result = cell.GetTerrain(parent.Map) is TerrainDef terrain && terrain == Props.terrainToLookFor
                && cell.GetEdifice(parent.Map) is null;
          
            return result;
        }


        protected bool TryGetCell(List<IntVec3> cells, out IntVec3 cell)
        {
            cell = cells.OrderBy(x => x.DistanceTo(parent.Position)).FirstOrDefault();
            return cell != default;
        }

        protected void DoEffect(IntVec3 cell)
        {
            parent.Map.terrainGrid.SetTerrain(cell, Props.terrainToSet);
        }

        public override void CompTick()
        {
            base.CompTick();
            if (Active)
            {
                if (nextTickEffect == 0)
                {
                    nextTickEffect = NextTickEffect;
                }
                if (Find.TickManager.TicksGame >= nextTickEffect)
                {
                    var cells = GetCells();
                    if (TryGetCell(cells, out var cell))
                    {
                        DoEffect(cell);
                    }
                    nextTickEffect = NextTickEffect;
                }
            }
        }

      
        protected virtual List<IntVec3> GetCells()
        {
            return GenRadial.RadialCellsAround(parent.Position, Props.radius, true)
                .Where(cell => cell.InBounds(parent.Map) && CellValidator(cell)).ToList();
        }

       


    }
}
