
using RimWorld;
using Verse;


namespace AlphaBehavioursAndEvents
{
    class CompGasProducer : ThingComp
    {

        private int gasProgress = 0;
        private int gasTickMax = 64;
        private System.Random rand = new System.Random();


        public CompProperties_GasProducer Props
        {
            get
            {
                return (CompProperties_GasProducer)this.props;
            }
        }
        public override void CompTick()
        {
            this.gasProgress += 1;
            if (this.gasProgress > gasTickMax)
            {
                Pawn pawn = this.parent as Pawn;
                if (pawn.Map != null) {
                    CellRect rect = GenAdj.OccupiedRect(pawn.Position, pawn.Rotation, IntVec2.One);
                    rect = rect.ExpandedBy(Props.radius);

                   foreach (IntVec3 current in rect.Cells)
                    {
                        if (current.InBounds(pawn.Map)&& rand.NextDouble() < Props.rate)
                        {
                            Thing thing = ThingMaker.MakeThing(ThingDef.Named(Props.gasType), null);

                            GenSpawn.Spawn(thing, current, pawn.Map);
                        }

                    }
                    // FilthMaker.MakeFilth(this.parent.PositionHeld, this.parent.MapHeld, ThingDef.Named("GR_FilthMucus"), 1);
                    this.gasProgress = 0;
                }
                    
            }
        }
    }
}