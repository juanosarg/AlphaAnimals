using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{

    public class DamageWorker_PlaguedBite : DamageWorker_Bite
    {


        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
            if (pawn?.IsGhoul !=false && pawn?.RaceProps.IsMechanoid != false)
            {

                float num = (totalDamage * 0.01f) * Mathf.Max(1f - pawn.GetStatValue(StatDefOf.ToxicResistance), 0f);
    
                if (num >= 0f)
                {
                    Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.Plague, pawn);
                    hediff.Severity = num;
                    pawn.health.AddHediff(hediff, null, dinfo);
                  
                }


            }

        }


    }
}
