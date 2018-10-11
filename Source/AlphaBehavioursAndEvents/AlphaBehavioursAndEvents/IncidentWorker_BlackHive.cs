using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_BlackHive : IncidentWorker
    {
        private const float HivePoints = 220f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return base.CanFireNowSub(parms) && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("AA_BlackScarab")) && this.TryFindEntryCell(map, out intVec);
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, null);
        }


        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            int hiveCount = Mathf.Max(GenMath.RoundRandom(parms.points / 220f), 1);
            Thing t = this.SpawnTunnels(hiveCount, map);
            base.SendStandardLetter(t, null, new string[0]);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }

        private Thing SpawnTunnels(int hiveCount, Map map)
        {
            IntVec3 loc;
            if (!TryFindEntryCell(map,out loc))
            {
                return null;
            }
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_BlackHiveMound"), null), loc, map, WipeMode.FullRefund);
            for (int i = 0; i < hiveCount - 1; i++)
            {
                Predicate<IntVec3> validator = (IntVec3 c) => DropCellFinder.IsGoodDropSpot(loc, map, false, false);
                if (CellFinder.TryFindRandomCellNear(loc, map, 8, validator, out loc, -1))
                {
                    thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_BlackHiveMound"),null), loc, map, WipeMode.FullRefund);
                }

               
                    
                
            }
            return thing;
        }
    }
}

