using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public static class FixedColonyAnimalsDebug
    {
        private static CellRect overRect;

        private static BoolGrid usedCells;

        private const int OverRectSize = 100;

        private static Map Map
        {
            get
            {
                return Find.CurrentMap;
            }
        }

        [DebugAction("Autotests", "Make colony (animals, fixed)",false,false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void MakeColonyAnimals()
        {
            FixedColonyAnimalsDebug.MakeColony(new ColonyMakerFlag[1]);
        }

        private static void DeleteAllSpawnedPawns()
        {
            foreach (Pawn pawn in FixedColonyAnimalsDebug.Map.mapPawns.AllPawnsSpawned.ToList<Pawn>())
            {
                pawn.Destroy(DestroyMode.Vanish);
                pawn.relations.ClearAllRelations();
            }
            Find.GameEnder.gameEnding = false;
        }


        public static void MakeColony(params ColonyMakerFlag[] flags)
        {
            bool godMode = DebugSettings.godMode;
            DebugSettings.godMode = true;
            Thing.allowDestroyNonDestroyable = true;
            if (FixedColonyAnimalsDebug.usedCells == null)
            {
                FixedColonyAnimalsDebug.usedCells = new BoolGrid(FixedColonyAnimalsDebug.Map);
            }
            else
            {
                FixedColonyAnimalsDebug.usedCells.ClearAndResizeTo(FixedColonyAnimalsDebug.Map);
            }
            FixedColonyAnimalsDebug.overRect = new CellRect(FixedColonyAnimalsDebug.Map.Center.x - 50, FixedColonyAnimalsDebug.Map.Center.z - 50, 100, 100);
            FixedColonyAnimalsDebug.DeleteAllSpawnedPawns();
            GenDebug.ClearArea(FixedColonyAnimalsDebug.overRect, Find.CurrentMap);
            if (flags.Contains(ColonyMakerFlag.Animals))
            {
                foreach (PawnKindDef pawnKindDef in from k in DefDatabase<PawnKindDef>.AllDefs
                                                    where k.RaceProps.Animal
                                                    select k)
                {
                    CellRect cellRect;
                    if (!FixedColonyAnimalsDebug.TryGetFreeRect(6, 3, out cellRect))
                    {
                        return;
                    }
                    cellRect = cellRect.ContractedBy(1);
                    foreach (IntVec3 c in cellRect)
                    {
                        FixedColonyAnimalsDebug.Map.terrainGrid.SetTerrain(c, TerrainDefOf.Concrete);
                    }
                    GenSpawn.Spawn(PawnGenerator.GeneratePawn(pawnKindDef, null), cellRect.Cells.ElementAt(0), FixedColonyAnimalsDebug.Map, WipeMode.Vanish);
                    IntVec3 intVec = cellRect.Cells.ElementAt(1);
                    Pawn deadpawn = (Pawn)GenSpawn.Spawn(PawnGenerator.GeneratePawn(pawnKindDef, null), intVec, FixedColonyAnimalsDebug.Map, WipeMode.Vanish);
                    deadpawn.Kill(null);
                    CompRottable compRottable = ((Corpse)intVec.GetThingList(Find.CurrentMap).First((Thing t) => t is Corpse)).TryGetComp<CompRottable>();
                    if (compRottable != null)
                    {
                        compRottable.RotProgress += 1200000f;
                    }
                    if (pawnKindDef.RaceProps.leatherDef != null)
                    {
                        GenSpawn.Spawn(pawnKindDef.RaceProps.leatherDef, cellRect.Cells.ElementAt(2), FixedColonyAnimalsDebug.Map, WipeMode.Vanish);
                    }
                    if (pawnKindDef.RaceProps.meatDef != null)
                    {
                        GenSpawn.Spawn(pawnKindDef.RaceProps.meatDef, cellRect.Cells.ElementAt(3), FixedColonyAnimalsDebug.Map, WipeMode.Vanish);
                    }
                }
            }

            FixedColonyAnimalsDebug.ClearAllHomeArea();
            FixedColonyAnimalsDebug.FillWithHomeArea(FixedColonyAnimalsDebug.overRect);
            DebugSettings.godMode = godMode;
            Thing.allowDestroyNonDestroyable = false;
        }
        private static bool TryGetFreeRect(int width, int height, out CellRect result)
        {
            for (int i = FixedColonyAnimalsDebug.overRect.minZ; i <= FixedColonyAnimalsDebug.overRect.maxZ - height; i++)
            {
                for (int j = FixedColonyAnimalsDebug.overRect.minX; j <= FixedColonyAnimalsDebug.overRect.maxX - width; j++)
                {
                    CellRect cellRect = new CellRect(j, i, width, height);
                    bool flag = true;
                    for (int k = cellRect.minZ; k <= cellRect.maxZ; k++)
                    {
                        for (int l = cellRect.minX; l <= cellRect.maxX; l++)
                        {
                            if (FixedColonyAnimalsDebug.usedCells[l, k])
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            break;
                        }
                    }
                    if (flag)
                    {
                        result = cellRect;
                        for (int m = cellRect.minZ; m <= cellRect.maxZ; m++)
                        {
                            for (int n = cellRect.minX; n <= cellRect.maxX; n++)
                            {
                                IntVec3 c = new IntVec3(n, 0, m);
                                FixedColonyAnimalsDebug.usedCells.Set(c, true);
                                if (c.GetTerrain(Find.CurrentMap).passability == Traversability.Impassable)
                                {
                                    FixedColonyAnimalsDebug.Map.terrainGrid.SetTerrain(c, TerrainDefOf.Concrete);
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            result = new CellRect(0, 0, width, height);
            return false;
        }
        private static void ClearAllHomeArea()
        {
            foreach (IntVec3 c in FixedColonyAnimalsDebug.Map.AllCells)
            {
                FixedColonyAnimalsDebug.Map.areaManager.Home[c] = false;
            }
        }

        private static void FillWithHomeArea(CellRect r)
        {
            new Designator_AreaHomeExpand().DesignateMultiCell(r.Cells);
        }

    }
}
