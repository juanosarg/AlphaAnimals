using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
using System.Reflection;


namespace AlphaBehavioursAndEvents
{
    public class Hediff_Stalking : HediffWithComps
    {
        public int initialHediffCount;
        private PawnRenderer pawn_renderer;


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.initialHediffCount, "initialHediffCount", 1);
            
        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            base.PostAdd(dinfo);
            List<Hediff> hediffs = this.pawn.health.hediffSet.hediffs;
            Vector2 vector = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;
            initialHediffCount = hediffs.Count;
            Pawn_DrawTracker drawtracker = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pawn));
            if (drawtracker != null)
            {
                this.pawn_renderer = drawtracker.renderer;
            }
            LongEventHandler.ExecuteWhenFinished(delegate
            {
                if (this.pawn_renderer != null)
                {
                    try
                    {
                        Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>("Things/Pawn/Animal/AA_ArcticLion/AA_ArcticLion_Invisible", ShaderDatabase.Cutout, vector, Color.white);                    
                        this.pawn_renderer.graphics.ResolveAllGraphics();
                        this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                        (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                    }
                    catch (NullReferenceException) { }
                }

            });

        }

        public override void Tick()
        {
            base.Tick();
            if (pawn.Map != null) {
                List<Hediff> hediffs = this.pawn.health.hediffSet.hediffs;

                if (hediffs.Count > initialHediffCount)
                {
                    ReturnToOriginalGraphics();
                    this.pawn.health.RemoveHediff(this);
                }
            }
            

        }

        public override void Notify_PawnDied()
        {
            base.Notify_PawnDied();
            ReturnToOriginalGraphics();        }

        public void ReturnToOriginalGraphics()
        {
           
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
