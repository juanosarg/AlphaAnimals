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
            return this.TryFindAnimalKind(map.Tile, out pawnKindDef) && AlphaAnimalsEvents_Mod.settings.flagCactipineDroppods;
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
            Building_Overgrown_DropPod overgrown_DropPod = (Building_Overgrown_DropPod)ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("AA_Overgrown_DropPod", true));

            ActiveDropPodInfo activeDropPodInfo = new ActiveDropPodInfo();
            List<Thing> things = new List<Thing>();
            things.Add(overgrown_DropPod);
            activeDropPodInfo.innerContainer.TryAddRangeOrTransfer(things, true, false);
            activeDropPodInfo.openDelay = 180;
            activeDropPodInfo.leaveSlag = true;
            DropPodUtility.MakeDropPodAt(intVec, map, activeDropPodInfo);
            LookTargets lookie = new LookTargets(intVec,map);
            
            Find.LetterStack.ReceiveLetter("LetterLabelCactipinePod".Translate(), "CactipineDropPod".Translate(), LetterDefOf.NeutralEvent, lookie, null, null);
            //GenSpawn.Spawn(overgrown_DropPod, intVec, map);
            return true;
        }
    }
}
