using RimWorld;
using Verse;
using Verse.Sound;
using VEF;


namespace AlphaBehavioursAndEvents
{
    internal class CompBlackSwarmlingToCocoon : ThingComp
    {
        private int timeBeforeTransform;

        private CompProperties_BlackSwarmlingToCocoon Props
        {
            get
            {
                return (CompProperties_BlackSwarmlingToCocoon)this.props;
            }
        }

        public override string CompInspectStringExtra()
        {

            return "AA_VFEI_CreatingCocoon".Translate(timeBeforeTransform.ToStringTicksToPeriod());
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            this.timeBeforeTransform = Props.ticksBeforeBecomingCocoon;
        }

        public override void CompTick()
        {
            base.CompTick();
            if (this.timeBeforeTransform < 0 && this.parent.Map != null)
            {
                IntVec3 pos = this.parent.Position;
                Map map = this.parent.Map;

                Thing thing = ThingMaker.MakeThing(InternalDefOf.VFEI2_BlackInsectoidCocoon);
                if (this.parent.Faction != null) thing.SetFaction(this.parent.Faction);

                GenSpawn.Spawn(thing, pos, map);
                for (int i = 0; i < 5; i++)
                {
                    CellFinder.TryFindRandomReachableCellNearPosition(pos, pos, map, 1, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out IntVec3 c);
                    FilthMaker.TryMakeFilth(c, map, ThingDefOf.Filth_Slime);
                }
                InternalDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(pos, map));
                this.parent.Destroy();
            }
            timeBeforeTransform--;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.timeBeforeTransform, "timeBeforeTransform");
        }
    }
}