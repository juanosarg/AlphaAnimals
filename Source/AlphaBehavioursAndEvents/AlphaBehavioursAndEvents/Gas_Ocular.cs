using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class Gas_Ocular : Gas
    {
        private int tickerInterval = 0;
        private int tickerMax = 64;




        public override void Tick()
        {
            base.Tick();
            try
            {
                if (tickerInterval >= tickerMax)
                {

                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                    if (hashSet != null)
                    {
                        foreach (Thing current in hashSet)
                        {
                            PlantProperties plant = current.def.plant;
                            bool flag = (plant != null);
                            if (flag)
                            {
                               if (plant.IsTree)
                                {
                                    current.Destroy();
                                }


                            }
                        }

                    }
                    tickerInterval = 0;



                }
                tickerInterval++;
            }
            catch (NullReferenceException e)
            {
                //A weird error is produced sometimes when GetThingList returns a NullReferenceException. I did a try-catch which is inellegant, but it works
            }

        }


    }
}
