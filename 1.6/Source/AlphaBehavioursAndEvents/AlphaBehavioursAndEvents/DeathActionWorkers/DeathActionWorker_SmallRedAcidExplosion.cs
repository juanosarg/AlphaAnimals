﻿
using RimWorld;
using Verse;
using Verse.AI.Group;
namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_SmallRedAcidExplosion : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse, Lord previousLord)
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



            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, InternalDefOf.AA_AcidSpit, corpse.InnerPawn, 10, -1, InternalDefOf.AA_GooPop, null, null, null, InternalDefOf.AA_Filth_RedSlime, 1f, 1, null, null, 255, false, null, 0f, 1);
        }


    }
}
