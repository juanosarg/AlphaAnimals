using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaBehavioursAndEvents
{
    /*
    [HarmonyPatch(typeof(PawnUtility))]
    [HarmonyPatch("IsFighting")]
   
    public static class AlphaAnimals_PawnUtility_IsFighting_Patch
    {

        public static List<PawnKindDef> blachHive = new List<PawnKindDef>() { InternalDefOf.AA_BlackScarab,InternalDefOf.AA_BlackSpelopede,
        InternalDefOf.AA_BlackSpider,InternalDefOf.AA_MegaLouse,InternalDefOf.AA_MammothWorm};


        [HarmonyPostfix]
        public static void DisableBlackHive(Pawn pawn, ref bool __result)

        {
            if(pawn!=null && blachHive.Contains(pawn.kindDef) && pawn.CurJob != null) { __result = true; }
                


        }
    }*/

    [HarmonyPatch(typeof(RimWorld.PawnUtility), nameof(RimWorld.PawnUtility.IsFighting))]
    public static class AlphaAnimals_PawnUtility_IsFighting_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> DisableBlackHive_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            MethodInfo getJobGetter = AccessTools.PropertyGetter(typeof(Verse.Pawn), nameof(Verse.Pawn.CurJob));
            FieldInfo turretField = AccessTools.Field(typeof(RimWorld.JobDefOf), nameof(RimWorld.JobDefOf.ManTurret));
            MethodInfo methodInsert = AccessTools.Method(typeof(AlphaAnimals_PawnUtility_IsFighting_Patch), nameof(HashSetFilter));
            FieldInfo valueField = AccessTools.Field(typeof(Verse.RaceProperties), nameof(Verse.RaceProperties.meatMarketValue));
            FieldInfo kindField = AccessTools.Field(typeof(Verse.Pawn), nameof(Verse.Pawn.kindDef));
            FieldInfo raceThingField = AccessTools.Field(typeof(Verse.PawnKindDef), nameof(Verse.PawnKindDef.race));
            FieldInfo raceRaceField = AccessTools.Field(typeof(Verse.ThingDef), nameof(Verse.ThingDef.race));

            bool errored = false;
            if (getJobGetter == null) { Verse.Log.Error($"Failed to get: '{nameof(getJobGetter)}'!"); errored = true; }
            if (turretField == null) { Verse.Log.Error($"Failed to get: '{nameof(turretField)}'!"); errored = true; }
            if (methodInsert == null) { Verse.Log.Error($"Failed to get: '{nameof(methodInsert)}'!"); errored = true; }
            if (valueField == null) { Verse.Log.Error($"Failed to get: '{nameof(valueField)}'!"); errored = true; }
            if (kindField == null) { Verse.Log.Error($"Failed to get: '{nameof(kindField)}'!"); errored = true; }
            if (raceThingField == null) { Verse.Log.Error($"Failed to get: '{nameof(raceThingField)}'!"); errored = true; }
            if (raceRaceField == null) { Verse.Log.Error($"Failed to get: '{nameof(raceRaceField)}'!"); errored = true; }

            bool insideFirstIf = false;
            bool foundTurretManFieldLoad = false;
            bool readyToInsert = false;
            bool insertionDone = false;
            foreach (CodeInstruction instruction in codeInstructions)
            {
                if (errored) { yield return instruction; continue; }
                else if (!insideFirstIf && instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo m && m == getJobGetter) { insideFirstIf = true; }
                else if (insideFirstIf && !foundTurretManFieldLoad && instruction.LoadsField(turretField)) { foundTurretManFieldLoad = true; }
                else if (!readyToInsert && insideFirstIf && foundTurretManFieldLoad && instruction.opcode == OpCodes.Ceq) { readyToInsert = true; }
                else if (readyToInsert && !insertionDone)
                {
                    Label myLabel = new Label();
                    instruction.labels.Add(myLabel);
                    yield return new CodeInstruction(OpCodes.Dup); //Duplicate the result of the original method
                    yield return new CodeInstruction(OpCodes.Brtrue, myLabel); //Return true if original is true (jumps to the return statement and return orignial value) - This consumes the duplicated value
                    yield return new CodeInstruction(OpCodes.Ldarg_0); //Push the pawn to the stack
                    yield return new CodeInstruction(OpCodes.Ldfld, kindField); //Load PawnKindDef field of the pawn - This consumes the pawn
                    yield return new CodeInstruction(OpCodes.Ldfld, raceThingField); //Load the race (ThingDef) field from the kind - This consumes the kind
                    yield return new CodeInstruction(OpCodes.Ldfld, raceRaceField); //Load the race (RaceProperties) field from the race ThingDef - This consumes the ThingDef
                    yield return new CodeInstruction(OpCodes.Ldfld, valueField); //Load the meat market value - This consumes the race
                    yield return new CodeInstruction(OpCodes.Ldc_R4, 0.5f); //Push the value of the insect meat to stack (0.5f in the BaseInsect2 def)
                    yield return new CodeInstruction(OpCodes.Bgt, myLabel); //If meat value of the pawn is greater than that of the insects -> return original value
                    yield return new CodeInstruction(OpCodes.Ldarg_0); //Push the pawn to stack again
                    yield return new CodeInstruction(OpCodes.Call, methodInsert); //Call the filter method - original value is still on stack
                    insertionDone = true;
                }
                yield return instruction;
            }
            if (!insertionDone || errored)
            {
                Verse.Log.Error("Transpile failed!");
            }
        }

        public static HashSet<PawnKindDef> blachHive = new HashSet<PawnKindDef>{
            InternalDefOf.AA_BlackScarab,
            InternalDefOf.AA_BlackSpelopede,
            InternalDefOf.AA_BlackSpider,
            InternalDefOf.AA_MegaLouse,
            InternalDefOf.AA_MammothWorm
        };

        /// <summary>If the pre filter didnt apply, we check against the blachHive HashSet</summary>
        /// <param name="inputValue">The original result, should realistically aloways be false at this point because of the pre filtering</param>
        /// <param name="pawn">The pawn we are comparing</param>
        /// <returns>True if the pawn is a black hive pawn or inputValue was true</returns>
        public static bool HashSetFilter(bool inputValue, Pawn pawn) => inputValue || blachHive.Contains(pawn.kindDef);
    }

}
