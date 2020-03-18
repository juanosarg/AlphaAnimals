using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Fertilizer : CompProperties
    {

        public string FirstStageTerrain = "";
        public string SecondStageTerrain = "";
        public string ThirdStageTerrain = "";




        public CompProperties_Fertilizer()
        {
            this.compClass = typeof(CompFertilizer);
        }


    }
}