using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{

        public class DamageWorker_ExtraInfecter : DamageWorker_Cut
        {
           

            protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
            {
                base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
                Random random = new Random();
                
                if (random.NextDouble() > ((float)(100 - dinfo.Instigator.TryGetComp<CompInfecter>().GetChance) / 100)){
                    pawn.health.AddHediff(HediffDefOf.WoundInfection, dinfo.HitPart, null, null);
                }

            }
        }
}
