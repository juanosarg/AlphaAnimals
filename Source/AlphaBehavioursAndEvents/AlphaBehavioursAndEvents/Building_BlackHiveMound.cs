using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Verse;
using Verse.AI.Group;
using Verse.Sound;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class Building_BlackHiveMound : ThingWithComps
    {

        public bool hasSpawned = false;
        public static List<PawnKindDef> spawnablePawnKinds = new List<PawnKindDef>();






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
                    GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 2, null), base.Map, WipeMode.Vanish);
                    remaining-= (int)kindDef.combatPower;

                }
            }
           

        }

       



       

      

      


     




    }
}
