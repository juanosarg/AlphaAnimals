
using UnityEngine;
using Verse;
using System.Linq;


namespace AlphaBehavioursAndEvents
{
    public class CompInitialHediff : ThingComp
    {
        private bool addHediffOnce = true;



        public CompProperties_InitialHediff Props
        {
            get
            {
                return (CompProperties_InitialHediff)this.props;
            }
        }




        public override void CompTickRare()
        {

            base.CompTickRare();

            if (addHediffOnce)
            {
                //Log.Message("Ticking");
                Pawn pawn = this.parent as Pawn;
                pawn.health.AddHediff(HediffDef.Named(Props.hediffname));
                //Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named(Props.hediffname), false);
                //hediff.Severity = Props.hediffseverity;
                addHediffOnce = false;
            }
        }


    }
}
