

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using Verse.Sound;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    class HediffComp_Mime : HediffComp
    {
        public HediffCompProperties_Mime Props
        {
            get
            {
                return (HediffCompProperties_Mime)this.props;
            }
        }


        public int ticksWithMalnutrition = 1;
        public bool naturalDeath = true;

        public override void CompExposeData()
        {
            Scribe_Values.Look<int>(ref this.ticksWithMalnutrition, "ticksWithMalnutrition", 1, false);
            Scribe_Values.Look<bool>(ref this.naturalDeath, "naturalDeath", true, false);


        }

        public override void CompPostMake()
        {
            base.CompPostMake();
            ticksWithMalnutrition = Props.malnutritionTrigger;
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (this.parent.pawn.IsHashIntervalTick(250))
            {
                if (this.parent.pawn.health.hediffSet.HasHediff(HediffDef.Named("Malnutrition")))
                {
                    ticksWithMalnutrition--;
                    if (ticksWithMalnutrition <= 0)
                    {
                        naturalDeath = false;
                        this.parent.pawn.Kill(null);
                    }
                }
            }
        }


        public override void Notify_PawnDied()
        {


            Map map = this.parent.pawn.Corpse.Map;
            if (map != null)
            {
                if (naturalDeath) {
                    PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named(Props.turnTo), null, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false);
                    Pawn pawn = PawnGenerator.GeneratePawn(request);
                    PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this.parent.pawn.Corpse);
                    pawn.Kill(null);
                    this.parent.pawn.Corpse.Destroy();
                } else
                {
                    Gender oldGender = this.parent.pawn.gender;
                    Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDefOf.AncientsHostile);
                    PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named(Props.turnTo), faction, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false);
                    Pawn pawn = PawnGenerator.GeneratePawn(request);
                    PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this.parent.pawn.Corpse);
                    pawn.gender = oldGender;
                    pawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("ManhunterPermanent", true), null, true, false, null, false);
                    for (int i = 0; i < 20; i++)
                    {
                        IntVec3 c;
                        CellFinder.TryFindRandomReachableCellNear(this.parent.pawn.Corpse.Position, map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                        FilthMaker.TryMakeFilth(c, this.parent.pawn.Corpse.Map, ThingDefOf.Filth_Blood);

                    }
                    SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(this.parent.pawn.Corpse.Position, map, false));
                    this.parent.pawn.Corpse.Destroy();
                }
                

            }



        }


    }
}
