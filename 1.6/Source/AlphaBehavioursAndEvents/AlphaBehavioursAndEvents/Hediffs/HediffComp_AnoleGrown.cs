using RimWorld;
using Verse;
using Verse.Sound;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VEF.AnimalBehaviours;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    class HediffComp_AnoleGrown : HediffComp
    {

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
                    if (Utils.GetInjuriesTendable(pawn.health.hediffSet.hediffs) != null && Utils.GetInjuriesTendable(pawn.health.hediffSet.hediffs).Count() > 0)
                    {
                        foreach (Hediff_Injury injury in Utils.GetInjuriesTendable(pawn.health.hediffSet.hediffs))
                        {
                            injury.Severity = injury.Severity - 0.1f;
                            break;
                        }
                    }
                }
                tickCounter = 0;
            }
        }
        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);
            this.parent.pawn.Drawer.renderer.SetAllGraphicsDirty();

        }
       
        public override void CompPostPostRemoved()
        {

            Pawn pawn = this.parent.pawn as Pawn;
            pawn.Drawer.renderer.SetAllGraphicsDirty();
            BodyPartRecord part = pawn.RaceProps.body.GetPartsWithDef(InternalDefOf.Body).FirstOrDefault();
            pawn.health.AddHediff(InternalDefOf.AA_AnoleGrownExhausted, part);
        }


    }
}
