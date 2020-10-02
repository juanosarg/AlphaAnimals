using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class CompUntameable : ThingComp
    {

        public CompProperties_Untameable Props
        {
            get
            {
                return (CompProperties_Untameable)this.props;
            }
        }


        public override void CompTick()
        {
            base.CompTick();
            if (this.parent.IsHashIntervalTick(500))
            {
                this.CheckFaction();
            }
        }

        public void CheckFaction()
        {
            if (this.parent.Faction == Faction.OfPlayer)
            {

                Pawn pawn = parent as Pawn;
                if (pawn != null)
                {
                    if (Props.factionToReturnTo == "")
                    {

                        pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);



                    }
                    else
                    {


                        parent.SetFaction(Find.FactionManager.FirstFactionOfDef(FactionDef.Named(Props.factionToReturnTo)), null);
                        pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);


                    };
                    if (Props.sendMessage)
                    {
                        Messages.Message(Props.message.Translate(pawn.LabelIndefinite().CapitalizeFirst()), pawn, MessageTypeDefOf.NegativeEvent, true);

                    }
                }


            }

        }


    }
}
