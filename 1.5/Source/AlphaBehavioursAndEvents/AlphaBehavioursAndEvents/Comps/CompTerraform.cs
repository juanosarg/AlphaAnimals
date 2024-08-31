using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using VFEInsectoids;

namespace AlphaBehavioursAndEvents
{


    public class CompTerraform : CompAOE_Cell
    {
        public CompProperties_Terraform Props => base.props as CompProperties_Terraform;
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                parent.Map.terrainGrid.SetTerrain(parent.Position, Props.terrainToSet);
            }
        }

        protected override bool CellValidator(IntVec3 cell)
        {
            var result = cell.GetTerrain(parent.Map) is TerrainDef terrain && terrain == Props.terrainToLookFor
                && cell.GetEdifice(parent.Map) is null;
          
            return result;
        }


        protected override bool TryGetCell(List<IntVec3> cells, out IntVec3 cell)
        {
            cell = cells.OrderBy(x => x.DistanceTo(parent.Position)).FirstOrDefault();
            return cell != default;
        }

        protected override void DoEffect(IntVec3 cell)
        {
            parent.Map.terrainGrid.SetTerrain(cell, Props.terrainToSet);
        }
    }
}
