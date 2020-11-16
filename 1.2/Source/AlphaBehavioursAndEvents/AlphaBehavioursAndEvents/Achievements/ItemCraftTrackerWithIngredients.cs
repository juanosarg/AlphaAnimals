using System;
using System.Reflection;
using HarmonyLib;
using Verse;
using RimWorld;

namespace AchievementsExpanded
{
    public class ItemCraftTrackerWithIngredientsAlpha : Tracker<Thing>
    {

        public override string Key => "ItemCraftTrackerWithIngredientsAlpha";

        public override MethodInfo MethodHook => AccessTools.Method(typeof(QuestManager), nameof(QuestManager.Notify_ThingsProduced));
        public override MethodInfo PatchMethod => AccessTools.Method(typeof(AlphaAnimals_QuestManager_Notify_ThingsProduced_Patch), nameof(AlphaAnimals_QuestManager_Notify_ThingsProduced_Patch.CheckItemCraftedIngredients));
        protected override string[] DebugText => new string[] { $"Def: {def?.defName ?? "None"}",
                                                                $"MadeFrom: {madeFrom?.defName ?? "Any"}",
                                                                $"Quality: {quality}",
                                                                $"Count: {count}",
                                                                $"Current: {triggeredCount}" };

        public ItemCraftTrackerWithIngredientsAlpha()
        {
        }
        public override (float percent, string text) PercentComplete => count > 1 ? ((float)triggeredCount / count, $"{triggeredCount} / {count}") : base.PercentComplete;


        public ItemCraftTrackerWithIngredientsAlpha(ItemCraftTrackerWithIngredientsAlpha reference) : base(reference)
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
            if ((def is null || thing.def == def) && (madeFrom is null || madeFrom == thing.Stuff)) {

                CompIngredients comping = thing.TryGetComp<CompIngredients>();

                if (includeingredient is null || (comping != null && comping.ingredients.Contains(includeingredient)))
                {
                    if (quality is null || (thing.TryGetQuality(out var qc) && qc >= quality))
                    {
                        triggeredCount = triggeredCount + thing.stackCount;

                    }
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

