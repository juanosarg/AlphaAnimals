using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using System;

namespace NewAlphaAnimalSubproducts
{
    public class CompAnimalProduct : CompHasGatherableBodyResource
    {

        System.Random rand = new System.Random();

        protected override int GatherResourcesIntervalDays
        {
            get
            {
                return this.Props.gatheringIntervalDays;
            }
        }



        protected override int ResourceAmount
        {
            get
            {
                return this.Props.resourceAmount;
            }
        }

        protected override ThingDef ResourceDef
        {
            get
            {
                if (Props.isRandom) {
                   
                    return ThingDef.Named(Props.randomItems.RandomElement());
                } else {
                    return this.Props.resourceDef;
                }
               
            }
        }

        protected override string SaveKey
        {
            get
            {
                return "resourceGrowth";
            }
        }

        public CompProperties_AnimalProduct Props
        {
            get
            {
                return (CompProperties_AnimalProduct)this.props;
            }
        }

        protected override bool Active
        {
            get
            {
                if (!base.Active)
                {
                    return false;
                }
                Pawn pawn = this.parent as Pawn;
                return pawn == null || pawn.ageTracker.CurLifeStage.shearable;
            }
        }

        public override string CompInspectStringExtra()
        {
            if (!this.Active)
            {
                return null;
            }

            if (!this.Props.customResourceString.NullOrEmpty())
            {
                return Translator.Translate(this.Props.customResourceString) + ": " + base.Fullness.ToStringPercent();
            }

           

            else  return Translator.Translate("ResourceGrowth") + ": " + base.Fullness.ToStringPercent();
        }



    }
}
