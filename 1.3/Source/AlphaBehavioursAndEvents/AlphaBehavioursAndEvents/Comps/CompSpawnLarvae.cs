using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.Sound;
using UnityEngine;
using Verse.AI.Group;

namespace AlphaBehavioursAndEvents
{
    class CompSpawnLarvae : ThingComp
    {

        int tickCounter;
        int TotalNumber = 0;

        private static readonly List<PawnKindDef> pawnsList = new List<PawnKindDef>() { InternalDefOf.AA_BlackScarab,
        InternalDefOf.AA_BlackSpelopede,InternalDefOf.AA_BlackSpider,InternalDefOf.AA_MammothWorm,InternalDefOf.AA_MegaLouse, PawnKindDef.Named("AA_BlackLarvae")};


        private CompProperties_SpawnLarvae Props
        {
            get
            {
                return (CompProperties_SpawnLarvae)this.props;
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);

            this.tickCounter = 0;
        }

        public override string CompInspectStringExtra()
        {
            if (TotalNumber <= Props.maxNumber)
            {
                return "AA_LarvaeTimeSpawn".Translate((Props.ticksBetweenSpawn - tickCounter).ToStringTicksToPeriod());
            }
            return "AA_NoMoreLarvaeAllowed".Translate();
        }

        public override void CompTick()
        {
            base.CompTick();

            tickCounter++;
            if (parent.IsHashIntervalTick(Props.ticksBetweenSpawn))
            {
                TotalNumber = 0;

                foreach (PawnKindDef pawnKindDef in pawnsList)
                {
                    List<Thing> listOfPawnsOfThisDef = this.parent.Map.listerThings.ThingsOfDef(pawnKindDef.race);
                    foreach (Thing pawn in listOfPawnsOfThisDef)
                    {
                        if (pawn.Faction == Faction.OfPlayer)
                        {
                            TotalNumber++;

                        }
                    }

                }
               
                if (TotalNumber <= Props.maxNumber)
                {
                    IntVec3 vec3 = this.parent.Position.RandomAdjacentCell8Way();
                    if (vec3.InBounds(this.parent.Map) && vec3.Walkable(this.parent.Map))
                    {
                        Pawn p = PawnGenerator.GeneratePawn(PawnKindDef.Named("AA_BlackLarvae"), Faction.OfPlayer);
                        GenSpawn.Spawn(p, vec3, this.parent.Map);


                        FilthMaker.TryMakeFilth(this.parent.Position, this.parent.Map, ThingDefOf.Filth_Slime, 1);
                        SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(this.parent));

                    }

                }





                tickCounter = 0;
            }


        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterLarvae");
            Scribe_Values.Look<int>(ref this.TotalNumber, "TotalNumber");
        }
    }
}
