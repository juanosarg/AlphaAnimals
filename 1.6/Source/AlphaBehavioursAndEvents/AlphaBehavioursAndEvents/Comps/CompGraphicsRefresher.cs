
using RimWorld;
using System;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class CompGraphicsRefresher : ThingComp
    {

        public bool cachedOption = AlphaAnimalsEvents_Mod.settings.alternatePokemonGraphics;

        protected CompProperties_GraphicsRefresher Props => (CompProperties_GraphicsRefresher)props;       

        public override void CompTick()
        {
            if (parent.IsHashIntervalTick(2000))
            {
                if(cachedOption != AlphaAnimalsEvents_Mod.settings.alternatePokemonGraphics)
                {
                    Pawn pawn = parent as Pawn;
                    pawn.Drawer.renderer.SetAllGraphicsDirty();
                   
                    cachedOption = AlphaAnimalsEvents_Mod.settings.alternatePokemonGraphics;
                }


            }

        }

      
    }
}
