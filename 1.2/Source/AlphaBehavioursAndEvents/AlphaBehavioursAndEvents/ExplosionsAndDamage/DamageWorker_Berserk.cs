using RimWorld;
using System;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class DamageWorker_Berserk : DamageWorker_AddInjury
    {
        public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            Pawn pawn = victim as Pawn;
            if (pawn != null && pawn.Faction == Faction.OfPlayer)
            {
                Find.TickManager.slower.SignalForceNormalSpeedShort();
                if (!pawn.Dead)
                {
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                }
            }
            
            DamageWorker.DamageResult damageResult = base.Apply(dinfo, victim);
           
           
            return damageResult;
        }

       
    }
}

