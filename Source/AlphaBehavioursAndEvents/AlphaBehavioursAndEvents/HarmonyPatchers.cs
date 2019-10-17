using Harmony;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System;
using Verse.AI;
using RimWorld.Planet;


// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace AlphaBehavioursAndEvents
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.alphaanimals");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }
  

    /*This Harmony Prefix makes the creature carry more weight
    */
    [HarmonyPatch(typeof(MassUtility))]
    [HarmonyPatch("Capacity")]
    public static class MassUtility_Capacity_Patch
    {
        [HarmonyPostfix]
        public static void MakeGigantelopesCarryMore(Pawn p, ref float __result)

        {
            bool flagIsCreatureMine = p.Faction != null && p.Faction.IsPlayer;
            bool flagDoesCreatureHaveTheHediffs = (p.TryGetComp<CompInitialHediff>() != null);
            bool flagCanCreatureCarryMore = false;
            if (flagDoesCreatureHaveTheHediffs)
            {
                flagCanCreatureCarryMore = (p.TryGetComp<CompInitialHediff>().Props.hediffname == "AA_CarryWeight");
            }
            

            
                               

            if (flagIsCreatureMine && flagCanCreatureCarryMore)
            {
                int factor = p.TryGetComp<CompInitialHediff>().phase;
                __result = p.BodySize * p.GetStatValue(StatDefOf.CarryingCapacity) + factor*factor;
            }

        }
    }

    /*This Harmony Prefix makes the creature carry more weight
*/
    [HarmonyPatch(typeof(PathGrid))]
    [HarmonyPatch("CalculatedCostAt")]
    public static class PathGrid_CalculatedCostAt_Patch
    {
        [HarmonyPostfix]
        public static void MakePawnNotCountTerrain(IntVec3 c,Pawn p, ref int __result)

        {
        /*    if (p.GetComp<CompFloating>().Props.isFloater) {
                bool flag = false;
                if (p.Map != null)
                {
                    TerrainDef terrainDef = p.Map.terrainGrid.TerrainAt(c);
                    if (terrainDef == null || (terrainDef.passability == Traversability.Impassable) && (!terrainDef.IsWater))
                    {
                        __result = 10000;
                    }
                    //int num = terrainDef.pathCost;
                    List<Thing> list = p.Map.thingGrid.ThingsListAt(c);
                    for (int i = 0; i < list.Count; i++)
                    {
                        Thing thing = list[i];
                        if (thing.def.passability == Traversability.Impassable)
                        {
                            __result = 10000;
                            
                        }
                        if (!PathGrid.IsPathCostIgnoreRepeater(thing.def) || !prevCell.IsValid || !this.ContainsPathCostIgnoreRepeater(prevCell))
                        {
                            int pathCost = thing.def.pathCost;
                            if (pathCost > num)
                            {
                                num = pathCost;
                            }
                        }
                        if (thing is Building_Door && prevCell.IsValid)
                        {
                            Building edifice = prevCell.GetEdifice(this.map);
                            if (edifice != null && edifice is Building_Door)
                            {
                                flag = true;
                            }
                        }
                    }
                    int num2 = SnowUtility.MovementTicksAddOn(this.map.snowGrid.GetCategory(c));
                    if (num2 > num)
                    {
                        num = num2;
                    }
                    if (flag)
                    {
                        num += 45;
                    }
                    if (perceivedStatic)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            IntVec3 b = GenAdj.AdjacentCellsAndInside[j];
                            IntVec3 c2 = c + b;
                            if (c2.InBounds(this.map))
                            {
                                Fire fire = null;
                                list = this.map.thingGrid.ThingsListAtFast(c2);
                                for (int k = 0; k < list.Count; k++)
                                {
                                    fire = (list[k] as Fire);
                                    if (fire != null)
                                    {
                                        break;
                                    }
                                }
                                if (fire != null && fire.parent == null)
                                {
                                    if (b.x == 0 && b.z == 0)
                                    {
                                        num += 1000;
                                    }
                                    else
                                    {
                                        num += 150;
                                    }
                                }
                            }
                        }
                    }
                    return num;

                }


            }
            
            
*/
        }
    }



    /*Buckle your belts or something, we are doing Transpilers!*/

    [HarmonyPatch(typeof(WildAnimalSpawner))]
    [HarmonyPatch("SpawnRandomWildAnimalAt")]
    public static class WildAnimalSpawner_SpawnRandomWildAnimalAt_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilg)
        {
            var codes = new List<CodeInstruction>(instructions);
            Label label = ilg.DefineLabel();
            int i = 0;
            foreach (CodeInstruction instruction in codes)
            {
                if (instruction.opcode == OpCodes.Stloc_0)
                {
                    codes[i + 1].labels.Add(label);
                    yield return new CodeInstruction(OpCodes.Stloc_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_0);//Load "PawnKindDef" local variable. 
                    yield return new CodeInstruction(OpCodes.Call, typeof(WildAnimalSpawner_SpawnRandomWildAnimalAt_Patch).GetMethod("DetectAlphaCreatureAndOptions"));
                    yield return new CodeInstruction(OpCodes.Brfalse, label);
                    yield return new CodeInstruction(OpCodes.Ret);
                }
                else
                {
                    yield return instruction;
                }

                i++;
            }

        }

        public static bool DetectAlphaCreatureAndOptions(PawnKindDef theCreature)
        {
            if (theCreature != null)
            {
                //Log.Message("Creature that was going to spawn was" + theCreature.defName);
                
                if (theCreature.defName.Contains("AA_"))
                {
                    
                    if (theCreature.defName.Contains("AA_Aerofleet") && AlphaAnimals_Settings.AA_AerofleetFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_AnimusVox") && AlphaAnimals_Settings.AA_AnimusVoxFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_ArcticLion") && AlphaAnimals_Settings.AA_ArcticLionFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Barbslinger") && AlphaAnimals_Settings.AA_BarbslingerFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_BedBug") && AlphaAnimals_Settings.AA_BedBugFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Blizzarisk") && AlphaAnimals_Settings.AA_BlizzariskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_BoulderMit") && AlphaAnimals_Settings.AA_BoulderMitFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Bumbledrone") && AlphaAnimals_Settings.AA_BumbledroneFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Cactipine") && AlphaAnimals_Settings.AA_CactipineFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_DecayDrake") && AlphaAnimals_Settings.AA_DecayDrakeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Dunealisk") && AlphaAnimals_Settings.AA_DunealiskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Feralisk") && AlphaAnimals_Settings.AA_FeraliskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_FissionMouse") && AlphaAnimals_Settings.AA_FissionMouseFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Frostmite") && AlphaAnimals_Settings.AA_FrostmiteFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Gigantelope") && AlphaAnimals_Settings.AA_GigantelopeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_GreatDevourer") && AlphaAnimals_Settings.AA_GreatDevourerFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Groundrunner") && AlphaAnimals_Settings.AA_GroundrunnerFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Lockjaw") && AlphaAnimals_Settings.AA_LockjawFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MammothWorm") && AlphaAnimals_Settings.AA_MammothWormFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Mantrap") && AlphaAnimals_Settings.AA_MantrapFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MeadowAve") && AlphaAnimals_Settings.AA_MeadowAveFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MegaLouse") && AlphaAnimals_Settings.AA_MegaLouseFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Needlepost") && AlphaAnimals_Settings.AA_NeedlepostFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Needleroll") && AlphaAnimals_Settings.AA_NeedlerollFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Nightling") && AlphaAnimals_Settings.AA_NightlingFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_OcularJelly") && AlphaAnimals_Settings.AA_OcularJellyFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_OvergrownColossus") && AlphaAnimals_Settings.AA_OvergrownColossusFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_PebbleMit") && AlphaAnimals_Settings.AA_PebbleMitFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Radyak") && AlphaAnimals_Settings.AA_RadyakFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_RaptorShrimp") && AlphaAnimals_Settings.AA_RaptorShrimpFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_SandLion") && AlphaAnimals_Settings.AA_SandLionFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_SandSquid") && AlphaAnimals_Settings.AA_SandSquidFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_ShockGoat") && AlphaAnimals_Settings.AA_ShockGoatFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Slurrypede") && AlphaAnimals_Settings.AA_SlurrypedeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_TetraSlug") && AlphaAnimals_Settings.AA_TetraSlugFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Thermadon") && AlphaAnimals_Settings.AA_ThermadonFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Wildpawn") && AlphaAnimals_Settings.AA_WildpawnFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Wildpod") && AlphaAnimals_Settings.AA_WildpodFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Agaripod") && AlphaAnimals_Settings.AA_AgaripodFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MycoidColossus") && AlphaAnimals_Settings.AA_MycoidColossusFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Junglelisk") && AlphaAnimals_Settings.AA_JungleliskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_RedSpore") && AlphaAnimals_Settings.AA_RedSporeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_GreenGoo") && AlphaAnimals_Settings.AA_GreenGooFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_RedGoo") && AlphaAnimals_Settings.AA_RedGooFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_InfectedAerofleet") && AlphaAnimals_Settings.AA_InfectedAerofleetFlag)
                    {
                        return true;
                    }


                    return false;
                    
                } else return false;
            }
            else return false;
            


        }
    }








}
