
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_GargantuanExplosion : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse)
        {
            float radius;
            if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
            {
                radius = 6.9f;
            }
            else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
            {
                radius = 8.9f;
            }
            else
            {
                radius = 12.9f;
            }
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1, -1, null, null, null, null, null, 0f, 1, null, false, null, 0f, 1);
        }


    }
}

