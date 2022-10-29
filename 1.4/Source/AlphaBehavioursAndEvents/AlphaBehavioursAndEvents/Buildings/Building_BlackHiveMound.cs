using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Verse;
using Verse.AI.Group;
using Verse.Sound;
using RimWorld;
using Verse.AI;

namespace AlphaBehavioursAndEvents
{
    public class Building_BlackHiveMound : ThingWithComps
    {

        public static List<PawnKindDef> spawnablePawnKinds = new List<PawnKindDef>();
        private Lord lord;


        public override void ExposeData()
        {
            base.ExposeData();
           // Scribe_Deep.Look<Lord>(ref this.lord, "lord", false);
           
        }

        public static void ResetStaticData()
        {
            Building_BlackHiveMound.spawnablePawnKinds.Clear();
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_MegaLouse"));
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_MammothWorm"));
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_BlackScarab"));
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_BlackSpelopede"));
            spawnablePawnKinds.Add(PawnKindDef.Named("AA_BlackSpider"));
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (base.Faction == null)
            {
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_MegaLouse"));
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_MammothWorm"));
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_BlackScarab"));
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_BlackSpelopede"));
                spawnablePawnKinds.Add(PawnKindDef.Named("AA_BlackSpider"));
                Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDef.Named("AA_BlackHive"));
                this.SetFaction(faction, null);
            }
            if (!respawningAfterLoad)
            {
                Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDef.Named("AA_BlackHive"));
                this.SpawnInitialPawns(faction);
            }
        }

        private void SpawnInitialPawns(Faction faction)
        {
            
                IntVec3 invalid;
                if (!CellFinder.TryFindRandomCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) && this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)), out invalid, -1))
                {
                    Log.Error("Found no place for insects to spawn " + this);
                    invalid = IntVec3.Invalid;
                }
                
                this.SpawnPawnsUntilPoints(200f, faction);
            
        }

        public void SpawnPawnsUntilPoints(float points, Faction faction)
        {
            IEnumerable<PawnKindDef> source = from x in spawnablePawnKinds
                                              where x.combatPower <= 500f
                                              select x;
            PawnKindDef kindDef;
            for (int remaining = (int)points; remaining > 0;)
            {
                if (source.TryRandomElement(out kindDef))
                {
                    Pawn pawn = PawnGenerator.GeneratePawn(kindDef, faction);
                    Thing spawnedCreature=GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 2, (IntVec3 c) => c.Standable(base.Map) &&
                    base.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false))),
                    base.Map, WipeMode.Vanish);
                    if (this.lord == null)
                    {
                        List<Pawn> list = new List<Pawn>();
                        list.Add(spawnedCreature as Pawn);
                        LordJob_AssaultColony lordJob = new LordJob_AssaultColony(this.Faction, false, false, false, false, false);
                        this.lord = LordMaker.MakeNewLord(this.Faction, lordJob, this.Map, list);
                        
                        
                    }else { this.lord.AddPawn(pawn); }

                   
                    
                    remaining -= (int)kindDef.combatPower;

                }
            }
           

        }

       



       

      

      


     




    }
}
