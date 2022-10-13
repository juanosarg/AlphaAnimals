using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI.Group;
using Verse.Sound;

namespace AlphaBehavioursAndEvents
{
    internal class CompCocoon : ThingComp
    {
        private bool once = true;
        private int timeBeforeInsect;
        private int timeBeforeInsectString;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.timeBeforeInsect, "timeBeforeInsect");
            Scribe_Values.Look<int>(ref this.timeBeforeInsectString, "timeBeforeInsectString");
            Scribe_Values.Look<bool>(ref this.once, "onceCocoonDev");
        }

        public override string CompInspectStringExtra()
        {
            return "AA_CocoonInsectSpawnIn".Translate(timeBeforeInsectString.ToStringTicksToPeriod());
        }

        private CompProperties_Cocoon Props
        {
            get
            {
                return (CompProperties_Cocoon)this.props;
            }
        }



        public override void CompTick()
        {
            base.CompTick();
            if (once) { this.timeBeforeInsect = Find.TickManager.TicksGame + Props.ticksToSpawn; timeBeforeInsectString = Props.ticksToSpawn; once = false; }
            if (Find.TickManager.TicksGame == this.timeBeforeInsect)
            {
                CellFinder.TryFindRandomReachableCellNear(this.parent.Position, parent.Map, 4, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out IntVec3 c);
                FilthMaker.TryMakeFilth(c, parent.Map, ThingDefOf.Filth_Slime);
                SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(parent.Position, parent.Map));

                List<PawnKindDef> pawnKindDefs = new List<PawnKindDef>
                {
                    InternalDefOf.AA_BlackScarab,
        InternalDefOf.AA_BlackSpelopede,InternalDefOf.AA_BlackSpider,InternalDefOf.AA_MammothWorm,InternalDefOf.AA_MegaLouse
                };

                if (pawnKindDefs.Count > 0)
                {
                    
                    PawnGenerationRequest request = new PawnGenerationRequest(pawnKindDefs.RandomElementByWeight(x => x.combatPower / x.race.BaseMarketValue), Faction.OfPlayer, PawnGenerationContext.All, -1, forceGenerateNewPawn: true,  allowDead: false, allowDowned: false, canGeneratePawnRelations: false, TutorSystem.TutorialMode, 20f);
                    Pawn p = PawnGenerator.GeneratePawn(request);

                    p.ageTracker.AgeBiologicalTicks = 30000;
                    GenSpawn.Spawn(p, parent.Position, parent.Map);
                    List<Pawn> pawns = new List<Pawn> { p };
                    
                }
                parent.Destroy();
            }
            timeBeforeInsectString--;
        }
    }
}