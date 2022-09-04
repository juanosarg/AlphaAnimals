using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class Gas_Frost : Gas
    {
        private int tickerInterval = 0;
        private int tickerMax = 128;


        public override void Tick()
        {
            base.Tick();
            if (tickerInterval >= tickerMax)
            {
                try
                {


                    List<Thing> list = new List<Thing>(this.Position.GetThingList(this.Map));
                    if (list != null)
                    {
                        foreach (Thing current in list)
                        {
                            Pawn pawn = current as Pawn;

                            if (pawn != null && (pawn.def.defName != "AA_FrostLynx" || pawn.def.defName != "AA_Genix"))
                            {


                                pawn.TakeDamage(new DamageInfo(DamageDefOf.Frostbite, 5, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));

                            }
                        }

                    }
                    tickerInterval = 0;



                } catch (NullReferenceException)
                {

                    //A weird error is produced sometimes when GetThingList returns a NullReferenceException. I did a try-catch which is inellegant, but it works
                }
                
            }
           tickerInterval++;
            

        }


    }
}
