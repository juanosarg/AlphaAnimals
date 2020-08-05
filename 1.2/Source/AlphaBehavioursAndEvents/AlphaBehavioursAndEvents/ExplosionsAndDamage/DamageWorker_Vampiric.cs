using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{

    public class DamageWorker_Vampiric : DamageWorker_Stab
    {


        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
            Pawn attacker = dinfo.Instigator as Pawn;
            if (attacker != null)
            {
                if (attacker.health != null)
                {
                    if (attacker.health.hediffSet.GetInjuriesTendable() != null && attacker.health.hediffSet.GetInjuriesTendable().Count<Hediff_Injury>() > 0)
                    {
                        foreach (Hediff_Injury injury in attacker.health.hediffSet.GetInjuriesTendable())
                        {
                            injury.Severity = injury.Severity - 15f;
                            break;
                        }
                    }
                }
            }
        }
    }
}