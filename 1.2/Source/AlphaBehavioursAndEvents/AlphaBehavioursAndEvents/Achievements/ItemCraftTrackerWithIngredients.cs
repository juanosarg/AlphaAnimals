using System;
using System.Reflection;
using HarmonyLib;
using Verse;
using RimWorld;

namespace AchievementsExpanded
{
    public class ItemCraftTrackerWithIngredients : Tracker<Thing>
    {

        public override string Key => "ItemCraftTrackerWithIngredients";

        public override MethodInfo MethodHook => AccessTools.Method(typeof(QuestManager), nameof(QuestManager.Notify_ThingsProduced));
        public override MethodInfo PatchMethod => AccessTools.Method(typeof(QuestManager_Notify_ThingsProduced_Patch), nameof(QuestManager_Notify_ThingsProduced_Patch.CheckItemCraftedIngredients));
        protected override string[] DebugText => new string[] { $"Def: {def?.defName ?? "None"}",
                                                                $"MadeFrom: {madeFrom?.defName ?? "Any"}",
                                                                $"Quality: {quality}",
                                                                $"Count: {count}",
                                                                $"Current: {triggeredCount}" };

        public ItemCraftTrackerWithIngredients()
        {
        }

        public ItemCraftTrackerWithIngredients(ItemCraftTrackerWithIngredients reference) : base(reference)
        {
            def = reference.def;
            madeFrom = reference.madeFrom;
            quality = reference.quality;
            count = reference.count;
            triggeredCount = 0;
            includeingredient = reference.includeingredient;

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Defs.Look(ref def, "def");
            Scribe_Defs.Look(ref madeFrom, "madeFrom");
            Scribe_Values.Look(ref quality, "quality");
            Scribe_Values.Look(ref count, "count");
            Scribe_Values.Look(ref triggeredCount, "triggeredCount");
            Scribe_Defs.Look(ref includeingredient, "includeingredient");

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
                    triggeredCount = triggeredCount + thing.stackCount;

                }
            }
            return triggeredCount >= count;
        }

        public ThingDef includeingredient;
        public ThingDef def;
        public ThingDef madeFrom;
        public QualityCategory? quality;
        public int count;

        private int triggeredCount;
    }
}

