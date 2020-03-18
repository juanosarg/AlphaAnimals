
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents

{
    public class CompProperties_EatWeirdFood : CompProperties
    {

        public List<string> customThingToEat = null;
        public int nutrition = 2;
        public bool fullyDestroyThing = false;
        public float percentageOfDestruction = 0.1f;
        public bool digThingIfMapEmpty = false;
        public string thingToDigIfMapEmpty = "";
        public int customAmountToDig = 1;

        public CompProperties_EatWeirdFood()
        {
            this.compClass = typeof(CompEatWeirdFood);
        }
    }
}
