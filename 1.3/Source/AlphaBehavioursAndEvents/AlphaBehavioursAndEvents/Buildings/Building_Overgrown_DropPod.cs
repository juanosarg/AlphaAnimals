
using Verse;
using System.Text;
using Verse.Sound;
using RimWorld;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class Building_Overgrown_DropPod : Building
    {
      

        public int nextPawnSpawnTick = 0;
        public int ticksInday = 60000;
        private static readonly IntRange CountRange = new IntRange(10, 20);
        public int lifespanTicks = 180000;
        public int age = -1;


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.age, "age", 0, false);
        }

        public override void Tick()
        {
            base.Tick();
            if (base.Spawned)
            {
                nextPawnSpawnTick++;
                if (nextPawnSpawnTick > 7500)
                {
                    Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDefOf.Insect);
                    Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("AA_Cactipine"), faction);
                   
                    GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 1, null), base.Map, WipeMode.Vanish);
                    nextPawnSpawnTick = 0;
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, false, false, null, false);

                    SoundDefOf.Hive_Spawn.PlayOneShot(this);

                }
            }

            this.age++;
            if (this.age >= this.lifespanTicks)
            {
                Map map = base.Map;

                Thing thing = null;
                int randomInRange = Building_Overgrown_DropPod.CountRange.RandomInRange;
                for (int i = 0; i < randomInRange; i++)
                {
                    IntVec3 intVec;
                    if (!CellFinder.TryRandomClosewalkCellNear(base.Position, map, 6, out intVec, (IntVec3 x) => this.CanSpawnAt(x, map)))
                    {
                        break;
                    }
                    Plant plant = intVec.GetPlant(map);
                    if (plant != null)
                    {
                        plant.Destroy(DestroyMode.Vanish);
                    }
                    Thing thing2 = GenSpawn.Spawn(ThingDef.Named("AA_Heat_Ambrosia"), intVec, map, WipeMode.Vanish);
                    if (thing == null)
                    {
                        thing = thing2;
                    }
                   
                }
                Thing thingslag = ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel, null);
                GenPlace.TryPlaceThing(thingslag, base.Position, map, ThingPlaceMode.Near, null, null);
                this.Destroy(DestroyMode.Vanish);
            }
        }

       

        private bool CanSpawnAt(IntVec3 c, Map map)
        {
            if (!c.Standable(map) || c.Fogged(map) || map.fertilityGrid.FertilityAt(c) < ThingDef.Named("AA_Heat_Ambrosia").plant.fertilityMin || !c.GetRoom(map).PsychologicallyOutdoors || c.GetEdifice(map) != null)
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
                if (thingList[i].def == ThingDefOf.Plant_Ambrosia)
                {
                    return false;
                }
            }
            return true;
        }


        public override string GetInspectString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            string ageInDesc = "";
           
            int num = this.lifespanTicks - this.age;
            if (num > 0)
            {
                ageInDesc = "LifespanExpiry".Translate() + " " + num.ToStringTicksToPeriod();
              
            }
           


            float totalProgress = ((float)nextPawnSpawnTick / (float)(7500));
             
            stringBuilder.Append(ageInDesc+"\nGenerating terraforming lifeform: "+ totalProgress.ToStringPercent());

            return stringBuilder.ToString();


           

        }









    }
}
