using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace NewAlphaAnimalSubproducts
{
    public class CompAnimalProduct : CompHasGatherableBodyResource
    {
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
                return this.Props.resourceDef;
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

            else if (ResourceDef.ToString() == "GR_EldritchInsectJelly")
            {
                return Translator.Translate("JellyGrowth2") + ": " + base.Fullness.ToStringPercent();
            }
            else if (ResourceDef.ToString() == "GR_DarkYoungWoodLog")
            {
                return Translator.Translate("TentacleGrowth") + ": " + base.Fullness.ToStringPercent();
            }
          

            else  return Translator.Translate("ResourceGrowth") + ": " + base.Fullness.ToStringPercent();
        }



    }
}
