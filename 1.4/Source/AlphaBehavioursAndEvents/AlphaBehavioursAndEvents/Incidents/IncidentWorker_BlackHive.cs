using System;
using UnityEngine;
using Verse;
using RimWorld;
using Verse.AI;


namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_BlackHive : IncidentWorker
    {
        private const float HivePoints = 220f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return base.CanFireNowSub(parms) &&  map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("AA_BlackScarab")) && 
                this.TryFindEntryCell(map, out intVec) && AlphaAnimalsEvents_Mod.settings.flagBlackHiveRaids && Find.FactionManager.FirstFactionOfDef(FactionDef.Named("AA_BlackHive"))!=null;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }


        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            int hiveCount = Mathf.Max(GenMath.RoundRandom(parms.points / 220f), 1);
            Thing t = this.SpawnTunnels(hiveCount, map);
            Find.LetterStack.ReceiveLetter("LetterLabelBlackHiveAttack".Translate(), "LetterBlackHiveAttack".Translate(), LetterDefOf.ThreatBig, t, null, null);

            return true;
        }

        private Thing SpawnTunnels(int hiveCount, Map map)
        {
            IntVec3 loc;
            IntVec3 loc2;

            if (!TryFindEntryCell(map,out loc))
            {
                return null;
            }
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_BlackHiveMound"), null), loc, map, WipeMode.FullRefund);
            for (int i = 0; i < hiveCount - 1; i++)
            {
                if (CellFinder.TryFindRandomCellNear(loc, map, 8, (IntVec3 c) => c.Standable(map) &&
                    map.reachability.CanReach(c, thing, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)), out loc2, -1))
                {
                    
                        thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_BlackHiveMound"), null), loc2, map, WipeMode.FullRefund);
                        thing.SetFaction(Find.FactionManager.FirstFactionOfDef(FactionDef.Named("AA_BlackHive")));
                }

               
                    
                
            }
            return thing;
        }
    }
}

