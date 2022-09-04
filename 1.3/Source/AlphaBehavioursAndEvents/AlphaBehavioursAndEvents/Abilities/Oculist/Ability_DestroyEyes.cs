using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;
using System.Linq;

namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_DestroyEyes : Ability
    {

        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo target in targets)
            {
                Pawn pawn = target.Thing as Pawn;
                List<BodyPartRecord> eyes = (from c in pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null)
                                             where c.def == BodyPartDefOf.Eye
                                             select c).ToList();

                              
                if (eyes != null && eyes.Count>0)
                {
                    foreach(BodyPartRecord eye in eyes)
                    {
                        DamageInfo damageInfo = new DamageInfo(DamageDefOf.Burn, 1000, 999f, -1f, this.pawn, eye, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true);
                        damageInfo.SetAllowDamagePropagation(false);
                        pawn.TakeDamage(damageInfo);
                    } 

                    
                }


            }
        }

       
    }
}