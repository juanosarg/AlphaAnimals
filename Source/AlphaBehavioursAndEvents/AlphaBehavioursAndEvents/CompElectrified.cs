
using UnityEngine;
using Verse;
using System.Linq;
using RimWorld;


namespace AlphaBehavioursAndEvents
{
    public class CompElectrified : ThingComp
    {

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

            Pawn pawn = this.parent as Pawn;

            CellRect rect = GenAdj.OccupiedRect(pawn.Position, pawn.Rotation, IntVec2.One);
            rect = rect.ExpandedBy(electroRadius);
            
            foreach (IntVec3 current in rect.Cells)
            {
                Building edifice = current.GetEdifice(pawn.Map);
                if (edifice != null && ((edifice.def.defName == "Battery")))
                {
                    MoteMaker.ThrowMicroSparks(edifice.Position.ToVector3(), edifice.Map);
                    foreach (CompPowerBattery current2 in edifice.GetComps<CompPowerBattery>())
                    {
                        current2.AddEnergy(1/(float)electroRate);
                        break;
                        
                    }
                    break;

                }
            }
        }
        


    }
}

