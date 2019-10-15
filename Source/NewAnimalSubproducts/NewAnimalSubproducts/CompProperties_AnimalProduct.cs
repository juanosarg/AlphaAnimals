using Verse;
using System.Collections.Generic;


namespace NewAlphaAnimalSubproducts
{
    public class CompProperties_AnimalProduct : CompProperties
    {

        public int gatheringIntervalDays = 1;
        public int resourceAmount = 1;
        public ThingDef resourceDef = null;
        public string customResourceString ="";
        public bool isRandom = false;
        public List<string> randomItems = null;

        public CompProperties_AnimalProduct()
        {
            this.compClass = typeof(CompAnimalProduct);
        }


    }
}
