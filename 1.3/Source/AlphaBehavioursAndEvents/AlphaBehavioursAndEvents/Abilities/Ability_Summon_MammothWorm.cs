




using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;
using AnimalBehaviours;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_Summon_MammothWorm : Ability
    {



        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo target in targets)
            {
                Pawn scarab = PawnGenerator.GeneratePawn(PawnKindDef.Named("AA_MammothWorm_Temporary"), this.pawn.Faction);
               
                GenSpawn.Spawn(scarab, target.Cell, target.Map, Rot4.South);
                scarab.mindState.mentalStateHandler.TryStartMentalState(InternalDefOf.AAVPE_Manhunter, null, true);
            }
        }


    }
}
