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
                __result = p.BodySize * 35f + factor*factor;
            }

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
                    
                    if (theCreature.defName.Contains("AA_Aerofleet") &&LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_AerofleetFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_AnimusVox") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_AnimusVoxFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_ArcticLion") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_ArcticLionFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Barbslinger") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_BarbslingerFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_BedBug") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_BedBugFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Blizzarisk") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_BlizzariskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_BoulderMit") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_BoulderMitFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Bumbledrone") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_BumbledroneFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Cactipine") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_CactipineFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_DecayDrake") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_DecayDrakeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Dunealisk") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_DunealiskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Feralisk") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_FeraliskFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_FissionMouse") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_FissionMouseFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Frostmite") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_FrostmiteFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Gigantelope") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_GigantelopeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_GreatDevourer") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_GreatDevourerFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Groundrunner") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_GroundrunnerFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Lockjaw") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_LockjawFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MammothWorm") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_MammothWormFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Mantrap") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_MantrapFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MeadowAve") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_MeadowAveFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_MegaLouse") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_MegaLouseFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Needlepost") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_NeedlepostFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Needleroll") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_NeedlerollFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Nightling") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_NightlingFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_OcularJelly") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_OcularJellyFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_OvergrownColossus") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_OvergrownColossusFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_PebbleMit") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_PebbleMitFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Radyak") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_RadyakFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_RaptorShrimp") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_RaptorShrimpFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_SandLion") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_SandLionFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_SandSquid") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_SandSquidFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_ShockGoat") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_ShockGoatFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Slurrypede") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_SlurrypedeFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_TetraSlug") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_TetraSlugFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Thermadon") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_ThermadonFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Wildpawn") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_WildpawnFlag)
                    {
                        return true;
                    }
                    if (theCreature.defName.Contains("AA_Wildpod") && LoadedModManager.GetMod<AlphaAnimals_Mod>().GetSettings<AlphaAnimals_Settings>().AA_WildpodFlag)
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
