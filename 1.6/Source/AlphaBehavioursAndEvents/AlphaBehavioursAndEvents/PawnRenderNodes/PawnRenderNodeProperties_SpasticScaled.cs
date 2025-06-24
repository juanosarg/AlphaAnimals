
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class PawnRenderNodeProperties_SpasticScaled : PawnRenderNodeProperties_Spastic
    {
 


        public PawnRenderNodeProperties_SpasticScaled()
        {
            nodeClass = typeof(PawnRenderNode_SpasticScaled);
            workerClass = typeof(PawnRenderNodeWorker_SpasticScaled);
        }
    }
}
