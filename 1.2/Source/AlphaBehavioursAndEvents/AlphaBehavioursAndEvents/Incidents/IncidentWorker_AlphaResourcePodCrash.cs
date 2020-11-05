using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_AlphaResourcePodCrash : IncidentWorker
    {




        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            List<Thing> things = DefDatabase<ThingSetMakerDef>.GetNamed("AA_AlphaResourcePod").root.Generate();
            IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
            DropPodUtility.DropThingsNear(intVec, map, things, 110, false, true, true, true);
            base.SendStandardLetter("AA_LetterLabelAlphaCargoPodCrash".Translate(), "AA_AlphaCargoPodCrash".Translate(), LetterDefOf.PositiveEvent, parms, new TargetInfo(intVec, map, false), Array.Empty<NamedArgument>());
            return true;
        }
    }
}