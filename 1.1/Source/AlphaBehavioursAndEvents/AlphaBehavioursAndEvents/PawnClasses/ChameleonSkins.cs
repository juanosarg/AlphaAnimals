using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using Verse;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class ChameleonSkins : Pawn
    {
        private PawnRenderer pawn_renderer;

        public string main_graphic;

        public int woolType = 0;

        public int changeGraphicsCounter = 0;
        public int changeGraphicsCounterMax = 240;

        public string terrainName = "";

        public Graphic dessicatedGraphic;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<string>(ref this.terrainName, "terrainName", "", false);

        }


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            //this.main_graphic = this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath;
            this.pawn_renderer = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this)).renderer;
            GraphicData dessicatedgraphicdata = new GraphicData();
            dessicatedgraphicdata.texPath = "Things/Pawn/Animal/AA_ChameleonYak/AA_Dessicated_ChameleonYak";
            dessicatedGraphic = dessicatedgraphicdata.Graphic;
            this.ChangeTheGraphics();


        }

    

        public override void Tick()
        {
            changeGraphicsCounter++;
            if (changeGraphicsCounter > changeGraphicsCounterMax)
            {
                this.ChangeTheGraphics();
                changeGraphicsCounter = 0;
            }
            
            base.Tick();
        }

        public void ChangeTheGraphics()
        {
            string currentName = "";
            if (this.Map != null) {
                Vector2 vector = this.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;
                //Graphic dessicatedGraphic = this.ageTracker.CurKindLifeStage.dessicatedBodyGraphicData.Graphic;
                if ((this.Position.GetTerrain(this.Map) == TerrainDef.Named("Ice")) || (this.Position.GetSnowDepth(this.Map) > 0) || (this.Map.mapTemperature.OutdoorTemp<-10f))
                {
                    currentName = "Ice";
                    if (this.terrainName != currentName) {
                        LongEventHandler.ExecuteWhenFinished(delegate
                        {

                            Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Winter", ShaderDatabase.Cutout, vector, Color.white);
                            this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                            this.pawn_renderer.graphics.ResolveAllGraphics();
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;


                            (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMin, -60f);
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 10f);
                            this.terrainName = "Ice";
                        });
                        woolType = 1;
                    }
                    
                }
                else if ((this.Position.GetTerrain(this.Map) == TerrainDef.Named("MossyTerrain")) || (this.Position.GetTerrain(this.Map) == TerrainDef.Named("MarshyTerrain")) || (this.Position.GetTerrain(this.Map) == TerrainDef.Named("SoilRich"))
                    || (this.Position.GetTerrain(this.Map).IsWater))
                {
                    currentName = "Water";
                    if (this.terrainName != currentName)
                    {
                        LongEventHandler.ExecuteWhenFinished(delegate
                        {
                            Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Jungle", ShaderDatabase.Cutout, vector, Color.white);
                            this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                            this.pawn_renderer.graphics.ResolveAllGraphics();
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;

                            (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMin, -10f);
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 35f);
                            this.terrainName = "Water";
                        });
                        woolType = 2;
                    }

                }
                else if ((this.Position.GetTerrain(this.Map) == TerrainDef.Named("Sand")) || (this.Position.GetTerrain(this.Map) == TerrainDef.Named("SoftSand")))
                {
                    currentName = "Desert";
                    if (this.terrainName != currentName)
                    {
                        LongEventHandler.ExecuteWhenFinished(delegate
                        {
                            Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Desert", ShaderDatabase.Cutout, vector, Color.white);
                            this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                            this.pawn_renderer.graphics.ResolveAllGraphics();
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;

                            (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMin, 0f);
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 65f);
                            this.terrainName = "Desert";
                        });
                        woolType = 3;
                    }

                }
                else {

                    currentName = "Normal";
                    if (this.terrainName != currentName)
                    {
                        LongEventHandler.ExecuteWhenFinished(delegate
                        {
                            Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath, ShaderDatabase.Cutout, vector, Color.white);
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                            this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                            this.pawn_renderer.graphics.ResolveAllGraphics();

                            //Log.Message(this.pawn_renderer.graphics.dessicatedGraphic.ToString());
                            (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMin, -10f);
                            StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 35f);
                            this.terrainName = "Normal";
                        });
                        woolType = 0;
                    }


                }

            } else {
                StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMin, -60f);
                StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 60f);
            }
            

           

        }

        




    }
}
