using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{

    public class DamageWorker_SwallowWhole : DamageWorker_Cut
    {


        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
            Pawn_SwallowWhole attacker = dinfo.Instigator as Pawn_SwallowWhole;
            if (attacker != null)
            {
                attacker.needs.food.CurLevel += 0.3f;
                if (attacker.innerContainer.Count < 10)
                {
                    HealthUtility.DamageUntilDowned(pawn, true);
                    attacker.TryAcceptThing(pawn);
                }
            }
            

        }
    }
}
