using Verse;
using System.Collections.Generic;


namespace AlphaBehavioursAndEvents
{
    public class CompProperties_GraphicByTerrain : CompProperties
    {

        //CompGraphicByTerrain changes a creature's graphic depending on the terrain it is positioned at

        public string suffix = "_Hidden";
        public List<string> terrains = null;
        public int changeGraphicsInterval = 240;
        public string dessicatedTxt = "";
        public string hediffToApply = "";



        public CompProperties_GraphicByTerrain()
        {
            this.compClass = typeof(CompGraphicByTerrain);
        }


    }
}
