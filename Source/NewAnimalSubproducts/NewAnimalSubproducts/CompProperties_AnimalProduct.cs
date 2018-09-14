using Verse;

namespace NewAlphaAnimalSubproducts
{
    public class CompProperties_AnimalProduct : CompProperties
    {

        public int gatheringIntervalDays = 1;
        public int resourceAmount = 1;
        public ThingDef resourceDef;
        public string customResourceString;

        public CompProperties_AnimalProduct()
        {
            this.compClass = typeof(CompAnimalProduct);
        }


    }
}
