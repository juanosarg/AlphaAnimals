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
using RimWorld.Planet;



namespace AlphaBehavioursAndEvents
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.alphaanimals");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }
  


  



   







}
