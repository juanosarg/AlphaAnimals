
using RimWorld;
using Verse;
using System.Collections.Generic;
using Verse.AI.Group;
namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_ExplodeAndSpawnEggs : DeathActionWorker
    {


        public override void PawnDied(Corpse corpse, Lord previousLord)
        {
            if (corpse.Map != null) {
                float radius;
                if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
                {
                    radius = 3.9f;
                }
                else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
                {
                    radius = 4.9f;
                }
                else
                {
                    radius = 5.9f;
                }
                int numberOfEggs = 1;
                if (Rand.Value <= 0.3)
                {
                    numberOfEggs = 2;
                }
                Thing thing = ThingMaker.MakeThing(InternalDefOf.AA_EggFlamingoPhoenixFertilized, null);
                thing.stackCount = numberOfEggs;
                GenPlace.TryPlaceThing(thing, corpse.Position, corpse.Map, ThingPlaceMode.Near, null, null, default(Rot4));

                List<Thing> ignoredThings = new List<Thing>();

                ignoredThings.Add(thing);
               
                GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1, -1, null, null, null, null, null, 0f, 1, null, null, 255,false, null, 0f, 1, 0, false, null, ignoredThings);



            }
            
        }


    }
}

