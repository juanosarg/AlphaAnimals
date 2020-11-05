using System;
using System.Reflection;
using HarmonyLib;
using Verse;
using RimWorld;

namespace AchievementsExpanded
{
    public class ItemCraftTrackerWithIngredients:ItemCraftTracker
    {
       
        public ItemCraftTrackerWithIngredients()
        {
        }

        public ItemCraftTrackerWithIngredients(ItemCraftTrackerWithIngredients reference) : base(reference)
        {
            includeingredient = reference.includeingredient;
           
        }

        public override void ExposeData()
        {
            base.ExposeData();
           
            Scribe_Values.Look(ref includeingredient, "includeingredient");
          
        }

        public override bool Trigger(Thing thing)
        {
            base.Trigger(thing);
            if ((def is null || thing.def == def) && (madeFrom is null || madeFrom == thing.Stuff)
                && (includeingredient is null || (thing.TryGetComp<CompIngredients>().ingredients.Contains(includeingredient)))
                )
            {
                if (quality is null || (thing.TryGetQuality(out var qc) && qc >= quality))
                {
                    triggeredCount++;
                }
            }
            return triggeredCount >= count;
        }

        public ThingDef includeingredient;
      
        private int triggeredCount;
    }
}

