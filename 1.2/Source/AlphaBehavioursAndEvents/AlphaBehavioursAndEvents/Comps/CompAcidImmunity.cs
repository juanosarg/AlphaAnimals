using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompAcidImmunity : ThingComp
    {
      

        public CompProperties_AcidImmunity Props
        {
            get
            {
                return (CompProperties_AcidImmunity)this.props;
            }
        }


        


    }
}

