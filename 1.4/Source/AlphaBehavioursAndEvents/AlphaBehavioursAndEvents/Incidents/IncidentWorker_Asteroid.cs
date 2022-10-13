using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_Asteroid : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return this.TryFindCell(out intVec, map) && AlphaAnimalsEvents_Mod.settings.flagAsteroids; ;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            if (!this.TryFindCell(out intVec, map))
            {
                return false;
            }
            List<Thing> list = DefDatabase<ThingSetMakerDef>.GetNamed("AA_Meteorite").root.Generate();
            SkyfallerMaker.SpawnSkyfaller(ThingDefOf.MeteoriteIncoming, list, intVec, map);
            Find.LetterStack.ReceiveLetter("LetterLabelSkyAsteroid".Translate(), "LetterSkyAsteroid".Translate(), LetterDefOf.NeutralEvent, new TargetInfo(intVec, map, false), null, null);

           
            return true;
        }

        private bool TryFindCell(out IntVec3 cell, Map map)
        {
            int maxMineables = ThingSetMaker_SpecialMeteorite.MineablesCountRange.max;
            return CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map, out cell, 10, default(IntVec3), -1, true, false, false, false, true, true, delegate (IntVec3 x)
            {
                int num = Mathf.CeilToInt(Mathf.Sqrt((float)maxMineables)) + 2;
                CellRect cellRect = CellRect.CenteredOn(x, num, num);
                int num2 = 0;
                foreach (IntVec3 current in cellRect)
                {
                    if (current.InBounds(map) && current.Standable(map))
                    {
                        num2++;
                    }
                }
                return num2 >= maxMineables;
            });
        }
    }
}

