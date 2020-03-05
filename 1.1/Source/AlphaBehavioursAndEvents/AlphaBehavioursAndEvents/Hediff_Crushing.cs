using RimWorld;

using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace AlphaBehavioursAndEvents
{
    public class Hediff_Crushing : HediffWithComps
    {

        private System.Random random = new System.Random();

        private int tickCounter = 0;
       
        private int filthCounter = 0;
        private Sustainer sustainer;
        private int spawnTick = 0;
        private int leftFadeOutTicks = -1;

        private static List<Thing> tmpThings = new List<Thing>();

        private float FadeInOutFactor
        {
            get
            {
                float a = Mathf.Clamp01((float)(Find.TickManager.TicksGame - this.spawnTick) / 120f);
                float b = (this.leftFadeOutTicks >= 0) ? Mathf.Min((float)this.leftFadeOutTicks / 120f, 1f) : 1f;
                return Mathf.Min(a, b);
            }
        }

        public override void Tick()
        {
            base.Tick();
            if (pawn.Spawned)
            {
                if (this.sustainer == null)
                {
                    //Log.Error("Tornado sustainer is null.", false);
                    this.CreateSustainer();
                }
                this.sustainer.Maintain();
               
                tickCounter++;
                if (tickCounter > 30)
                {
                    if (!pawn.Downed) {
                        this.DamageCloseThings();
                        tickCounter = 0;
                    }
                    
                }
               
                filthCounter++;
                if (filthCounter > 130)
                {
                    if (!pawn.Downed)
                    {
                        this.RandomFilthGenerator();
                        filthCounter = 0;
                    }
                        
                }
            }

        }

       

        private void CreateSustainer()
        {
            LongEventHandler.ExecuteWhenFinished(delegate
            {
                SoundDef tornado = SoundDef.Named("AA_Rumbling");
                this.sustainer = tornado.TrySpawnSustainer(SoundInfo.InMap(this.pawn, MaintenanceType.PerTick));
               
            });
        }

        private void RandomFilthGenerator()
        {
            int num =   GenRadial.NumCellsInRadius(3f);
            for (int i = 0; i < num; i++)
            {
                if (random.NextDouble() > 0.8) {
                    IntVec3 intVec = this.pawn.Position + GenRadial.RadialPattern[i];
                    if (pawn.Map != null)
                    {
                        if (intVec.InBounds(pawn.Map))
                        {

                            Thing thing = ThingMaker.MakeThing(ThingDef.Named("Filth_RubbleRock"), null);

                            GenSpawn.Spawn(thing, intVec, pawn.Map);
                        }
                    }
                }
                
            }

        }

        private void DamageCloseThings()
        {
            int num = GenRadial.NumCellsInRadius(3f);
            for (int i = 0; i < num; i++)
            {
                IntVec3 intVec = this.pawn.Position + GenRadial.RadialPattern[i];
                if (pawn.Map != null) { 
                    if (intVec.InBounds(pawn.Map) && !this.CellImmuneToDamage(intVec))
                    {
                        Pawn firstPawn = intVec.GetFirstPawn(pawn.Map);
                        if (firstPawn == null || !firstPawn.Downed || !Rand.Bool)
                        {
                            float damageFactor = GenMath.LerpDouble(0f, 4.2f, 1f, 0.2f, intVec.DistanceTo(pawn.Position));
                            this.DoDamage(intVec, damageFactor);
                        }
                    }
                }
            }
        }

        private bool CellImmuneToDamage(IntVec3 c)
        {
            if (c.Roofed(pawn.Map) && c.GetRoof(pawn.Map).isThickRoof)
            {
                return true;
            }
            Building edifice = c.GetEdifice(pawn.Map);
            return edifice != null && edifice.def.category == ThingCategory.Building && (edifice.def.building.isNaturalRock || (edifice.def == ThingDefOf.Wall && edifice.Faction == null));
        }

        private void DoDamage(IntVec3 c, float damageFactor)
        {
            Hediff_Crushing.tmpThings.Clear();
            Hediff_Crushing.tmpThings.AddRange(c.GetThingList(pawn.Map));
            Vector3 vector = c.ToVector3Shifted();
            Vector2 b = new Vector2(vector.x, vector.z);
            //float num = -this.realPosition.AngleTo(b) + 180f;
            for (int i = 0; i < Hediff_Crushing.tmpThings.Count; i++)
            {
               if (tmpThings[i] != this.pawn) { 
                        BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = null;
                    switch (Hediff_Crushing.tmpThings[i].def.category)
                    {
                        case ThingCategory.Pawn:
                            {
                                Pawn pawn = (Pawn)Hediff_Crushing.tmpThings[i];
                                battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(pawn, RulePackDef.Named("AA_DamageEvent_Crushing"), null);
                                Find.BattleLog.Add(battleLogEntry_DamageTaken);
                                if (pawn.RaceProps.baseHealthScale < 1f)
                                {
                                    damageFactor *= pawn.RaceProps.baseHealthScale;
                                }
                                if (pawn.RaceProps.Animal)
                                {
                                    damageFactor *= 0.75f;
                                }
                                if (pawn.Downed)
                                {
                                    damageFactor *= 0.1f;
                                }
                                break;
                            }
                        case ThingCategory.Item:
                            damageFactor *= 0.03f;
                            break;
                        case ThingCategory.Building:
                            damageFactor *= 0.8f;
                            break;
                        case ThingCategory.Plant:
                            damageFactor *= 1.7f;
                            break;
                    }
                    int num2 = Mathf.Max(GenMath.RoundRandom(30f * damageFactor), 1);
                    Thing thingtoHurt = Hediff_Crushing.tmpThings[i];
                    DamageDef boulderScratch = DefDatabase<DamageDef>.GetNamed("AA_BoulderScratch", true);
                    float amount = (float)num2;
                    //float angle = num;
                    
                        thingtoHurt.TakeDamage(new DamageInfo(boulderScratch, amount, 0f, 0, pawn, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null)).AssociateWithLog(battleLogEntry_DamageTaken);
                   
                    
                }
            }
            Hediff_Crushing.tmpThings.Clear();
        }

    }
}
