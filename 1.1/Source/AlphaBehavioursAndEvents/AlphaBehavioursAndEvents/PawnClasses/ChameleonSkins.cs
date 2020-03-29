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

       
        public Graphic dessicatedGraphic;

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
            this.ChangeTheGraphics();
            base.Tick();
        }

        public void ChangeTheGraphics()
        {
            if (this.Map != null) {
                Vector2 vector = this.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;
                //Graphic dessicatedGraphic = this.ageTracker.CurKindLifeStage.dessicatedBodyGraphicData.Graphic;
                if ((this.Position.GetTerrain(this.Map) == TerrainDef.Named("Ice")) || (this.Position.GetSnowDepth(this.Map) > 0) || (this.Map.mapTemperature.OutdoorTemp<-10f))
                {
                    LongEventHandler.ExecuteWhenFinished(delegate
                    {
                       
                        Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Winter", ShaderDatabase.Cutout, vector, Color.white);
                        this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                        this.pawn_renderer.graphics.ResolveAllGraphics();
                        this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;


                        (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;                        
                        StatExtension.SetStatBaseValue(this.def,StatDefOf.ComfyTemperatureMin,-60f);
                        StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 10f);
                    });
                    woolType = 1;
                }
                else if ((this.Position.GetTerrain(this.Map) == TerrainDef.Named("MossyTerrain")) || (this.Position.GetTerrain(this.Map) == TerrainDef.Named("MarshyTerrain")) || (this.Position.GetTerrain(this.Map) == TerrainDef.Named("SoilRich"))
                    || (this.Position.GetTerrain(this.Map).IsWater))
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
                    });
                    woolType = 2;

                }
                else if ((this.Position.GetTerrain(this.Map) == TerrainDef.Named("Sand")) || (this.Position.GetTerrain(this.Map) == TerrainDef.Named("SoftSand")))
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
                    });
                    woolType = 3;

                }
                else {
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
                    });
                    woolType = 0;


                }

            } else {
                StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMin, -60f);
                StatExtension.SetStatBaseValue(this.def, StatDefOf.ComfyTemperatureMax, 60f);
            }
            

           

        }

        




    }
}
