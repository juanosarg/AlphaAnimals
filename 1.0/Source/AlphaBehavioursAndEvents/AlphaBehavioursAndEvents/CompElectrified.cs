
using UnityEngine;
using Verse;
using System.Collections.Generic;
using RimWorld;


namespace AlphaBehavioursAndEvents
{
    public class CompElectrified : ThingComp
    {

        public int tickCounter = 0;

        public CompProperties_Electrified Props
        {
            get
            {
                return (CompProperties_Electrified)this.props;
            }
        }

        protected int electroRate
        {
            get
            {
                return this.Props.electroRate;
            }
        }

        protected int electroRadius
        {
            get
            {
                return this.Props.electroRadius;
            }
        }


        public override void CompTick()
        {


            if (this.parent.Map != null) {
                tickCounter++;

                if (tickCounter >= electroRate)
                {
                    Pawn pawn = this.parent as Pawn;

                    CellRect rect = GenAdj.OccupiedRect(pawn.Position, pawn.Rotation, IntVec2.One);
                    rect = rect.ExpandedBy(electroRadius);

                    List<Building> batteriesInRange = new List<Building>();

                    foreach (IntVec3 current in rect.Cells)
                    {
                        if (current.InBounds(pawn.Map))
                        {
                            Building edifice = current.GetEdifice(pawn.Map);
                            if (edifice != null && ((edifice.def.defName == "Battery")))
                            {
                                batteriesInRange.Add(edifice);
                            }
                        }

                    }

                    if (batteriesInRange.Count > 0)
                    {
                        Building batteryToAffect = batteriesInRange.RandomElement();
                        MoteMaker.ThrowMicroSparks(batteryToAffect.Position.ToVector3(), batteryToAffect.Map);
                        foreach (CompPowerBattery current2 in batteryToAffect.GetComps<CompPowerBattery>())
                        {
                            current2.AddEnergy((float)1);
                            break;

                        }
                    }
                    tickCounter = 0;
                }
            }
            

            

            

        }
        


    }
}

