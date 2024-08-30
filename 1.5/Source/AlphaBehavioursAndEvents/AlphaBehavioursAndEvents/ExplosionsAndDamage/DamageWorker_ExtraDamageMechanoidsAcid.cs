using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{

    public class DamageWorker_ExtraDamageMechanoidsAcid : DamageWorker_Cut
    {


        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
            if (pawn.RaceProps.FleshType == FleshTypeDefOf.Mechanoid) {
                float num = 0.01f * totalDamage;
                Hediff hediff = HediffMaker.MakeHediff(InternalDefOf.AA_AcidBuildup_AgainstMechs, pawn);
                hediff.Severity = num;
                pawn.health.AddHediff(hediff, null, dinfo);
            }

        }



    }
}