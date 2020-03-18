using System;
using System.Collections.Generic;
using Verse.Sound;
using Verse;
using RimWorld;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
    public class CompHediffEffecter : ThingComp
    {
        public int tickCounter = 0;
        public List<Pawn> pawnList = new List<Pawn>();
        public Pawn thisPawn;


        public CompProperties_HediffEffecter Props
        {
            get
            {
                return (CompProperties_HediffEffecter)this.props;
            }
        }




        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;
            if (tickCounter > Props.tickInterval)
            {
                thisPawn = this.parent as Pawn;
                if (thisPawn != null && thisPawn.Map != null && !thisPawn.Dead && !thisPawn.Downed)
                {
                    List<Pawn> allPawnsSpawned = thisPawn.Map.mapPawns.AllPawnsSpawned;

                    for (int k = 0; k < allPawnsSpawned.Count; k++)
                    {
                        if (allPawnsSpawned[k] != null && allPawnsSpawned[k].IsColonist)
                        {
                            pawnList.Add(allPawnsSpawned[k]);
                        }
                    }

                    if (pawnList.Count > 0)
                    {
                        IntVec3 thisPawnLocation = thisPawn.Position;
                        List<Pawn> tempList = new List<Pawn>();
                        for (int k = 0; k < pawnList.Count; k++)
                        {
                            if (IntVec3Utility.ManhattanDistanceFlat(thisPawnLocation, pawnList[k].Position) < Props.radius)
                            {
                                tempList.Add(pawnList[k]);
                            }
                        }

                        if (tempList.Count > 0)
                        {
                            Pawn chosenOne = tempList.RandomElement();
                            if (chosenOne != null)
                            {

                                if (!chosenOne.Dead && !chosenOne.Downed)
                                {
                                    //SoundDefOf.PsychicPulseGlobal.PlayOneShot(new TargetInfo(this.parent.Position, this.parent.Map, false));
                                    MoteMaker.MakeAttachedOverlay(this.parent, ThingDef.Named("Mote_PsycastPsychicEffect"), Vector3.zero, 1f, -1f);

                                    chosenOne.health.AddHediff(HediffDef.Named(Props.hediff));
                                }

                            }
                        }

                        tempList.Clear();


                    }


                }
                pawnList.Clear();
                tickCounter = 0;
            }
        }


    }
}

