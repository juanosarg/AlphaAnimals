
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_MouseFission : DeathActionWorker
    {
       

        public override void PawnDied(Corpse corpse)
        {
            //GenExplosion.DoExplosion(corpse.Position, corpse.Map, 2.9f, DamageDefOf.Stun, corpse.InnerPawn, -1, -1, null, null, null, null, ThingDef.Named("Gas_Smoke"), .7f, 1, false, null, 0f, 1);

            Faction newFaction = corpse.InnerPawn.Faction;
            
           
            if (corpse.def.defName== "Corpse_AA_FissionMouse") {
                PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named("AA_FissionMouseSecond"), newFaction, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                Pawn pawn2 = PawnGenerator.GeneratePawn(request);
                Pawn pawn3 = PawnGenerator.GeneratePawn(request);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn, corpse.InnerPawn);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn2, corpse.InnerPawn);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn3, corpse.InnerPawn);





            }
            else if (corpse.def.defName == "Corpse_AA_FissionMouseSecond")
            {
                PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named("AA_FissionMouseThird"), newFaction, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                Pawn pawn2 = PawnGenerator.GeneratePawn(request);
                Pawn pawn3 = PawnGenerator.GeneratePawn(request);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn, corpse.InnerPawn);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn2, corpse.InnerPawn);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn3, corpse.InnerPawn);


            }

        }


    }
}