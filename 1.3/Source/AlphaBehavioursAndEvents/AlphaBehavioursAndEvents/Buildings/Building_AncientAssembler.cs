using System.Collections.Generic;
using Verse;
using Verse.AI.Group;
using RimWorld;
using Verse.AI;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    public class Building_AncientAssembler : ThingWithComps
    {

        public static List<PawnKindDef> spawnablePawnKinds = new List<PawnKindDef>();
        
        private Lord lord;


        public override void ExposeData()
        {
            base.ExposeData();
            
        }

        public static void ResetStaticData()
        {
            Building_AncientAssembler.spawnablePawnKinds.Clear();
            spawnablePawnKinds.Add(PawnKindDef.Named("Mech_Centipede"));
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_Goliath"));
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_Siegebreaker"));
            spawnablePawnKinds.Add(PawnKindDef.Named("Mech_Pikeman"));
          
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (base.Faction == null)
            {
                spawnablePawnKinds.Add(PawnKindDef.Named("Mech_Centipede"));
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_Goliath"));
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_Siegebreaker"));
                spawnablePawnKinds.Add(PawnKindDef.Named("Mech_Pikeman"));
                Faction faction = Find.FactionManager.OfMechanoids;
                this.SetFaction(faction, null);
            }
            if (!respawningAfterLoad)
            {
                Faction faction = Find.FactionManager.OfMechanoids;
                this.SpawnInitialPawns(faction);
            }
        }

        private void SpawnInitialPawns(Faction faction)
        {
            if (this.lord == null)
            {
                IntVec3 invalid;
                if (!CellFinder.TryFindRandomCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) && this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)), out invalid, -1))
                {
                    Log.Error("Found no place for mechanoids to spawn " + this);
                    invalid = IntVec3.Invalid;
                }
                LordJob_AssaultColony lordJob = new LordJob_AssaultColony(this.Faction, false, false, true, true, false);
                this.lord = LordMaker.MakeNewLord(this.Faction, lordJob, this.Map, null);
                Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("AA_Demolisher"), faction);
                Thing spawnedCreature = GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 2, (IntVec3 c) => c.Standable(base.Map) &&
                base.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false))),
                base.Map, WipeMode.Vanish);
                this.lord.AddPawn(pawn);
                this.SpawnPawnsUntilPoints(10 , faction);
            }
        }

        public void SpawnPawnsUntilPoints(float points, Faction faction)
        {
           
            IEnumerable<PawnKindDef> source = spawnablePawnKinds;
            PawnKindDef kindDef;
            for (int remaining = (int)points; remaining > 0;)
            {
                if (source.TryRandomElement(out kindDef))
                {
                    Pawn pawn = PawnGenerator.GeneratePawn(kindDef, faction);
                    Thing spawnedCreature = GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 2, (IntVec3 c) => c.Standable(base.Map) &&
                      base.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false))),
                    base.Map, WipeMode.Vanish);
                    if (spawnedCreature != null) { this.lord.AddPawn(pawn); }

                    remaining --;

                }
            }


        }

















    }
}
