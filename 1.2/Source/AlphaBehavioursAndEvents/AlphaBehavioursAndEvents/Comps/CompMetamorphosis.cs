using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.Sound;
using UnityEngine;
using System.Collections;

namespace AlphaBehavioursAndEvents
{
    public class CompMetamorphosis : ThingComp
    {

        public int metamorphosisTick = 0;

        public int rareTicksInAYear = 14400;

        public CompProperties_Metamorphosis Props
        {
            get
            {
                return (CompProperties_Metamorphosis)this.props;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
         
            Scribe_Values.Look<int>(ref this.metamorphosisTick, "metamorphosisTick", 0, false);

        }

        public override void CompTickRare()
        {
            base.CompTickRare();
            metamorphosisTick++;
            if (metamorphosisTick> rareTicksInAYear * Props.timeInYears)
            {

                Faction faction = this.parent.Faction;
               
               
                
                PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named(Props.pawnToTurnInto), faction, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false);
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(parent.Position, parent.Map, 3, null), parent.Map, WipeMode.Vanish);





                for (int i = 0; i < 20; i++)
                {
                    IntVec3 c;
                    CellFinder.TryFindRandomReachableCellNear(this.parent.Position, this.parent.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                    FilthMaker.TryMakeFilth(c, this.parent.Map, ThingDefOf.Filth_AmnioticFluid);

                }


                SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(this.parent.Position, this.parent.Map, false));
                this.parent.Destroy();









            }
        }

        public override string CompInspectStringExtra()
        {
            int tickNumber = (int)(((rareTicksInAYear * Props.timeInYears) - metamorphosisTick) * 250);

            return ("AA_TimeToMetamorphosis").Translate(tickNumber.ToStringTicksToPeriod(false,false,false,true));
        }


    }
}
