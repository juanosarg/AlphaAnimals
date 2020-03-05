using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_DigWhenHungry : CompProperties
    {

       
        public string customThingToDig ="";

        public CompProperties_DigWhenHungry()
        {
            this.compClass = typeof(CompDigWhenHungry);
        }


    }
}