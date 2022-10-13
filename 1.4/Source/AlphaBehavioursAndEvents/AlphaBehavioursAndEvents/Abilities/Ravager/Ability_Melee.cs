using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;
using System.Linq;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_Melee : Ability
    {

        public override bool IsEnabledForPawn(out string reason)
        {
            if (pawn.equipment.Primary == null)
            {
                reason = "AA_MeleeWeaponNeeded".Translate();
                return false;
            }
            else if (!pawn.equipment.Primary.def.IsMeleeWeapon)
            {
                reason = "AA_MeleeWeaponNeeded".Translate();
                return false;
            }


            return base.IsEnabledForPawn(out reason);

        }

        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);

            Ability_Damage_Extension extension = this.def.GetModExtension<Ability_Damage_Extension>();

            foreach (GlobalTargetInfo target in targets)
            {
                Pawn targetpawn = target.Thing as Pawn;
                if (targetpawn == null || extension == null)
                    return;

                List<IntVec3> positionsAround = new List<IntVec3>();
                positionsAround.Add(targetpawn.Position + IntVec3.North);
                positionsAround.Add(targetpawn.Position + IntVec3.South);
                positionsAround.Add(targetpawn.Position + IntVec3.East);
                positionsAround.Add(targetpawn.Position + IntVec3.West);

                foreach (IntVec3 pos in positionsAround)
                {
                    if (!pos.Impassable(targetpawn.Map))
                    {
                        List<BodyPartRecord> bps = null;
                        if (extension.teleport) { pawn.Position = pos; }

                        if (extension.jump)
                        {
                            var map = Caster.Map;
                            var flyer = (JumpingPawn)PawnFlyer.MakeFlyer(ThingDef.Named("AAVPE_JumpingPawn"), CasterPawn, targets[0].Cell,null,null);
                            flyer.ability = this;
                            flyer.target = targets[0].Cell.ToVector3Shifted();
                            GenSpawn.Spawn(flyer, Caster.Position, map);                         

                        }


                        if (extension.bodyPart != null)
                        {
                            bps = (from c in targetpawn?.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null)
                                   where c.def == extension.bodyPart && !c.def.conceptual && c.coverageAbs>0
                                   select c).ToList();
                        }
                        else
                        {
                            bps = (from c in targetpawn?.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null)
                                   where !c.def.conceptual && c.coverageAbs > 0
                                   select c).ToList();
                        }

                        if (bps != null && bps.Count > 0)
                        {
                            for (int i = 0; i < extension.repeatAmount; i++)
                            {
                                if (extension.cleaveAttack)
                                {

                                    IntVec3 targetPawnPosition = targetpawn.Position;
                                    
                                    targetpawn.TakeDamage(new DamageInfo(extension.damageDef, extension.damage, extension.armourPen, -1, pawn, bps.RandomElement(), pawn.equipment.Primary.def));
                                    


                                    foreach (IntVec3 positionAround in GenRadial.RadialCellsAround(targetPawnPosition, 1.9f, false).ToList())
                                    {
                                        
                                        List<Pawn> pawnList = pawn.Map.mapPawns.AllPawnsSpawned;

                                        foreach (Pawn possiblePawnAffected in pawnList)
                                        {
                                          
                                            if (positionAround == possiblePawnAffected.Position)
                                            {
                                                if (extension.instakill)
                                                {
                                                    possiblePawnAffected.Kill(null);
                                                }
                                                else
                                                {
                                                    possiblePawnAffected.TakeDamage(new DamageInfo(extension.damageDef, extension.damage, extension.armourPen, -1, pawn, bps.RandomElement(), pawn.equipment.Primary.def));
                                                }
                                            }
                                        }
                                    }
                                    if (extension.instakill && !targetpawn.Dead)
                                    {
                                        targetpawn.Kill(null);
                                    }

                                }
                                else {
                                    if (extension.instakill)
                                    {
                                        targetpawn.Kill(null);
                                    }
                                    else
                                    {
                                        targetpawn.TakeDamage(new DamageInfo(extension.damageDef, extension.damage, extension.armourPen, -1, targetpawn, bps.RandomElement(), pawn.equipment.Primary.def));
                                    }
                                }


                            }

                            MoteMaker.ThrowText(targetpawn.PositionHeld.ToVector3(), targetpawn.MapHeld, extension.mote.Translate(), 3f);
                        }
                        else
                        {
                            MoteMaker.ThrowText(pawn.PositionHeld.ToVector3(), pawn.MapHeld, extension.moteFailed.Translate(), 3f);
                            return;
                        }

                        return;
                    }
                }

                MoteMaker.ThrowText(pawn.PositionHeld.ToVector3(), pawn.MapHeld, extension.moteFailed.Translate(), 3f);



            }
        }



    }
}
