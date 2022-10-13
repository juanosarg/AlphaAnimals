
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_AcidExplosion : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse)
        {
            float radius;
            if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
            {
                radius = 1.9f;
            }
            else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
            {
                radius = 2.9f;
            }
            else
            {
                radius = 3.9f;
            }



            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DefDatabase<DamageDef>.GetNamed("AA_AcidSpit"), corpse.InnerPawn, 10, -1, SoundDef.Named("AA_GooPop"), null, null, null, ThingDef.Named("AA_Filth_Slime"), 1f, 1,null, false, null, 0f, 1);
        }


    }
}
