using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;


namespace AlphaBehavioursAndEvents
{
    public class AlphaAnimals_Settings : ModSettings

    {
        private static Vector2 scrollPosition = Vector2.zero;
        public Dictionary<string, bool> pawnSpawnStates = new Dictionary<string, bool>();
        public bool flagVanillaAnimals = true;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref pawnSpawnStates, "pawnSpawnStates", LookMode.Value, LookMode.Value, ref pawnKeys, ref boolValues);
            Scribe_Values.Look(ref flagVanillaAnimals, "flagVanillaAnimals", true, true);



        }
        private List<string> pawnKeys;
        private List<bool> boolValues;
        private string searchKey;



        public void DoWindowContents(Rect inRect)
        {

            var rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            Verse.Text.Anchor = TextAnchor.MiddleLeft;
            var searchLabel = new Rect(rect.x + 5, rect.y, 60, 24);
            Widgets.Label(searchLabel, "VEF_AnimalsSearch".Translate());
            var searchRect = new Rect(searchLabel.xMax + 5, searchLabel.y, 200, 24f);
            searchKey = Widgets.TextField(searchRect, searchKey);
            Verse.Text.Anchor = TextAnchor.UpperLeft;

            List<string> keys = pawnSpawnStates.Keys.ToList().OrderBy(x => DefDatabase<ThingDef>.GetNamedSilentFail(x)?.label)?.Where(x => DefDatabase<ThingDef>.GetNamedSilentFail(x)?.label.ToLower().
            Contains(searchKey.ToLower()) == true)?.ToList();
            Listing_Standard ls = new Listing_Standard();
            Rect rectExt = new Rect(inRect.x, searchRect.yMax + 35, inRect.width, inRect.height - 70);
            Rect rect2 = new Rect(0f, 0f, inRect.width - 30f, keys.Count * 24 + 24);
            Widgets.BeginScrollView(rectExt, ref scrollPosition, rect2, true);
            
            ls.Begin(rect2);

            var allowAnimalsRect = new Rect(inRect.x, 0, inRect.width - 70f, 24);
            Widgets.CheckboxLabeled(allowAnimalsRect, "allowVanillaAnimals".Translate(), ref flagVanillaAnimals);

            
            if(keys.Count > 0) {
                for (int num = 0; num < keys.Count; num++)
                {

                    bool test = pawnSpawnStates[keys[num]];
                    if (DefDatabase<PawnKindDef>.GetNamedSilentFail(keys[num]) == null)
                    {
                        pawnSpawnStates.Remove(keys[num]);
                    }
                    else
                    {
                        var iconRect = new Rect(0, (num + 1) * 24, 24, 24);
                        var labelRect = new Rect(30, (num + 1) * 24, inRect.width - 100f, 24);
                        Widgets.ThingIcon(iconRect, PawnKindDef.Named(keys[num]).race);
                        Widgets.CheckboxLabeled(labelRect, "AA_DisableAnimal".Translate(PawnKindDef.Named(keys[num]).LabelCap), ref test);


                        pawnSpawnStates[keys[num]] = test;
                    }
                }

            }
            
           
            ls.End();
            Widgets.EndScrollView();
            base.Write();


        }


    }

    public class AlphaAnimalsEvents_Settings : ModSettings

    {

        
        public  bool flagBlackHiveRaids = true;
        public  bool flagStalkingLions = true;
        public  bool flagCactipineDroppods = true;
        public  bool flagSpiderClutchMothers = true;
        public  bool flagAerofleets = true;
        public  bool flagGallatross = true;
        public bool flagBehemoth = true;
        public  bool flagSummitCrab = true;
        public  bool flagBumbledrones = true;      
        public  bool flagAsteroids = true;
        public  bool flagAnimalisk = true;
        public  bool flagMime = true;
        public bool flagFungalHusk = true;


        public const float alphaAnimalSpawnMultiplierBase = 1;
        public float alphaAnimalSpawnMultiplier = alphaAnimalSpawnMultiplierBase;



        public override void ExposeData()
        {
            base.ExposeData();
          
            Scribe_Values.Look(ref flagBlackHiveRaids, "flagBlackHiveRaids", true, true);
            Scribe_Values.Look(ref flagStalkingLions, "flagStalkingLions", true, true);
            Scribe_Values.Look(ref flagCactipineDroppods, "flagCactipineDroppods", true, true);
            Scribe_Values.Look(ref flagSpiderClutchMothers, "flagSpiderClutchMothers", true, true);
            Scribe_Values.Look(ref flagAerofleets, "flagAerofleets", true, true);
            Scribe_Values.Look(ref flagGallatross, "flagGallatross", true, true);
            Scribe_Values.Look(ref flagBehemoth, "flagBehemoth", true, true);
            Scribe_Values.Look(ref flagSummitCrab, "flagSummitCrab", true, true);
            Scribe_Values.Look(ref flagBumbledrones, "flagBumbledrones", true, true);         
            Scribe_Values.Look(ref flagAsteroids, "flagAsteroid", true, true);
            Scribe_Values.Look(ref flagAnimalisk, "flagAnimalisk", true, true);
            Scribe_Values.Look(ref flagMime, "flagMime", true, true);
            Scribe_Values.Look(ref flagFungalHusk, "flagFungalHusk", true, true);

            Scribe_Values.Look(ref alphaAnimalSpawnMultiplier, "alphaAnimalSpawnMultiplier", 1, true);






        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.Gap(10f);
           
           
            ls.CheckboxLabeled("allowBlackHive".Translate(), ref flagBlackHiveRaids, null);
           
            ls.CheckboxLabeled("allowStalkingLions".Translate(), ref flagStalkingLions, null);
            
            ls.CheckboxLabeled("allowCactipineDroppods".Translate(), ref flagCactipineDroppods, null);
            
            ls.CheckboxLabeled("allowSpiderClutchMothers".Translate(), ref flagSpiderClutchMothers, null);
          
            ls.CheckboxLabeled("allowAerofleet".Translate(), ref flagAerofleets, null);
           
            ls.CheckboxLabeled("allowGallatross".Translate(), ref flagGallatross, null);

            ls.CheckboxLabeled("allowBehemoth".Translate(), ref flagBehemoth, null);

            ls.CheckboxLabeled("allowSummitCrab".Translate(), ref flagSummitCrab, null);
          
            ls.CheckboxLabeled("allowBumbledrones".Translate(), ref flagBumbledrones, null);       
          
            ls.CheckboxLabeled("allowAsteroids".Translate(), ref flagAsteroids, null);
            
            ls.CheckboxLabeled("allowAnimalisk".Translate(), ref flagAnimalisk, null);
        
            ls.CheckboxLabeled("allowMime".Translate(), ref flagMime, null);

            ls.CheckboxLabeled("allowFungalHusk".Translate(), ref flagFungalHusk, null);



            ls.Label("AA_AlphaAnimalSpawnMultiplier".Translate() + ": " + alphaAnimalSpawnMultiplier, -1, "AA_AlphaAnimalSpawnMultiplierTooltip".Translate());
            alphaAnimalSpawnMultiplier = (float)Math.Round(ls.Slider(alphaAnimalSpawnMultiplier, 0.1f, 5f), 2);

            if (ls.Settings_Button("AA_Reset".Translate(), new Rect(0f, ls.CurHeight, 180f, 29f)))
            {
                alphaAnimalSpawnMultiplier = alphaAnimalSpawnMultiplierBase;
            }


            ls.End();
        }



    }










}
