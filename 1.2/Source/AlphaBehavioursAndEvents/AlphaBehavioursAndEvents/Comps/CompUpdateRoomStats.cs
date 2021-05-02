using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using System;
using System.Reflection;

namespace AlphaBehavioursAndEvents
{
    public class CompUpdateRoomStats : ThingComp
    {

        public int tickCounter = 0;


        public CompProperties_UpdateRoomStats Props
        {
            get
            {
                return (CompProperties_UpdateRoomStats)this.props;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;
            if (tickCounter > Props.timer){

                this.parent.GetRoom().Notify_TerrainChanged();
                tickCounter = 0;
            }
            


        }




    }
}