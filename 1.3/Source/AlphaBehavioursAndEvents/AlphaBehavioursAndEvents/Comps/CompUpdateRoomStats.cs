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
            if (this.parent.Map != null)
            {
                tickCounter++;
                if (tickCounter > Props.timer)
                {

                    this.parent.GetRoom().Notify_TerrainChanged();
                    tickCounter = 0;
                }
            }
            


        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            if (this.parent.Map != null)
            {
                MapRoomUpdater mapComp = this.parent.Map.GetComponent<MapRoomUpdater>();
                if (mapComp != null)
                {
                    mapComp.AddObjectToMap(this.parent);
                }
            }

        }

        public override void PostDeSpawn(Map map)
        {
            if (map != null)
            {
                MapRoomUpdater mapComp = map.GetComponent<MapRoomUpdater>();
                if (mapComp != null)
                {
                    mapComp.RemoveObjectFromMap(this.parent);
                }
            }
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if (previousMap != null)
            {
                MapRoomUpdater mapComp = previousMap.GetComponent<MapRoomUpdater>();
                if (mapComp != null)
                {
                    mapComp.RemoveObjectFromMap(this.parent);
                }
            }

        }




    }
}