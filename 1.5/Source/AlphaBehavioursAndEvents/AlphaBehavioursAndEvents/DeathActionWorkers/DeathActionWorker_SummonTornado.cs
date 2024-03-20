
using RimWorld;
using Verse;
using Verse.AI.Group;
namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_SummonTornado : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse, Lord previousLord)
        {
            if (corpse.Map != null) {
                ThingDef tornado = InternalDefOf.Tornado;
                Thing newTornado = GenSpawn.Spawn(tornado, corpse.Position, corpse.Map, WipeMode.Vanish);
            }
            
        }


    }
}
