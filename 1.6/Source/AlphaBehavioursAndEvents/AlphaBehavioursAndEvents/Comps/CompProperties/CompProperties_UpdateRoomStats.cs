
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class CompProperties_UpdateRoomStats : CompProperties
    {


        public int timer=100;


        public CompProperties_UpdateRoomStats()
        {
            this.compClass = typeof(CompUpdateRoomStats);
        }


    }
}