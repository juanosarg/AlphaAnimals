
using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class PawnRenderNode_AlternatesWithVariations : PawnRenderNode_AnimalPart
    {
        public PawnRenderNode_AlternatesWithVariations(Pawn pawn, PawnRenderNodeProperties_WithIndex props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            
            if (AlphaAnimalsEvents_Mod.settings.alternatePokemonGraphics)
            {
                PawnKindLifeStage curKindLifeStage = pawn.ageTracker.CurKindLifeStage;
                Graphic OGgraphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic;
                PawnRenderNodeProperties_WithIndex indexProps = this.Props as PawnRenderNodeProperties_WithIndex;

                Graphic graphic = GraphicDatabase.Get<Graphic_Multi>("Things/Pawn/Animal_Alternate/"+pawn.def.defName+"/"+ pawn.def.defName+ Rand.Range(1, indexProps.index+1), OGgraphic.Shader, OGgraphic.drawSize, OGgraphic.color);
                if ((pawn.Dead || (pawn.IsMutant && pawn.mutant.Def.useCorpseGraphics)) && curKindLifeStage.corpseGraphicData != null)
                {
                    graphic = ((pawn.gender == Gender.Female && curKindLifeStage.femaleCorpseGraphicData != null) ? curKindLifeStage.femaleCorpseGraphicData.Graphic.GetColoredVersion(curKindLifeStage.femaleCorpseGraphicData.Graphic.Shader, graphic.Color, graphic.ColorTwo) : curKindLifeStage.corpseGraphicData.Graphic.GetColoredVersion(curKindLifeStage.corpseGraphicData.Graphic.Shader, graphic.Color, graphic.ColorTwo));
                }
                switch (pawn.Drawer.renderer.CurRotDrawMode)
                {
                    case RotDrawMode.Fresh:
                        if (ModsConfig.AnomalyActive && pawn.IsMutant && pawn.mutant.HasTurned)
                        {
                            return graphic.GetColoredVersion(ShaderDatabase.Cutout, MutantUtility.GetSkinColor(pawn, graphic.Color).Value, MutantUtility.GetSkinColor(pawn, graphic.ColorTwo).Value);
                        }
                        return graphic;
                    case RotDrawMode.Rotting:
                        return graphic.GetColoredVersion(ShaderDatabase.Cutout, PawnRenderUtility.GetRottenColor(graphic.Color), PawnRenderUtility.GetRottenColor(graphic.ColorTwo));
                    case RotDrawMode.Dessicated:
                        if (curKindLifeStage.dessicatedBodyGraphicData != null)
                        {
                            Graphic graphic2;
                            if (pawn.RaceProps.FleshType != FleshTypeDefOf.Insectoid)
                            {
                                graphic2 = ((pawn.gender == Gender.Female && curKindLifeStage.femaleDessicatedBodyGraphicData != null) ? curKindLifeStage.femaleDessicatedBodyGraphicData.GraphicColoredFor(pawn) : curKindLifeStage.dessicatedBodyGraphicData.GraphicColoredFor(pawn));
                            }
                            else
                            {
                                Color dessicatedColorInsect = PawnRenderUtility.DessicatedColorInsect;
                                graphic2 = ((pawn.gender == Gender.Female && curKindLifeStage.femaleDessicatedBodyGraphicData != null) ? curKindLifeStage.femaleDessicatedBodyGraphicData.Graphic.GetColoredVersion(ShaderDatabase.Cutout, dessicatedColorInsect, dessicatedColorInsect) : curKindLifeStage.dessicatedBodyGraphicData.Graphic.GetColoredVersion(ShaderDatabase.Cutout, dessicatedColorInsect, dessicatedColorInsect));
                            }
                            if (pawn.IsMutant)
                            {
                                graphic2.ShadowGraphic = graphic.ShadowGraphic;
                            }
                        
                            return graphic2;
                        }
                        break;
                }



                }

            return base.GraphicFor(pawn);
        }
    }
}
