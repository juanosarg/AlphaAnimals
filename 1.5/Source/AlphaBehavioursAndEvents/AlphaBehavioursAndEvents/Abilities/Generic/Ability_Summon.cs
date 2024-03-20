



using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;
using AnimalBehaviours;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_Summon : Ability
    {



        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo target in targets)
            {

                Ability_Summon_Extension extension = this.def.GetModExtension<Ability_Summon_Extension>();
                PawnKindDef pawnkind = extension.pawnToSpawn;
                Faction faction = extension.playerFaction ? Faction.OfPlayer : Find.FactionManager.FirstFactionOfDef(extension.factionIfNotOfPlayer);
                for(int i = 0; i < extension.numberCreated; i++)
                {
                    Pawn pawnCreated = PawnGenerator.GeneratePawn(pawnkind, faction);
                    GenSpawn.Spawn(pawnCreated, target.Cell, target.Map, Rot4.South);
                    if (extension.enrage)
                    {
                        pawnCreated.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamedSilentFail("AAVPE_Manhunter"), null, true);
                    }   
                }            
            }
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (!base.ValidateTarget(target, showMessages)) return false;
            Ability_Summon_Extension extension = this.def.GetModExtension<Ability_Summon_Extension>();

            if (extension.allowAnAmount && this.pawn.Map.listerThings.ThingsOfDef(extension.pawnToSpawn.race).Count>extension.allowedAmount-1)
            {
                if (showMessages) Messages.Message("AA_OnlyNumberAllowed".Translate(extension.allowedAmount), MessageTypeDefOf.RejectInput, false);
                return false;
            }



            return true;
        }
    }
}
