using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse.Sound;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompCorpseDecayer : ThingComp
    {

       
        public int asexualFissionCounter = 0;
        public int tickCounter = 0;
        public bool flagOnce = false;



        public CompProperties_CorpseDecayer Props
        {
            get
            {
                return (CompProperties_CorpseDecayer)this.props;
            }
        }

     

        public override void CompTick()
        {
            base.CompTick();
            
            if (AlphaAnimalsEvents_Settings.flagHelixienCorpseEffect) {
                tickCounter++;
                if (tickCounter > Props.tickInterval)
                {
                    Pawn pawn = this.parent as Pawn;
                    if (pawn.Map != null)
                    {
                        CellRect rect = GenAdj.OccupiedRect(pawn.Position, pawn.Rotation, IntVec2.One);
                        rect = rect.ExpandedBy(Props.radius);

                        foreach (IntVec3 current in rect.Cells)
                        {
                            if (current.InBounds(pawn.Map))
                            {
                                HashSet<Thing> hashSet = new HashSet<Thing>(current.GetThingList(pawn.Map));
                                if (hashSet != null)
                                {
                                    foreach (Thing thingInCell in hashSet)
                                    {
                                        Corpse corpse = thingInCell as Corpse;
                                        if (corpse != null)
                                        {
                                            if (corpse.InnerPawn.def.race.IsFlesh)
                                            {
                                                corpse.HitPoints -= 5;
                                                pawn.needs.food.CurLevel += Props.nutritionGained;

                                                CompRottable compRottable = corpse.TryGetComp<CompRottable>();
                                                if (compRottable.Stage == RotStage.Fresh)
                                                {
                                                    compRottable.RotProgress += 100000;
                                                }

                                                if (corpse.HitPoints < 0)
                                                {
                                                    corpse.Destroy(DestroyMode.Vanish);
                                                    for (int i = 0; i < 20; i++)
                                                    {
                                                        IntVec3 c;
                                                        CellFinder.TryFindRandomReachableCellNear(pawn.Position, pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                                                        FilthMaker.TryMakeFilth(c, pawn.Map, ThingDefOf.Filth_CorpseBile, pawn.LabelIndefinite(), 1, FilthSourceFlags.None);
                                                        SoundDef.Named(Props.corpseSound).PlayOneShot(new TargetInfo(pawn.Position, pawn.Map, false));

                                                    }
                                                }
                                                FilthMaker.TryMakeFilth(current, pawn.Map, ThingDefOf.Filth_CorpseBile, pawn.LabelIndefinite(), 1, FilthSourceFlags.None);
                                                flagOnce = true;

                                            }

                                        }

                                    }
                                }
                            }
                            if (flagOnce) { flagOnce = false; break; }

                        }


                    }



                    tickCounter = 0;
                }


            }
            

        }

       
    }
}
