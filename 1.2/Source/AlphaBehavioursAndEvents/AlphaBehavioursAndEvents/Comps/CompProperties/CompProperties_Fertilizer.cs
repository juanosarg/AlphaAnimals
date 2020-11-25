using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Fertilizer : CompProperties
    {

        //Makes the animal change a given terrain to a second one, and then that second one to a third one

        public string FirstStageTerrain = "";
        public string SecondStageTerrain = "";
        public string ThirdStageTerrain = "";

        public CompProperties_Fertilizer()
        {
            this.compClass = typeof(CompFertilizer);
        }
    }
}