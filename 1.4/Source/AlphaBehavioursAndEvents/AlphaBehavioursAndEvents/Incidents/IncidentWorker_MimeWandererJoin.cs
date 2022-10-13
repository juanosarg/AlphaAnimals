using System;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class IncidentWorker_MimeWandererJoin : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            return this.CanSpawnJoiner(map) && AlphaAnimalsEvents_Mod.settings.flagMime;
        }

        public virtual Pawn GeneratePawn()
        {
            Gender? fixedGender = null;
            if (this.def.pawnFixedGender != Gender.None)
            {
                fixedGender = new Gender?(this.def.pawnFixedGender);
            }
            return PawnGenerator.GeneratePawn(new PawnGenerationRequest(this.def.pawnKind, Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, true, false, false, false, this.def.pawnMustBeCapableOfViolence, 20f));
        }

        public virtual bool CanSpawnJoiner(Map map)
        {
            IntVec3 intVec;
            return this.TryFindEntryCell(map, out intVec);
        }

        public virtual void SpawnJoiner(Map map, Pawn pawn)
        {
            IntVec3 loc;
            this.TryFindEntryCell(map, out loc);
            GenSpawn.Spawn(pawn, loc, map, WipeMode.Vanish);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (!this.CanSpawnJoiner(map))
            {
                return false;
            }
            Pawn pawn = this.GeneratePawn();
            this.SpawnJoiner(map, pawn);
            pawn.health.AddHediff(HediffDef.Named("AA_MimeHediff"), null, null, null);
            TaggedString baseLetterText =  this.def.letterText.Formatted(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
            TaggedString baseLetterLabel = this.def.letterLabel.Formatted(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
            PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref baseLetterText, ref baseLetterLabel, pawn);
            base.SendStandardLetter(baseLetterLabel, baseLetterText, LetterDefOf.PositiveEvent, parms, pawn, Array.Empty<NamedArgument>());
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => map.reachability.CanReachColony(c) && !c.Fogged(map), map, CellFinder.EdgeRoadChance_Neutral, out cell);
        }

        private const float RelationWithColonistWeight = 20f;
    }
}

