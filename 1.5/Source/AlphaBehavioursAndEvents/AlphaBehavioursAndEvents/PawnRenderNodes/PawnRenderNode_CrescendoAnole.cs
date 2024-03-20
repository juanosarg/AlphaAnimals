
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class PawnRenderNode_CrescendoAnole : PawnRenderNode_AnimalPart
    {
        public PawnRenderNode_CrescendoAnole(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            if (pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AA_AnoleGrown) != null)
            {
                Graphic graphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic;
                return GraphicDatabase.Get<Graphic_Multi>(pawn.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "_Enlarged", ShaderDatabase.Cutout, graphic.drawSize*3, Color.white);
            }

            return base.GraphicFor(pawn);
        }
    }
}
