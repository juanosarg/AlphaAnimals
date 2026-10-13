

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using Verse.Sound;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    class HediffComp_RemoveFromMechsAndGhouls : HediffComp
    {
        public HediffCompProperties_RemoveFromMechsAndGhouls Props
        {
            get
            {
                return (HediffCompProperties_RemoveFromMechsAndGhouls)this.props;
            }
        }



        public override void CompPostMake()
        {
            base.CompPostMake();
            if (this.parent.pawn.RaceProps.IsMechanoid)
            {
                this.parent.pawn.health.RemoveHediff(this.parent);
            }
            if (this.parent.pawn.IsGhoul)
            {
                this.parent.pawn.health.RemoveHediff(this.parent);
            }
        }

       


    }
}
