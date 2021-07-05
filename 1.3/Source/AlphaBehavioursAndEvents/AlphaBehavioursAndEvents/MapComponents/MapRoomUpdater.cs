using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace AlphaBehavioursAndEvents
{
    public class MapRoomUpdater : MapComponent
    {

        //This class updates all the rooms in the map every 2000 ticks if a pedigreed raptor is in the map

        public HashSet<Thing> raptors_InMap = new HashSet<Thing>();
        public int tickCounter = 0;
        public int tickInterval = 2000;


        public MapRoomUpdater(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {

            base.FinalizeInit();

        }

        public void AddObjectToMap(Thing thing)
        {
            if (!raptors_InMap.Contains(thing))
            {
                raptors_InMap.Add(thing);
            }

        }

        public void RemoveObjectFromMap(Thing thing)
        {
            if (raptors_InMap.Contains(thing))
            {
                raptors_InMap.Remove(thing);
            }

        }

        public override void MapComponentTick()
        {
           
            tickCounter++;
            if ((tickCounter > tickInterval)&& raptors_InMap.Count>0)
            {
              
                foreach (Room room in this.map.regionGrid.allRooms) {
                    room.Notify_TerrainChanged();
                }
              
                tickCounter = 0;
            }



        }


    }


}
