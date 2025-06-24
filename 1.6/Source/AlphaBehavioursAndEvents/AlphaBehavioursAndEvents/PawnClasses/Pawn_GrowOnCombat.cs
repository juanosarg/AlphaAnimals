﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaBehavioursAndEvents

{
    [StaticConstructorOnStartup]
    public class Pawn_GrowOnCombat : Pawn
    {

        public int tickCountMax = 100;
        public int tickCounter = 0;

        protected override void Tick()
        {
            base.Tick();
            tickCounter++;
            if (tickCounter > tickCountMax)
            {
                if (this.Awake())
                {
                    if (this.Map != null && this.jobs.curDriver != null && this.jobs.curDriver is JobDriver_AttackMelee)
                    {
                        if(this.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AA_AnoleGrown)==null&&
                            this.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AA_AnoleGrownExhausted) == null)
                        {
                            BodyPartRecord part = this.RaceProps.body.GetPartsWithDef(InternalDefOf.Body).FirstOrDefault();

                            this.health.AddHediff(InternalDefOf.AA_AnoleGrown, part);

                        }

                    }
                }
                tickCounter = 0;
            }
            

        }

    }
}