using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System;


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

       

        public void DoWindowContents(Rect inRect)
        {

            List<string> keys = pawnSpawnStates.Keys.ToList().OrderByDescending(x => x).ToList();
            Listing_Standard ls = new Listing_Standard();
            Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            Rect rect2 = new Rect(0f, 0f, inRect.width - 30f, ((keys.Count/2)+2) * 24);
            Widgets.BeginScrollView(rect, ref scrollPosition, rect2, true);
            ls.ColumnWidth = rect2.width / 2.2f;
            ls.Begin(rect2);
            ls.CheckboxLabeled("allowVanillaAnimals".Translate(), ref flagVanillaAnimals, null);
            for (int num = keys.Count - 1; num >= 0; num--)
            {
                if (num == keys.Count/2) { ls.NewColumn(); }
                bool test = pawnSpawnStates[keys[num]];
                if (DefDatabase<PawnKindDef>.GetNamedSilentFail(keys[num]) == null)
                {
                    pawnSpawnStates.Remove(keys[num]);
                }
                else
                {
                    ls.CheckboxLabeled("AA_DisableAnimal".Translate(PawnKindDef.Named(keys[num]).LabelCap), ref test);
                    pawnSpawnStates[keys[num]] = test;
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
