
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_MouseFission : DeathActionWorker
    {
       

        public override void PawnDied(Corpse corpse)
        {

            Faction newFaction = corpse.InnerPawn.Faction;
            
           
            if (corpse.def.defName== "Corpse_AA_FissionMouse") {
                PawnGenerationRequest request = new PawnGenerationRequest(InternalDefOf.AA_FissionMouseSecond, newFaction, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                Pawn pawn2 = PawnGenerator.GeneratePawn(request);
                Pawn pawn3 = PawnGenerator.GeneratePawn(request);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn, corpse.InnerPawn);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn2, corpse.InnerPawn);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn3, corpse.InnerPawn);





            }
            else if (corpse.def.defName == "Corpse_AA_FissionMouseSecond")
            {
                PawnGenerationRequest request = new PawnGenerationRequest(InternalDefOf.AA_FissionMouseThird, newFaction, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);
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