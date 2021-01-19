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
    public class CompGraphicByTerrain : ThingComp
    {

        private PawnRenderer pawn_renderer;
        public Graphic dessicatedGraphic;
        public int changeGraphicsCounter = 0;
        public string terrainName = "";

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<string>(ref this.terrainName, "terrainName", "", false);
        }

        public CompProperties_GraphicByTerrain Props
        {
            get
            {
                return (CompProperties_GraphicByTerrain)this.props;
            }
        }

        public override void CompTick()
        {
            changeGraphicsCounter++;
            if (changeGraphicsCounter > Props.changeGraphicsInterval)
            {
                this.ChangeTheGraphics();
                changeGraphicsCounter = 0;
            }
            base.CompTick();
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            Pawn pawn = this.parent as Pawn;
            Pawn_DrawTracker drawtracker = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pawn));
            if (drawtracker != null)
            {
                this.pawn_renderer = drawtracker.renderer;
            }
            GraphicData dessicatedgraphicdata = new GraphicData();
            dessicatedgraphicdata.texPath = Props.dessicatedTxt;
            dessicatedGraphic = dessicatedgraphicdata.Graphic;
            this.ChangeTheGraphics();

        }


        public void ChangeTheGraphics()
        {
            string currentName = "";
            if (this.parent.Map != null)
            {
                Pawn pawn = this.parent as Pawn;
                if (this.pawn_renderer == null)
                {
                    Pawn_DrawTracker drawtracker = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pawn));
                    if (drawtracker != null)
                    {
                        this.pawn_renderer = drawtracker.renderer;
                    }

                }

                Vector2 vector = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;

                if (Props.terrains.Contains(pawn.Position.GetTerrain(pawn.Map).defName))
                {
                    currentName = pawn.Position.GetTerrain(pawn.Map).defName;
                    if (this.terrainName != currentName)
                    {
                        LongEventHandler.ExecuteWhenFinished(delegate
                        {
                            if (this.pawn_renderer != null)
                            {
                                try
                                {
                                    Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(pawn.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + Props.suffix, ShaderDatabase.Cutout, vector, Color.white);
                                    this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                                    this.pawn_renderer.graphics.ResolveAllGraphics();
                                    this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                                    (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                                    this.terrainName = pawn.Position.GetTerrain(pawn.Map).defName;
                                    pawn.health.AddHediff(DefDatabase<HediffDef>.GetNamed(Props.hediffToApply));
                                }
                                catch (NullReferenceException) { }
                            }

                        });

                    }
                }
                else
                {
                    currentName = "Normal";
                    if (this.terrainName != currentName)
                    {
                        LongEventHandler.ExecuteWhenFinished(delegate
                        {
                            if (this.pawn_renderer != null)
                            {
                                try
                                {
                                    Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(pawn.ageTracker.CurKindLifeStage.bodyGraphicData.texPath, ShaderDatabase.Cutout, vector, Color.white);
                                    this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                                    this.pawn_renderer.graphics.dessicatedGraphic = dessicatedGraphic;
                                    this.pawn_renderer.graphics.ResolveAllGraphics();
                                    (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                                    this.terrainName = "Normal";
                                    Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed(Props.hediffToApply));
                                    if (hediff != null)
                                    {
                                        pawn.health.RemoveHediff(hediff);
                                    }
                                }
                                catch (NullReferenceException) { }
                            }

                        });
                    }
                }
            }




        }


    }
}
