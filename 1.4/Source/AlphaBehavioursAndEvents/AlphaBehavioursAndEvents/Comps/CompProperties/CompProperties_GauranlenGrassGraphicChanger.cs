using Verse;
using System.Collections.Generic;


namespace AlphaBehavioursAndEvents
{
    public class CompProperties_GauranlenGrassGraphicChanger : CompProperties
    {

        //A simple comp class that changes a building's graphic by using reflection

        public List<string> newGraphics;



        public CompProperties_GauranlenGrassGraphicChanger()
        {
            this.compClass = typeof(CompGauranlenGrassGraphicChanger);
        }


    }
}
