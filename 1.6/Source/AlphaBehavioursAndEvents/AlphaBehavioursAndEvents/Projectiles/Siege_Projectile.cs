﻿using System;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class Siege_Projectile : Projectile
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing);
            BattleLogEntry_RangedImpact battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(this.launcher, hitThing, this.intendedTarget.Thing, InternalDefOf.Gun_Autopistol, this.def, this.targetCoverDef);
            Find.BattleLog.Add(battleLogEntry_RangedImpact);
            if (hitThing != null)
            {
                DamageDef damageDef = this.def.projectile.damageDef;
                float amount = (float)base.DamageAmount;
                float armorPenetration = base.ArmorPenetration;
                float y = this.ExactRotation.eulerAngles.y;
                Thing launcher = this.launcher;
                ThingDef equipmentDef = this.equipmentDef;
                DamageInfo dinfo = new DamageInfo(damageDef, amount, armorPenetration, y, launcher, null, null, DamageInfo.SourceCategory.ThingOrUnknown, this.intendedTarget.Thing);
                hitThing.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
                Pawn pawn = hitThing as Pawn;
                if (pawn != null && pawn.stances != null && pawn.BodySize <= this.def.projectile.stoppingPower + 0.001f)
                {
                    pawn.stances.stagger.StaggerFor(95);
                }
                Building wall = hitThing as Building;
                if (wall != null)
                {
                    if ((wall.def.building.isNaturalRock) || (wall.def == ThingDefOf.Wall))
                    {
                        DamageInfo dinfo2 = new DamageInfo(damageDef, amount * 5, armorPenetration * 3, y, launcher, null, null, DamageInfo.SourceCategory.ThingOrUnknown, this.intendedTarget.Thing);
                        hitThing.TakeDamage(dinfo2).AssociateWithLog(battleLogEntry_RangedImpact);
                    }
                }


            }
            else
            {
                SoundDefOf.BulletImpact_Ground.PlayOneShot(new TargetInfo(base.Position, map, false));
                FleckMaker.Static(this.ExactPosition, map, FleckDefOf.ShotHit_Dirt, 1f);
                if (base.Position.GetTerrain(map).takeSplashes)
                {
                    FleckMaker.WaterSplash(this.ExactPosition, map, Mathf.Sqrt((float)base.DamageAmount) * 1f, 4f);
                }
            }
        }
    }
}