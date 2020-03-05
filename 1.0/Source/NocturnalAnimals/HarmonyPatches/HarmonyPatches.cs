using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;

namespace NocturnalAnimals
{

    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        
        static HarmonyPatches()
        {
            var h = HarmonyInstance.Create("XeoNovaDan.NocturnalAnimals");
            h.PatchAll();
        }

    }

}
