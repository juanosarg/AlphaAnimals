



using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;
using System;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_SpawnOnRadius : Ability
    {



        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            Ability_SpawnOnRadius_Extension extension = this.def.GetModExtension<Ability_SpawnOnRadius_Extension>();
            Random rand = new Random();
            foreach (GlobalTargetInfo target in targets)
            {
                
                if (rand.NextDouble() < extension.probability)
                {
                   
                    ThingDef newThing = extension.thingToSpawn;
                    Thing spawnedThing = GenSpawn.Spawn(newThing, target.Cell, target.Map, WipeMode.Vanish);
                }
               




            }
        }
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (!base.ValidateTarget(target, showMessages)) return false;
            Ability_SpawnOnRadius_Extension extension = this.def.GetModExtension<Ability_SpawnOnRadius_Extension>();

            if (extension.allowAnAmount && this.pawn.Map.listerThings.ThingsOfDef(extension.thingToSpawn).Count > extension.allowedAmount - 1)
            {
                if (showMessages) Messages.Message("AA_OnlyNumberAllowed".Translate(extension.allowedAmount), MessageTypeDefOf.RejectInput, false);
                return false;
            }



            return true;
        }
    }
}
