using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_OnlyBlackHive : Ability
    {


        private static readonly List<PawnKindDef> allowedTargets = new List<PawnKindDef>() { InternalDefOf.AA_BlackScarab,
        InternalDefOf.AA_BlackSpelopede,InternalDefOf.AA_BlackSpider,InternalDefOf.AA_MammothWorm,InternalDefOf.AA_MegaLouse,DefDatabase<PawnKindDef>.GetNamedSilentFail("AA_BlackScarab_Temporary"),
        DefDatabase<PawnKindDef>.GetNamedSilentFail("AA_BlackSpider_Temporary"),DefDatabase<PawnKindDef>.GetNamedSilentFail("AA_BlackSpelopede_Temporary"),DefDatabase<PawnKindDef>.GetNamedSilentFail("AA_MammothWorm_Temporary"),DefDatabase<PawnKindDef>.GetNamedSilentFail("AAVPE_BlackQueen")};

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (!base.ValidateTarget(target, showMessages)) return false;
            if (!target.HasThing) return false;
            Pawn pawn = target.Thing as Pawn;
            if (pawn==null || !allowedTargets.Contains(pawn.kindDef))
            {
                if (showMessages) Messages.Message("AA_OnlyBlackHive".Translate(), MessageTypeDefOf.RejectInput, false);
                return false;
            }

            

            return true;
        }
    }
}