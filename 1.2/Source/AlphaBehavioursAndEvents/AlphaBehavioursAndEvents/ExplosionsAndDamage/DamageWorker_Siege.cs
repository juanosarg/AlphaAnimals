using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
   
    public class DamageWorker_Siege : DamageWorker_AddInjury
    {
      

        public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            Building wall = victim as Building;
            if ((wall != null)&&((wall.def.building.isNaturalRock) || (wall.def == ThingDefOf.Wall)))
            {
               
                //Log.Message("wall hit");
                DamageWorker.DamageResult damageResult = new DamageWorker.DamageResult();
                if (victim.SpawnedOrAnyParentSpawned)
                {
                    ImpactSoundUtility.PlayImpactSound(victim, dinfo.Def.impactSoundType, victim.MapHeld);
                }
                if (victim.def.useHitPoints && dinfo.Def.harmsHealth)
                {
                    damageResult.totalDamageDealt = Mathf.Min((float)victim.HitPoints, dinfo.Amount*8);
                    victim.HitPoints -= (int)damageResult.totalDamageDealt;
                    if (victim.HitPoints <= 0)
                    {
                        victim.HitPoints = 0;
                        victim.Kill(new DamageInfo?(dinfo), null);
                    }
                }
                return damageResult;

            }
            else return base.Apply(dinfo,victim);
            
        }

       
    }
}
