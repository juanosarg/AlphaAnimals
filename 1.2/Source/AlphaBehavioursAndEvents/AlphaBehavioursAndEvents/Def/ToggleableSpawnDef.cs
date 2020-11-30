using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class ToggleableSpawnDef : Def
    {

        //ToggleableSpawnDef is a simple custom def that allows you to input a list of defNames for use
        //in mod options so people can choose which animals don't spawn
       
        //A list of pawnkind defNames
        public List<string> toggleablePawns;
    }
}
