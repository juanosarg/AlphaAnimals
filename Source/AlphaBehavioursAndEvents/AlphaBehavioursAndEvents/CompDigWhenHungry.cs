using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompDigWhenHungry : ThingComp
    {
        public int stopdiggingcounter = 0;
       

        public CompProperties_DigWhenHungry Props
        {
            get
            {
                return (CompProperties_DigWhenHungry)this.props;
            }
        }


        protected string customThingToDig
        {
            get
            {
                return this.Props.customThingToDig;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            Pawn pawn = this.parent as Pawn;
            if ((pawn.Map != null)&&(pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry))
            {
               
                if (stopdiggingcounter <= 0) {
                    Thing thing = ThingMaker.MakeThing(ThingDef.Named(customThingToDig), null);
                    thing.stackCount = 1;
                    Thing t;
                    GenPlace.TryPlaceThing(thing, pawn.Position, this.parent.Map, ThingPlaceMode.Direct, out t, null, null);
                    stopdiggingcounter = 2000;
                }
                stopdiggingcounter--;

            }
        }

     
    }
}
