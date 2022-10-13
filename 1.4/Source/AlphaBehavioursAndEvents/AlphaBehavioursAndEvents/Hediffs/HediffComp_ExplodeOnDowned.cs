

using RimWorld;
using Verse;
using Verse.Sound;
using System;

namespace AlphaBehavioursAndEvents
{
    public class HediffComp_ExplodeOnDowned : HediffComp
    {
        public int checkDownCounter = 0;
        public int checkEveryTicks = 60;

        public HediffCompProperties_ExplodeOnDowned Props
        {
            get
            {
                return (HediffCompProperties_ExplodeOnDowned)this.props;
            }
        }

     

         public override void CompPostTick(ref float severityAdjustment)
         {
            base.CompPostTick(ref severityAdjustment);
            checkDownCounter++;
            if (checkDownCounter > checkEveryTicks)
            {
                if (this.parent.pawn.Downed)
                {
                    if (!this.parent.pawn.health.hediffSet.HasHediff(HediffDef.Named("Anesthetic"))) {
                        this.parent.pawn.Kill(null);
                    }
                    
                }
                checkDownCounter = 0;
            }

         }
    }
}

