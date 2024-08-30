using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
using VFECore;

namespace AlphaBehavioursAndEvents
{
    public class Hediff_OverpoweringAcidBuildup : HediffWithComps
    {
        private int tickMax = 64;
        private int tickCounter = 0;

        public AnimalBehaviours.CompAcidImmunity comp;

        public AnimalBehaviours.CompAcidImmunity Immunity
        {
            get
            {
                if (comp == null)
                {
                    comp = pawn.TryGetComp<AnimalBehaviours.CompAcidImmunity>();
                }
                return comp;
            }
        }

        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            if (tickCounter > tickMax)
            {
                if (Immunity is null)
                {
                    pawn.TakeDamage(new DamageInfo(InternalDefOf.AA_OverpoweringSecondaryAcidBurn, 1f, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null));
                }
                tickCounter = 0;
            }
        }
    }
}
