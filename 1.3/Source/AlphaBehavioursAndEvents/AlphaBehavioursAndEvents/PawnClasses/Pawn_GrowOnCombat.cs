using System;
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

        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            if (tickCounter > tickCountMax)
            {
                if (this.Awake())
                {
                    if (this.Map != null && this.jobs.curDriver != null && this.jobs.curDriver is JobDriver_AttackMelee)
                    {
                        if(this.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_AnoleGrown"))==null&&
                            this.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_AnoleGrownExhausted")) == null)
                        {
                            BodyPartRecord part = this.RaceProps.body.GetPartsWithDef(BodyPartDefOf.Body).FirstOrDefault();

                            this.health.AddHediff(HediffDef.Named("AA_AnoleGrown"), part);

                        }

                    }
                }
                tickCounter = 0;
            }
            

        }

    }
}