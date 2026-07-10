
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI.Group;
using Verse.Noise;
using Verse.Sound;
namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_PlantCacti : DeathActionWorker
    {
        private static readonly IntRange CountRange = new IntRange(3, 6); 

        private const int SpawnRadius = 4;

        public override void PawnDied(Corpse corpse, Lord previousLord)
        {
            if(ModLister.HasActiveModWithName("Alpha Biomes") && corpse.Map!=null)
            {
                int randomInRange = CountRange.RandomInRange;
                for (int i = 0; i < randomInRange; i++)
                {
                    if (!CellFinder.TryRandomClosewalkCellNear(corpse.Position, corpse.Map, SpawnRadius, out var result, (IntVec3 x) => CanSpawnAt(x, corpse.Map)))
                    {
                        break;
                    }
                    result.GetPlant(corpse.Map)?.Destroy();
                    GenSpawn.Spawn(InternalDefOf.AB_Plant_OcularCactus_Small, result, corpse.Map);
                   
                }
                InternalDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(corpse.Position, corpse.Map));
            }
        
        }

        private bool CanSpawnAt(IntVec3 c, Map map)
        {
            if (!c.Standable(map) || c.Fogged(map) || !c.GetRoom(map).PsychologicallyOutdoors || c.GetEdifice(map) != null)
            {
                return false;
            }
            Plant plant = c.GetPlant(map);
            if (plant != null && plant.def.plant.growDays > 10f)
            {
                return false;
            }
            List<Thing> thingList = c.GetThingList(map);
            for (int i = 0; i < thingList.Count; i++)
            {
                if (thingList[i].def == InternalDefOf.AB_Plant_OcularCactus_Small)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
