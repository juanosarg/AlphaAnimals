
using UnityEngine;
using Verse;
namespace AlphaBehavioursAndEvents
{
    public class PawnRenderNodeWorker_SpasticScaled : PawnRenderNodeWorker
    {
        public override Vector3 OffsetFor(PawnRenderNode node, PawnDrawParms parms, out Vector3 pivot)
        {
            Vector3 result = base.OffsetFor(node, parms, out pivot);
            PawnRenderNode_SpasticScaled pawnRenderNode_Spastic;
            if ((pawnRenderNode_Spastic = (node as PawnRenderNode_SpasticScaled)) != null && pawnRenderNode_Spastic.CheckAndDoSpasm(parms, out PawnRenderNode_SpasticScaled.SpasmData dat, out float progress))
            {
                result += Vector3.Lerp(dat.offsetStart, dat.offsetTarget, progress);
            }
            return result;
        }

        public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
        {
            Quaternion quaternion = base.RotationFor(node, parms);
            PawnRenderNode_SpasticScaled pawnRenderNode_Spastic;
            if ((pawnRenderNode_Spastic = (node as PawnRenderNode_SpasticScaled)) == null)
            {
                return quaternion;
            }
            float num = 0f;
            PawnRenderNodeProperties_SpasticScaled pawnRenderNodeProperties_Spastic;
            if ((pawnRenderNodeProperties_Spastic = (node.Props as PawnRenderNodeProperties_SpasticScaled)) != null && pawnRenderNodeProperties_Spastic.rotateFacing)
            {
                num += parms.facing.AsAngle;
            }
            if (pawnRenderNode_Spastic.CheckAndDoSpasm(parms, out PawnRenderNode_SpasticScaled.SpasmData dat, out float progress))
            {
                num += Mathf.Lerp(dat.rotationStart, dat.rotationTarget, progress);
            }
            return quaternion * num.ToQuat();
        }

        public override Vector3 ScaleFor(PawnRenderNode node, PawnDrawParms parms)
        {
            Vector3 result = base.ScaleFor(node, parms);
            PawnRenderNode_SpasticScaled pawnRenderNode_Spastic;
            if ((pawnRenderNode_Spastic = (node as PawnRenderNode_SpasticScaled)) != null && pawnRenderNode_Spastic.CheckAndDoSpasm(parms, out PawnRenderNode_SpasticScaled.SpasmData dat, out float progress))
            {
                result *= Mathf.Lerp(dat.scaleStart, dat.scaleTarget, progress);
                result.y = 1f;
            }
            return result;
        }
    }
}