using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Verse;
using Verse.AI.Group;
using Verse.Sound;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class Building_Overgrown_DropPod : Building
    {
      

        public int nextPawnSpawnTick = 0;
        public int ticksInday = 60000;




        public override void Tick()
        {
            base.Tick();
            if (base.Spawned)
            {
                nextPawnSpawnTick++;
                if (nextPawnSpawnTick > 7500)
                {
                    Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDefOf.Insect);
                    Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("AA_Cactipine"), faction);
                   
                    GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 1, null), base.Map, WipeMode.Vanish);
                    nextPawnSpawnTick = 0;
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, false, false, null, false);

                    SoundDefOf.Hive_Spawn.PlayOneShot(this);

                }
            }
        }


        public override string GetInspectString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            string text = base.InspectStringPartsFromComps();
            if (!text.NullOrEmpty())
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.AppendLine();
                }
                stringBuilder.Append(text);
            }
            float totalProgress = ((float)nextPawnSpawnTick / (float)(7500));
             
            stringBuilder.Append("Generating terraforming lifeform: "+ totalProgress.ToStringPercent());

            return stringBuilder.ToString();


           

        }









    }
}
