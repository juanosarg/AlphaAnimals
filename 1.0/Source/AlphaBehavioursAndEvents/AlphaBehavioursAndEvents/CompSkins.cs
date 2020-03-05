using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class CompSkins : ThingComp
    {

        private System.Random rand = new System.Random();
        private int storeGraphic = 0;
        private bool hasAsigned = false;



        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref this.hasAsigned, "hasAsigned", true, false);
            Scribe_Values.Look<int>(ref this.storeGraphic, "storeGraphic", 0, false);
        }




        public CompProperties_Skins Props
        {
            get
            {
                return (CompProperties_Skins)this.props;
            }
        }

        protected int numberOfSkins
        {
            get
            {
                return this.Props.numberOfSkins;
            }
        }

        protected string skinBaseString
        {
            get
            {
                return this.Props.skinBaseString;
            }
        }

      

        public override void CompTick()
        {
            base.CompTick();
            if (!hasAsigned)
            {
                int randomNumber = rand.Next(1, numberOfSkins + 1);
                string skinString = skinBaseString + "_" + randomNumber.ToString();
                Pawn pawn = this.parent as Pawn;
                Graphic newGraphic = GraphicDatabase.Get<Graphic_Multi>(skinString, ShaderDatabase.Cutout, pawn.Drawer.renderer.graphics.nakedGraphic.drawSize, Color.white);
                pawn.Drawer.renderer.graphics.nakedGraphic = newGraphic;
                storeGraphic = randomNumber;
                hasAsigned = true;
            }
            else
            {
                string skinString = skinBaseString + "_" + storeGraphic.ToString();
                Pawn pawn = this.parent as Pawn;
                Graphic newGraphic2 = GraphicDatabase.Get<Graphic_Multi>(skinString, ShaderDatabase.Cutout, pawn.Drawer.renderer.graphics.nakedGraphic.drawSize, Color.white);
                pawn.Drawer.renderer.graphics.nakedGraphic = newGraphic2;

            }
        }





    }
}
