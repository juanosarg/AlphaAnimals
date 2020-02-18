using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class CompHighlyFlammable : ThingComp
    {

        public int flameCounter = 0;




        public CompProperties_HighlyFlammable Props
        {
            get
            {
                return (CompProperties_HighlyFlammable)this.props;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            flameCounter++;
            if (flameCounter >= 50) {
                Pawn pawn = this.parent as Pawn;
                if ((pawn.Map != null) && (pawn.IsBurning()))
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = null;
                    if (pawn != null)
                    {
                        battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(pawn, RulePackDefOf.DamageEvent_Fire, pawn);
                        Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    }
                    DamageDef flame = Named("AA_PermanentBurn");
                    float amount = (float)15;
                    Thing instigator = this.parent;
                    //ThingDef weaponDef = ThingDef.Named("Gun_Autopistol");
                    this.parent.TakeDamage(new DamageInfo(flame, amount, 0f, -1f, instigator, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null)).AssociateWithLog(battleLogEntry_DamageTaken);

                }
                flameCounter = 0;
            }
           
        }
        public static DamageDef Named(string defName)
        {
            return DefDatabase<DamageDef>.GetNamed(defName, true);
        }





    }
}
