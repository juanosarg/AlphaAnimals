using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompFeminism : ThingComp
    {
        private bool changeGenderOnce = true;

        public void ExposeData()
        {
            Scribe_Values.Look<bool>(ref this.changeGenderOnce, "changeGenderOnce", true, false);
        }

        public CompProperties_Feminism Props
        {
            get
            {
                return (CompProperties_Feminism)this.props;
            }
        }


        public override void CompTick()
        {
            base.CompTick();
            if (changeGenderOnce)
            {
                if (this.parent.Map != null)
                {

                    Pawn pawn = this.parent as Pawn;
                    pawn.gender = Props.gender;

                    changeGenderOnce = false;

                }


            }

        }


    }
}

