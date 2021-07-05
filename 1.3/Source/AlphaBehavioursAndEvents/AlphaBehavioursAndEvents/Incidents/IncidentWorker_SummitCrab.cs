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
    public class IncidentWorker_SummitCrab : IncidentWorker
    {
        private static readonly IntRange AnimalsCount = new IntRange(1, 1);

        private const float MinTotalBodySize = 4f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef pawnKindDef;
            IntVec3 intVec;
            IntVec3 intVec2;
            return this.TryFindAnimalKind(map.Tile, out pawnKindDef) && this.TryFindStartAndEndCells(map, out intVec, out intVec2) && AlphaAnimalsEvents_Mod.settings.flagSummitCrab;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef pawnKindDef;
            if (!this.TryFindAnimalKind(map.Tile, out pawnKindDef))
            {
                return false;
            }
            IntVec3 intVec;
            IntVec3 near;
            if (!this.TryFindStartAndEndCells(map, out intVec, out near))
            {
                return false;
            }
            Rot4 rot = Rot4.FromAngleFlat((map.Center - intVec).AngleFlat);
            List<Pawn> list = this.GenerateAnimals(pawnKindDef, map.Tile);
            for (int i = 0; i < list.Count; i++)
            {
                Pawn newThing = list[i];
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                GenSpawn.Spawn(newThing, loc, map, rot, WipeMode.Vanish, false);
                newThing.health.AddHediff(HediffDef.Named("AA_CrushingEverything"));
                Find.LetterStack.ReceiveLetter("LetterLabelSummitCrab".Translate(), "SummitCrab".Translate(), LetterDefOf.ThreatBig, newThing, null, null);
            }
            LordMaker.MakeNewLord(null, new LordJob_ExitMapNear(near, LocomotionUrgency.Walk, 12f, false, false), map, list);
            
            return true;
        }

        private bool TryFindAnimalKind(int tile, out PawnKindDef animalKind)
        {
            return (from k in DefDatabase<PawnKindDef>.AllDefs
                    where Find.World.tileTemperatures.SeasonAndOutdoorTemperatureAcceptableFor(tile, ThingDef.Named("AA_SummitCrab")) && k.defName== "AA_SummitCrab"
                    select k).TryRandomElement(out animalKind);
        }

        private bool TryFindStartAndEndCells(Map map, out IntVec3 start, out IntVec3 end)
        {
            if (!RCellFinder.TryFindRandomPawnEntryCell(out start, map, CellFinder.EdgeRoadChance_Animal))
            {
                end = IntVec3.Invalid;
                return false;
            }
            end = IntVec3.Invalid;
            for (int i = 0; i < 8; i++)
            {
                IntVec3 startLocal = start;
                IntVec3 intVec;
                if (!CellFinder.TryFindRandomEdgeCellWith((IntVec3 x) => map.reachability.CanReach(startLocal, x, PathEndMode.OnCell, TraverseMode.NoPassClosedDoors, Danger.Deadly), map, CellFinder.EdgeRoadChance_Ignore, out intVec))
                {
                    break;
                }
                if (!end.IsValid || intVec.DistanceToSquared(start) > end.DistanceToSquared(start))
                {
                    end = intVec;
                }
            }
            return end.IsValid;
        }

        private List<Pawn> GenerateAnimals(PawnKindDef animalKind, int tile)
        {
            int num = IncidentWorker_SummitCrab.AnimalsCount.RandomInRange;
            num = Mathf.Max(num, Mathf.CeilToInt(4f / animalKind.RaceProps.baseBodySize));
            List<Pawn> list = new List<Pawn>();
            for (int i = 0; i < num; i++)
            {
                PawnGenerationRequest request = new PawnGenerationRequest(animalKind, null, PawnGenerationContext.NonPlayer, tile);
                Pawn item = PawnGenerator.GeneratePawn(request);
                list.Add(item);
            }
            return list;
        }
    }
}
