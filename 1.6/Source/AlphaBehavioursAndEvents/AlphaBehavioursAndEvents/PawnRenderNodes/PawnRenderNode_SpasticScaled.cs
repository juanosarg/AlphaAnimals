
using System.Linq;
using UnityEngine;
using Verse;
namespace AlphaBehavioursAndEvents
{
    public class PawnRenderNode_SpasticScaled : PawnRenderNode_Spastic
    {
       

        public PawnRenderNode_SpasticScaled(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

        public override GraphicMeshSet MeshSetFor(Pawn pawn)
        {
            return new GraphicMeshSet(MeshPool.GridPlane(props.overrideMeshSize ?? props.drawSize * pawn.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize/
                pawn.kindDef.lifeStages.Last().bodyGraphicData.drawSize));
        }

        
    }
}