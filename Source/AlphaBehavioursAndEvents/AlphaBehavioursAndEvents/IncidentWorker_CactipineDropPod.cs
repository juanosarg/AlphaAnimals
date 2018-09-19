using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_CactipineDropPod : IncidentWorker
    {
       

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef pawnKindDef;
            return this.TryFindAnimalKind(map.Tile, out pawnKindDef);
        }

        private bool TryFindAnimalKind(int tile, out PawnKindDef animalKind)
        {
            return (from k in DefDatabase<PawnKindDef>.AllDefs
                    where Find.World.tileTemperatures.SeasonAndOutdoorTemperatureAcceptableFor(tile, ThingDef.Named("AA_Cactipine")) && k.defName == "AA_Cactipine"
                    select k).TryRandomElement(out animalKind);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec = DropCellFinder.RandomDropSpot(map);

            ActiveDropPodInfo activeDropPodInfo = new ActiveDropPodInfo();
            activeDropPodInfo.leaveSlag = true;
            DropPodUtility.MakeDropPodAt(intVec, map, activeDropPodInfo);
            Building overgrown_DropPod = (Building)ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("AA_Overgrown_DropPod", true));
            GenSpawn.Spawn(overgrown_DropPod, intVec, map);
            Find.LetterStack.ReceiveLetter("LetterLabelCactipinePod".Translate(), "CactipineDropPod".Translate(), LetterDefOf.NeutralEvent, overgrown_DropPod, null, null);

            return true;
        }
    }
}
