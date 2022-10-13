using Verse;
using System.Collections.Generic;


namespace AlphaBehavioursAndEvents
{
    public class CompProperties_GauranlenGraphicChanger : CompProperties
    {

        //A simple comp class that changes a building's graphic by using reflection

        public List<string> newGraphics;



        public CompProperties_GauranlenGraphicChanger()
        {
            this.compClass = typeof(CompGauranlenGraphicChanger);
        }


    }
}
