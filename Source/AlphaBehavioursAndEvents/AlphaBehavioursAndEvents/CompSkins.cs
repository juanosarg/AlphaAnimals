using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompSkins : ThingComp
    {
      
        public override void PostExposeData()
        {
            base.PostExposeData();
           // Scribe_Values.Look<int>(ref this.asexualFissionCounter, "asexualFissionCounter", 0, false);
        }




        public CompProperties_Skins Props
        {
            get
            {
                return (CompProperties_Skins)this.props;
            }
        }

        protected int numberOfSkins
        {
            get
            {
                return this.Props.numberOfSkins;
            }
        }

        protected string skinBaseString
        {
            get
            {
                return this.Props.skinBaseString;
            }
        }

        public override void CompTick()
        {
           /* base.CompTick();
            Pawn pawn = this.parent as Pawn;
            if ((pawn.Faction == Faction.OfPlayer)&&(pawn.ageTracker.CurLifeStage.reproductive))
            {
                asexualFissionCounter++;
                if (asexualFissionCounter >= ticksInday * reproductionIntervalDays)
                {
                    Hediff_Pregnant.DoBirthSpawn(pawn, pawn);
                    Messages.Message("AA_AsexualHatched".Translate(new object[]
                    {
                       pawn.LabelIndefinite()
                    }).CapitalizeFirst(), pawn, MessageTypeDefOf.PositiveEvent, true);
                    asexualFissionCounter = 0;
                }
            }*/
        }

       
    }
}
