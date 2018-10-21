using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class Hediff_Stalking : HediffWithComps
    {
        public int initialHediffCount;
        Graphic storeGraphic;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.initialHediffCount, "initialHediffCount", 1);
            Scribe_Values.Look<Graphic>(ref this.storeGraphic, "storeGraphic", null);

        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            base.PostAdd(dinfo);
            List<Hediff> hediffs = this.pawn.health.hediffSet.hediffs;
            initialHediffCount = hediffs.Count;
            storeGraphic = this.pawn.Drawer.renderer.graphics.nakedGraphic;
            Graphic newGraphic = GraphicDatabase.Get<Graphic_Single>("Things/Pawn/Animal/AA_ArcticLion/AA_ArcticLion_Invisible", ShaderDatabase.Cutout, this.pawn.Drawer.renderer.graphics.nakedGraphic.drawSize, Color.white);

            this.pawn.Drawer.renderer.graphics.nakedGraphic = newGraphic;
        }



        public override void Tick()
        {
            base.Tick();
            List<Hediff> hediffs = this.pawn.health.hediffSet.hediffs;

            if (hediffs.Count > initialHediffCount)
            {
                this.pawn.Drawer.renderer.graphics.nakedGraphic = storeGraphic;
                this.pawn.health.RemoveHediff(this);
            }

        }

        public override void Notify_PawnDied()
        {
            base.Notify_PawnDied();
            this.pawn.Drawer.renderer.graphics.nakedGraphic = storeGraphic;
        }


    }
}
