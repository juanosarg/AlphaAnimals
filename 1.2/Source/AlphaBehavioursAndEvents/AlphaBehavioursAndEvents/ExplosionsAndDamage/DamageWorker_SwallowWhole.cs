using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse.Sound;
using Verse;

namespace AlphaBehavioursAndEvents
{

    public class DamageWorker_SwallowWhole : DamageWorker_Cut
    {


        protected override void ApplySpecialEffectsToPart(Pawn pawn, float totalDamage, DamageInfo dinfo, DamageWorker.DamageResult result)
        {
            base.ApplySpecialEffectsToPart(pawn, totalDamage, dinfo, result);
            Pawn_SwallowWhole attacker = dinfo.Instigator as Pawn_SwallowWhole;
            if (attacker != null && attacker.Map != null && !pawn.Downed && !pawn.Dead && pawn.def.defName!= "AA_PhoenixOwlcat")
            {
                
                if (attacker.innerContainer.Count < 5)
                {
                    attacker.needs.food.CurLevel = attacker.needs.food.MaxLevel;
                    HealthUtility.DamageUntilDowned(pawn);
                    SoundDef.Named("AA_MatureFleshbeastSwallow").PlayOneShot(new TargetInfo(attacker.Position, attacker.Map, false));
                    if (pawn!=null&&pawn.Faction!=null &&pawn.Faction.IsPlayer)
                    {
                        Find.LetterStack.ReceiveLetter("AA_LetterLabelMatureFleshbeast".Translate(), "AA_LetterMatureFleshbeast".Translate(pawn), LetterDefOf.ThreatBig, attacker, null, null);
                    }
                    attacker.TryAcceptThing(pawn);
                    
                }
            }
            

        }
    }
}
