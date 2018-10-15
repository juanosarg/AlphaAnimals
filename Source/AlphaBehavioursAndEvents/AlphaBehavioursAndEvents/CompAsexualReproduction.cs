using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompAsexualReproduction: ThingComp
    {

        public int ticksInday = 60000;
        public int asexualFissionCounter = 0;


        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.asexualFissionCounter, "asexualFissionCounter", 0, false);
        }




        public CompProperties_AsexualReproduction Props
        {
            get
            {
                return (CompProperties_AsexualReproduction)this.props;
            }
        }

        protected int reproductionIntervalDays
        {
            get
            {
                return this.Props.reproductionIntervalDays;
            }
        }

        protected string customString
        {
            get
            {
                return this.Props.customString;
            }
        }

        protected bool produceEggs
        {
            get
            {
                return this.Props.produceEggs;
            }
        }

        protected string eggDef
        {
            get
            {
                return this.Props.eggDef;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            Pawn pawn = this.parent as Pawn;
            if ((pawn.Faction == Faction.OfPlayer)&&(pawn.ageTracker.CurLifeStage.reproductive))
            {
                asexualFissionCounter++;
                if (asexualFissionCounter >= ticksInday * reproductionIntervalDays)
                {
                    if (produceEggs) {
                        GenSpawn.Spawn(ThingDef.Named(eggDef), pawn.Position, pawn.Map);
                        Messages.Message("AA_AsexualHatchedEgg".Translate(pawn.LabelIndefinite().CapitalizeFirst()), pawn, MessageTypeDefOf.PositiveEvent, true);
                        asexualFissionCounter = 0;
                    } else {
                        Hediff_Pregnant.DoBirthSpawn(pawn, pawn);
                        Messages.Message("AA_AsexualHatched".Translate(pawn.LabelIndefinite().CapitalizeFirst()), pawn, MessageTypeDefOf.PositiveEvent, true);
                        asexualFissionCounter = 0;
                    }
                    
                }
            }
        }

        public override string CompInspectStringExtra()
        {
            Pawn pawn = this.parent as Pawn;
            if ((pawn.Faction == Faction.OfPlayer) && (pawn.ageTracker.CurLifeStage.reproductive))
            {
                float totalProgress = ((float)asexualFissionCounter / (float)(ticksInday * reproductionIntervalDays));
                return customString + totalProgress.ToStringPercent() + " (" + reproductionIntervalDays.ToString() + " days)";
            }
            else return "";
            
        }
    }
}
