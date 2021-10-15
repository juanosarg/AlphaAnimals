
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_SummonTornado : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse)
        {
            if (corpse.Map != null) {
                ThingDef tornado = ThingDefOf.Tornado;
                Thing newTornado = GenSpawn.Spawn(tornado, corpse.Position, corpse.Map, WipeMode.Vanish);
            }
            
        }


    }
}
