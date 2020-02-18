using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class Hediff_AcidBuildup : HediffWithComps
    {
        private int tickMax = 64;
        private int tickCounter = 0;
       


        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            if (tickCounter > tickMax)
            {
                pawn.TakeDamage(new DamageInfo(DefDatabase<DamageDef>.GetNamed("AA_SecondaryAcidBurn"), 1f, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null));
                tickCounter = 0;
            }
           

        }

     


    }
}
