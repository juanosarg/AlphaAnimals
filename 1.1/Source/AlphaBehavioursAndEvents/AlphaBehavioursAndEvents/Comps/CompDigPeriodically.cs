using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompDigPeriodically : ThingComp
    {
        public int diggingCounter = 0;
        private Effecter effecter;


        public CompProperties_DigPeriodically Props
        {
            get
            {
                return (CompProperties_DigPeriodically)this.props;
            }
        }




        public override void CompTick()
        {
            base.CompTick();
            diggingCounter++;
            if (diggingCounter > Props.ticksToDig)
            {
                Pawn pawn = this.parent as Pawn;


                if ((pawn.Map != null) && pawn.Awake() && !pawn.Downed && !pawn.Dead)
                {
                    string thingToDig = this.Props.customThingToDig.RandomElement();
                    int index = Props.customThingToDig.IndexOf(thingToDig);
                    int amount = Props.customAmountToDig[index];

                    ThingDef newThing = ThingDef.Named(thingToDig);
                    Thing newDugThing = GenSpawn.Spawn(newThing, pawn.Position, pawn.Map, WipeMode.Vanish);
                    newDugThing.stackCount = amount;
                    if (this.effecter == null)
                    {
                        this.effecter = EffecterDefOf.Mine.Spawn();
                    }
                    this.effecter.Trigger(pawn, newDugThing);






                }
                diggingCounter = 0;
            }






        }


    }
}
