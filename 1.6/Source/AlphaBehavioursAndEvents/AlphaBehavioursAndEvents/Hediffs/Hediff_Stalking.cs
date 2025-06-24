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


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.initialHediffCount, "initialHediffCount", 1);

        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            base.PostAdd(dinfo);
            List<Hediff> hediffs = this.pawn.health.hediffSet.hediffs;

            initialHediffCount = hediffs.Count;



        }

        public override void Tick()
        {
            base.Tick();
            if (pawn.Map != null && this.pawn.IsHashIntervalTick(20))
            {
                List<Hediff> hediffs = this.pawn.health.hediffSet.hediffs;

                if (hediffs.Count > initialHediffCount)
                {
                    this.pawn.Drawer.renderer.SetAllGraphicsDirty();
                    this.pawn.health.RemoveHediff(this);
                }
            }


        }

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            this.pawn.health.RemoveHediff(this);
        }



    }
}
