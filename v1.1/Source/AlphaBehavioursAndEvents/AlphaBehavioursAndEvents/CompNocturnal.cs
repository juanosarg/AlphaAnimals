using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompNocturnal : ThingComp
    {


        public float growOptimalGlow = 0.1f;
        private bool addHediffOnce = true;



        public CompProperties_Nocturnal Props
        {
            get
            {
                return (CompProperties_Nocturnal)this.props;
            }
        }


        public override void CompTick()
        {
            Pawn pawn = this.parent as Pawn;

            if (pawn.Spawned)
            {
                if (addHediffOnce)
            {
                pawn.health.AddHediff(HediffDef.Named("AA_Nocturnal"));
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_Nocturnal"), false);
                hediff.Severity = 1f;
                addHediffOnce = false;
            }
                int currentHour = GenLocalDate.HourInteger(pawn.Map);
                //float num = this.Map.glowGrid.GameGlowAt(this.Position, false);
                if ((currentHour >= 19 && currentHour <= 24)|| (currentHour >= 0 && currentHour <= 9))
                {
                    if (pawn.needs.rest.CurLevelPercentage < 0.8) {
                        pawn.needs.rest.SetInitialLevel();
                        RestUtility.WakeUp(pawn);
                    }
                    

                }else if (currentHour >= 10 && currentHour <= 13)
                {
                    pawn.needs.rest.CurLevel = 0;

                }

            }
        }


    }
}

