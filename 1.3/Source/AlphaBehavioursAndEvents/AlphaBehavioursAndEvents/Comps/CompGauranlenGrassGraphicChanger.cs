using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace AlphaBehavioursAndEvents
{

    //A simple comp class that changes a building's graphic by using reflection

    public class CompGauranlenGrassGraphicChanger : ThingComp
    {
        public Thing thingToGrab;
        public Graphic_Random newGraphic;
      
        public string newGraphicPath = "";
        public string newGraphicSinglePath = "";
        public bool reloading = false;

        public CompProperties_GauranlenGrassGraphicChanger Props
        {
            get
            {
                return (CompProperties_GauranlenGrassGraphicChanger)this.props;
            }
        }

        

       

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            thingToGrab = (Thing)this.parent;
            reloading = true;
            //Using LongEventHandler to avoid having to create a GraphicCache
            if (Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo.HasMeme(DefDatabase<MemeDef>.GetNamed("AA_BiologicalCorruptors")))
            {
                LongEventHandler.ExecuteWhenFinished(ChangeGraphic);

            }


        }

        public void ChangeGraphic()
        {
            Vector2 sizeVector = this.parent.Graphic.drawSize;
            Color objectColour = this.parent.Graphic.color;
            ShaderTypeDef shaderUsed = this.parent.def.graphicData.shaderType;

            if (!reloading)
            {
                int newGraphicPathIndex = Props.newGraphics.IndexOf(newGraphicPath);
                if (newGraphicPathIndex + 1 > Props.newGraphics.Count - 1)
                {
                    newGraphicPathIndex = 0;
                }
                else newGraphicPathIndex++;
                newGraphicPath = Props.newGraphics[newGraphicPathIndex];
                newGraphic = (Graphic_Random)GraphicDatabase.Get<Graphic_Random>(newGraphicPath, shaderUsed.Shader, sizeVector, objectColour);
            }
            else
            {
                if (newGraphicPath == "")
                {
                    newGraphicPath = Props.newGraphics[0];
                    newGraphic = (Graphic_Random)GraphicDatabase.Get<Graphic_Random>(newGraphicPath, shaderUsed.Shader, sizeVector, objectColour);
                }
                else
                {
                    newGraphic = (Graphic_Random)GraphicDatabase.Get<Graphic_Random>(newGraphicPath, shaderUsed.Shader, sizeVector, objectColour);
                }
                reloading = false;

            }
            Type typ = typeof(Thing);
            FieldInfo type = typ.GetField("graphicInt", BindingFlags.Instance | BindingFlags.NonPublic);
            type.SetValue(thingToGrab, newGraphic);




        }

        public override void PostExposeData()
        {
            Scribe_Values.Look<string>(ref this.newGraphicPath, "newGraphicPath");
         
            Scribe_Values.Look<bool>(ref this.reloading, "reloading", false);

        }

        
    }
}
