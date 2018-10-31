using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompLightSustenance : ThingComp
    {


        public float growOptimalGlow = 0.4f;
        private bool addHediffOnce = true;



        public CompProperties_LightSustenance Props
        {
            get
            {
                return (CompProperties_LightSustenance)this.props;
            }
        }


        public override void CompTick()
        {
            Pawn pawn = this.parent as Pawn;

            if (pawn.Spawned)
            {
                if (addHediffOnce)
            {
                pawn.health.AddHediff(HediffDef.Named("AA_LightSustenance"));
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_LightSustenance"), false);
                hediff.Severity = 0.2f;
                addHediffOnce = false;
            }
            float num = this.parent.Map.glowGrid.GameGlowAt(this.parent.Position, false);
            //Log.Warning("Light level "+num.ToString());

            if (num >= growOptimalGlow)
            {

                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_LightSustenance"), false);

                if ((hediff != null) && hediff.Severity > 0f)
                {
                    hediff.Severity -= 0.000010f;
                   // Log.Warning("Severity " + hediff.Severity.ToString());
                }
            }
            else
            {
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("AA_LightSustenance"), false);

                if ((hediff != null) && hediff.Severity < 1f)
                {

                    hediff.Severity += 0.000010f;
                    // Log.Warning("Severity " + hediff.Severity.ToString());

                }
                }
            }
        }


    }
}

