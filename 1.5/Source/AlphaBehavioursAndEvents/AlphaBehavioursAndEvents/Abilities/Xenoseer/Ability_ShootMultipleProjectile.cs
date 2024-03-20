
using RimWorld.Planet;
using Verse;

using VFECore.Abilities;

namespace AlphaBehavioursAndEvents
{
    public class Ability_ShootMultipleProjectile : Ability_ShootProjectile
    {

        public bool firingNow = false;
        public int projectilesFired = 0;
        public int waitCounter = 0;
        public const int ticksBetweenProjectiles = 30;
        GlobalTargetInfo targetVariable;
        Ability_MultipleProjectile_Extension extension;

        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            
            foreach (GlobalTargetInfo target in targets)
            {
                ShootProjectile(target);
            }
        }
        protected override Projectile ShootProjectile(GlobalTargetInfo target)
        {
            extension = this.def.GetModExtension<Ability_MultipleProjectile_Extension>();
            Projectile projectile = null;
            targetVariable = target;
            projectile = GenSpawn.Spawn(extension.projectile, this.pawn.Position, this.pawn.Map) as Projectile;
            if (projectile is AbilityProjectile abilityProjectile)
            {
                abilityProjectile.ability = this;
            }
            if (target.HasThing)
                projectile?.Launch(this.pawn, this.pawn.DrawPos, target.Thing, target.Thing, ProjectileHitFlags.IntendedTarget);
            else
                projectile?.Launch(this.pawn, this.pawn.DrawPos, target.Cell, target.Cell, ProjectileHitFlags.IntendedTarget);

            firingNow = true;

            return projectile;
        }


        public override void Tick()
        {
            base.Tick();

            if (firingNow)
            {
                waitCounter++;
                if (waitCounter > ticksBetweenProjectiles)
                {

                    
                     
                    Projectile projectile = GenSpawn.Spawn(extension.projectile, this.pawn.Position, this.pawn.Map) as Projectile;
                    if (projectile is AbilityProjectile abilityProjectile)
                    {
                        abilityProjectile.ability = this;
                    }
                    if (targetVariable.HasThing)
                        projectile?.Launch(this.pawn, this.pawn.DrawPos, targetVariable.Thing, targetVariable.Thing, ProjectileHitFlags.IntendedTarget);
                    else
                        projectile?.Launch(this.pawn, this.pawn.DrawPos, targetVariable.Cell, targetVariable.Cell, ProjectileHitFlags.IntendedTarget);

                    projectilesFired++;
                    waitCounter = 0;
                }


                if (projectilesFired >= extension.numberOfProjectiles-1)
                {
                    projectilesFired = 0;
                    waitCounter = 0;
                    firingNow = false;
                }


            }


        }
    }
}