
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class PawnRenderNode_ArcticLion : PawnRenderNode_AnimalPart
    {
        public PawnRenderNode_ArcticLion(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            if (pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AA_InvisibleArcticLion)!=null)
            {
                Graphic graphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic;
                return GraphicDatabase.Get<Graphic_Multi>("Things/Pawn/Animal/AA_ArcticLion/AA_ArcticLion_Invisible", ShaderDatabase.Cutout, graphic.drawSize, Color.white);
            }
            
            return base.GraphicFor(pawn);
        }
    }
}
