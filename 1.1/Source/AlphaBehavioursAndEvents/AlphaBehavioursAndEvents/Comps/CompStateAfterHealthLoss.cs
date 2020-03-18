using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using UnityEngine;
using System.Collections;

namespace AlphaBehavioursAndEvents
{
    public class CompStateAfterHealthLoss : ThingComp
    {
        public int tickCounter = 0;
       

        public CompProperties_StateAfterHealthLoss Props
        {
            get
            {
                return (CompProperties_StateAfterHealthLoss)this.props;
            }
        }




        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;
            if (tickCounter > Props.tickInterval)
            {
                Pawn thisPawn = this.parent as Pawn;
                if (thisPawn!=null && thisPawn.Map != null && !thisPawn.Dead && !thisPawn.Downed)
                {
                   
                    if (thisPawn.health.summaryHealth.SummaryHealthPercent < ((float)(Props.healthPercent)/100))
                    {
                        thisPawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed(Props.mentalState, true), null, true, false, null, false);

                    }

                }
                tickCounter = 0;
            }
        }


    }
}

