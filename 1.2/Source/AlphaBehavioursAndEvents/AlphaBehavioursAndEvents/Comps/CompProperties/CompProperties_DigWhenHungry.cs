using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_DigWhenHungry : CompProperties
    {

       
        public string customThingToDig ="";
        public bool isFrostmite = false;
        public int customAmountToDig = 1;

        public CompProperties_DigWhenHungry()
        {
            this.compClass = typeof(CompDigWhenHungry);
        }


    }
}