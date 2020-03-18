using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_Feminism : CompProperties
    {


        public Gender gender = Gender.Female;


        public CompProperties_Feminism()
        {
            this.compClass = typeof(CompFeminism);
        }


    }
}
