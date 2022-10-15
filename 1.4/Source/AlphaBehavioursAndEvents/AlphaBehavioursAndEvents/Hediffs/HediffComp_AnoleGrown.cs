using RimWorld;
using Verse;
using Verse.Sound;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    class HediffComp_AnoleGrown : HediffComp
    {
        private PawnRenderer pawn_renderer;
        public int tickCounter = 0;

        public HediffCompProperties_AnoleGrown Props
        {
            get
            {
                return (HediffCompProperties_AnoleGrown)this.props;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            tickCounter++;

            if (tickCounter >= Props.rateInTicks)
            {
                Pawn pawn = this.parent.pawn as Pawn;

                if (pawn.health != null)
                {
                    if (pawn.health.hediffSet.GetInjuriesTendable() != null && pawn.health.hediffSet.GetInjuriesTendable().Count<Hediff_Injury>() > 0)
                    {
                        foreach (Hediff_Injury injury in pawn.health.hediffSet.GetInjuriesTendable())
                        {
                            injury.Severity = injury.Severity - 0.1f;
                            break;
                        }
                    }
                }
                tickCounter = 0;
            }
        }

        public override void CompPostMake()
        {
            Pawn pawn = this.parent.pawn as Pawn;
            if(pawn.def == InternalDefOf.AA_CrescendoAnole) {
                Pawn_DrawTracker drawtracker = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pawn));
                if (drawtracker != null)
                {
                    this.pawn_renderer = drawtracker.renderer;
                }
                Vector2 vector = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize * 3;

                LongEventHandler.ExecuteWhenFinished(delegate
                {
                    if (this.pawn_renderer != null)
                    {
                        try
                        {
                            Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(pawn.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "_Enlarged", ShaderDatabase.Cutout, vector, Color.white);
                            this.pawn_renderer.graphics.ResolveAllGraphics();
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                            (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;


                        }
                        catch (NullReferenceException) { }
                    }

                });

            }
           

        }

        public override void CompPostPostRemoved()
        {
            ReturnToOriginalGraphics();
            Pawn pawn = this.parent.pawn as Pawn;
            BodyPartRecord part = pawn.RaceProps.body.GetPartsWithDef(BodyPartDefOf.Body).FirstOrDefault();
            pawn.health.AddHediff(HediffDef.Named("AA_AnoleGrownExhausted"), part);
        }

        public override void Notify_PawnDied()
        {
            ReturnToOriginalGraphics();
        }

        public void ReturnToOriginalGraphics()
        {
            Pawn pawn = this.parent.pawn as Pawn;
            Vector2 vector = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;

            LongEventHandler.ExecuteWhenFinished(delegate
            {
                if (this.pawn_renderer != null)
                {
                    try
                    {
                        Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(pawn.ageTracker.CurKindLifeStage.bodyGraphicData.texPath, ShaderDatabase.Cutout, vector, Color.white);
                        this.pawn_renderer.graphics.ResolveAllGraphics();
                        this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                        (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;


                    }
                    catch (NullReferenceException) { }
                }

            });
        }
    }
}
