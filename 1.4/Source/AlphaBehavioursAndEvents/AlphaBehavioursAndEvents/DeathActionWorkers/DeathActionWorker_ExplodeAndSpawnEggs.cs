
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_ExplodeAndSpawnEggs : DeathActionWorker
    {

        System.Random rand = new System.Random();


        public override void PawnDied(Corpse corpse)
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
                if (rand.NextDouble() <= 0.3)
                {
                    numberOfEggs = 2;
                }
                Thing thing = ThingMaker.MakeThing(ThingDef.Named("AA_EggFlamingoPhoenixFertilized"), null);
                thing.stackCount = numberOfEggs;
                GenPlace.TryPlaceThing(thing, corpse.Position, corpse.Map, ThingPlaceMode.Near, null, null, default(Rot4));

                List<Thing> ignoredThings = new List<Thing>();

                ignoredThings.Add(thing);
               
                GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1, -1, null, null, null, null, null, 0f, 1, null,false, null, 0f, 1, 0, false, null, ignoredThings);



            }
            
        }


    }
}

