
using UnityEngine;
using Verse;
using System.Linq;


namespace AlphaBehavioursAndEvents
{
    public class CompInitialHediff : ThingComp
    {
        private bool addHediffOnce = true;
        private System.Random rand = new System.Random();
        public int phase = 1;


        public CompProperties_InitialHediff Props
        {
            get
            {
                return (CompProperties_InitialHediff)this.props;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref this.addHediffOnce, "addHediffOnce", true, false);
            Scribe_Values.Look<int>(ref this.phase, "phase", 1, false);

        }



        public override void CompTickRare()
        {

            base.CompTickRare();

            if (addHediffOnce)
            {
                //Log.Message("Ticking");
                Pawn pawn = this.parent as Pawn;
                if (Props.numberOfHediffs == 1) {
                    pawn.health.AddHediff(HediffDef.Named(Props.hediffname));
                } else
                {
                    int randomHediff = rand.Next(1,Props.numberOfHediffs + 1);
                    phase = randomHediff;
                    pawn.health.AddHediff(HediffDef.Named(Props.hediffname + randomHediff.ToString()));
                }
                
                //Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named(Props.hediffname), false);
                //hediff.Severity = Props.hediffseverity;
                addHediffOnce = false;
            }
        }


    }
}
