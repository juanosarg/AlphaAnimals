
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_LuciferiumExplosion : DeathActionWorker
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
                radius = 1.9f;
            }
            else
            {
                radius = 1.9f;
            }



            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DefDatabase<DamageDef>.GetNamed("AA_LuciferiumExplosion"), corpse.InnerPawn, 30, -1, SoundDef.Named("AA_GooPop"), null, null, null, ThingDef.Named("AA_Filth_RedSlime"), 1f, 1, null, false, null, 0f, 1);
        }


    }
}
