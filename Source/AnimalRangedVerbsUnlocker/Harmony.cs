using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Harmony;
using System.Reflection;

namespace AnimalRangeAttack
{

	[StaticConstructorOnStartup]
	internal static class AnimalRangeAttack_Init
	{
		static AnimalRangeAttack_Init()
		{
			HarmonyInstance harmony = HarmonyInstance.Create("com.github.rimworld.mod.AnimalRangeAttack");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}



	//Current effective verb influence target pick.
	[HarmonyPatch(typeof(Pawn), "TryGetAttackVerb")]
	public static class ARA__VerbCheck_Patch
	{
		static bool Prefix(ref Pawn __instance,ref Verb __result)
		{
			//If animal don't bother
			if (!__instance.AnimalOrWildMan())
				return true;
			
			List<Verb> verbList = __instance.verbTracker.AllVerbs;
			for (int i = 0; i < verbList.Count; i++)
			{
				if (verbList[i].verbProps.range > 1.1f)
				{
					//found range verb return first one in the list
					__result = verbList[i];
					return false;
				}
			}
			return true;

		}
	}
	
	
	[HarmonyPatch(typeof(JobGiver_Manhunter), "TryGiveJob")]
	public static class ARA__ManHunter_Patch
	{
		
		static bool Prefix(ref JobGiver_Manhunter __instance, ref Job __result, ref Pawn pawn)
		{
			//Log.Warning("Detected Animal Attack");

			bool hasRangedVerb = false;
			
			

			//Log.Warning("Trying to fire at pawn");

			List<Verb> verbList = pawn.verbTracker.AllVerbs;

			//Log.Warning("Got list of verb");

			List<Verb> rangeList = new List<Verb>();
			for (int i = 0; i < verbList.Count; i++)
			{
				//Log.Warning("Checkity");
				//It corresponds with verbs anyway
				if (verbList[i].verbProps.range > 1.1f)
				{
					rangeList.Add(verbList[i]);
					hasRangedVerb = true;
				}
				//Log.Warning("Added Ranged Verb");
			}
			//Log.Warning("got list of ranged verb");



			//If there is no ranged verb just return;
			if (hasRangedVerb == false)
				return true;

			Verb rangeVerb = rangeList.RandomElementByWeight((Verb rangeItem) => rangeItem.verbProps.commonality);
			if (rangeVerb == null)
			{
				Log.Warning("Can't get random range verb");
				return true;
			}

			//Log.Warning("Range verb detected");
			Thing target = (Thing)ARA_AttackTargetFinder.BestAttackTarget((IAttackTargetSearcher)pawn, TargetScanFlags.NeedThreat | TargetScanFlags.NeedReachable, (Predicate<Thing>)(x =>
             x is Pawn || x is Building), 0.0f, 9999, new IntVec3(), float.MaxValue, false);
			
			//Seek thing hiding in embrasure.
			if(target == null)
				target = (Thing)ARA_AttackTargetFinder.BestAttackTarget((IAttackTargetSearcher)pawn, TargetScanFlags.NeedThreat , (Predicate<Thing>)(x =>
             x is Pawn || x is Building), 0.0f, 9999, new IntVec3(), float.MaxValue, false);
			//Use normal manhunter
			//Can't check for target if it doesn't exist duh

			//Log.Warning("CurrentEffectiveVerb " + pawn.CurrentEffectiveVerb);

			/*
			Log.Warning("Effective Range " + pawn.CurrentEffectiveVerb.verbProps.range);
			if (!pawn.CurrentEffectiveVerb.verbProps.MeleeRange)
			{
				Log.Warning("Not melee range");
			}
			*/
			
			bool targetInSight = false;
			Thing shootable = null;
			if (target == null)
			{
				shootable = (Thing)ARA_AttackTargetFinder.BestShootTargetFromCurrentPosition(pawn, (Predicate<Thing>)(x =>
             	x is Pawn || x is Building), rangeVerb.verbProps.range, rangeVerb.verbProps.minRange, TargetScanFlags.NeedThreat | TargetScanFlags.NeedLOSToPawns | TargetScanFlags.LOSBlockableByGas );
				  
				//Log.Warning("Shootable found, " + shootable);
				if (shootable == null)
				{
					//Log.Warning("No target in line of site");
					return true;
				}
				targetInSight = true;
			}
			else if (target.Position.DistanceTo(pawn.Position) < rangeVerb.verbProps.minRange || target.Position.AdjacentTo8Way(pawn.Position))
			{
				//Log.Warning("Target too close, melee!");
				//Core code can't handle animal anymore
				if (pawn.CanReach(target, PathEndMode.Touch, Danger.Deadly, false))
				{
					//Log.Warning("Melee Attack");
					__result = new Job(JobDefOf.AttackMelee,target)
					{
						maxNumMeleeAttacks = 1,
						expiryInterval = Rand.Range(420, 900),
						attackDoorIfTargetLost = false
					};
					return false;
				}
				else
				{
					return true;
				}
			}


			//Log.Warning("Got target");

			//Formally range attack job
			//if (target != null && pawn.CanReach(target, PathEndMode.Touch, Danger.Deadly, false))


			//Log.Warning("Ranged verb selected, range of  " + verb.verbProps.range);
			IntVec3 intVec;

			//Log.Warning("Trying to shoot");
			//Searches for target within range again.
			if (!targetInSight)
			{
				//Log.Warning("No target");
				shootable = (Thing)ARA_AttackTargetFinder.BestShootTargetFromCurrentPosition(pawn, (Predicate<Thing>)(x =>
             	x is Pawn || x is Building), rangeVerb.verbProps.range, rangeVerb.verbProps.minRange, TargetScanFlags.NeedThreat | TargetScanFlags.NeedLOSToPawns | TargetScanFlags.LOSBlockableByGas );
			}
			
			if (shootable != null)
			{
				//Log.Warning("Got Shootable");
				if (target.Position.DistanceTo(pawn.Position) < rangeVerb.verbProps.minRange || target.Position.AdjacentTo8Way(pawn.Position))
				{
					//Log.Warning("Target too close, melee!");
					//Core code can't handle animal anymore
					if (pawn.CanReach(target, PathEndMode.Touch, Danger.Deadly, false))
					{
						//Log.Warning("Melee Attack");
						__result = new Job(JobDefOf.AttackMelee, target)
						{
							maxNumMeleeAttacks = 1,
							expiryInterval = Rand.Range(420, 900),
							attackDoorIfTargetLost = false
						};
						return false;
					}
					else
					{
						//Log.Warning("Target too close and I can't melee right away");
						return true;
					}
				}
				
				//Log.Warning("Trying initiate job");
				__result = new Job(DefDatabase<JobDef>.GetNamed("AnimalRangeAttack"), shootable, JobGiver_AIFightEnemy.ExpiryInterval_ShooterSucceeded.RandomInRange, true)
				{
					verbToUse = rangeVerb
				};
				//Log.Warning("Succesfully created job");
				return false;
			}
			//Target is not null
			if(target != null)
			{
				//Can't shoot at target from current position. Find a new position

				bool canShootCondition = false;
				//Log.Warning("Try casting");
				canShootCondition = CastPositionFinder.TryFindCastPosition(new CastPositionRequest
				{
					caster = pawn,
					target = target,
					verb = rangeVerb,
					maxRangeFromTarget = 9999,
					wantCoverFromTarget = false
				}, out intVec);

				if (!canShootCondition)
				{
					//Log.Warning("Can't find place to shoot at target");
					return true;
				}
				//Log.Warning("Going");
				//Go to new destination

				//Protection againt not being able to find target.
				if (pawn.Position == intVec)
				{
					//Log.Warning(pawn + " already at position to shoot, but target not selected to shoot.");
					__result = new Job(JobDefOf.Wait, 100);
					return false;
				}
				
				__result = new Job(JobDefOf.Goto, intVec)
				{

					expiryInterval = JobGiver_AIFightEnemy.ExpiryInterval_ShooterSucceeded.RandomInRange,
					checkOverrideOnExpire = true
				};
				return false;
			}
			//Log.Warning("Hit end condition");
			return true;

		}
	}

	//Because of the way patching work, tamed animal are saviour and smarter at shooting than wild one
	[HarmonyPatch(typeof(JobGiver_AIDefendMaster),"TryGiveJob")]
	static class ARA_FightAI_Patch
	{
		static bool Prefix(ref JobGiver_AIFightEnemy __instance, ref Job __result, ref Pawn pawn)
		{
			//Log.Warning("Tame animal job detected");
			if (!pawn.RaceProps.Animal)
				return true;
			
			bool hasRangedVerb = false;


			List<Verb> verbList = pawn.verbTracker.AllVerbs;
			List<Verb> rangeList = new List<Verb>();
			for (int i = 0; i < verbList.Count; i++)
			{
				//Log.Warning("Checkity");
				//It corresponds with verbs anyway
				if (verbList[i].verbProps.range > 1.1f)
				{
					rangeList.Add(verbList[i]);
					hasRangedVerb = true;
				}
				//Log.Warning("Added Ranged Verb");
			}

			if (hasRangedVerb == false)
			{
				//Log.Warning("I don't have range verb");
				return true;
			}
			// this.SetCurMeleeVerb(updatedAvailableVerbsList.RandomElementByWeight((VerbEntry ve) => ve.SelectionWeight).verb);
			Verb rangeVerb = rangeList.RandomElementByWeight((Verb rangeItem) => rangeItem.verbProps.commonality);
			if (rangeVerb == null)
			{
				//Log.Warning("Can't get random range verb");
				return true;
			}

			
			 Thing enemyTarget = (Thing)AttackTargetFinder.BestAttackTarget((IAttackTargetSearcher)pawn, TargetScanFlags.NeedThreat, (Predicate<Thing>)(x =>
             x is Pawn || x is Building), 0.0f, rangeVerb.verbProps.range, new IntVec3(), float.MaxValue, false);

			
			if (enemyTarget == null)
			{
				//Log.Warning("I can't find anything to fight.");
				return true;
			}

			//Check if enemy directly next to pawn
			if (enemyTarget.Position.DistanceTo(pawn.Position) < rangeVerb.verbProps.minRange )
			{
				//If adjacent melee attack
				if (enemyTarget.Position.AdjacentTo8Way(pawn.Position))
				{
					__result = new Job(JobDefOf.AttackMelee,enemyTarget)
					{
						maxNumMeleeAttacks = 1,
						expiryInterval = Rand.Range(420, 900),
						attackDoorIfTargetLost = false
					};
					return false;
				}
				//Only go if I am to be released. This prevent animal running off.
				if (pawn.CanReach(enemyTarget, PathEndMode.Touch, Danger.Deadly, false) && pawn.playerSettings.Master.playerSettings.animalsReleased)
				{
					//Log.Warning("Melee Attack");
					__result = new Job(JobDefOf.AttackMelee,enemyTarget)
					{
						maxNumMeleeAttacks = 1,
						expiryInterval = Rand.Range(420, 900),
						attackDoorIfTargetLost = false
					};
					return false;
				}
				else
				{
					return true;
				}
			}
			
			//Log.Warning("got list of ranged verb");

			//Log.Warning("Attempting flag");
			bool flag1 = (double)CoverUtility.CalculateOverallBlockChance(pawn.Position, enemyTarget.Position, pawn.Map) > 0.00999999977648258;
			bool flag2 = pawn.Position.Standable(pawn.Map);
			bool flag3 = rangeVerb.CanHitTarget(enemyTarget);
			bool flag4 = (pawn.Position - enemyTarget.Position).LengthHorizontalSquared < 25;
			
			
			
			if (flag1 && flag2 && flag3 || flag4 && flag3)
			{
				//Log.Warning("Shooting");
				__result = new Job(DefDatabase<JobDef>.GetNamed("AnimalRangeAttack"), enemyTarget, JobGiver_AIFightEnemy.ExpiryInterval_ShooterSucceeded.RandomInRange, true)
				{
					verbToUse = rangeVerb

				};
				return false;
			}
			IntVec3 dest;
			bool canShootCondition = false;
			//Log.Warning("Try casting");

			//Animals with training seek cover
			/*
				if (pawn.training.IsCompleted(TrainableDefOf.Release) && (double)verb.verbProps.range > 7.0)
					Log.Warning("Attempting cover");
				Log.Warning("Try get flag radius :" + Traverse.Create(__instance).Method("GetFlagRadius", pawn).GetValue<float>());
				Log.Warning("Checking cast condition");
				*/

			//Don't find new position if animal not released.
			
			
			canShootCondition = CastPositionFinder.TryFindCastPosition(new CastPositionRequest
			{
				caster = pawn,
				target = enemyTarget,
				verb = rangeVerb,
				maxRangeFromTarget = rangeVerb.verbProps.range,
				wantCoverFromTarget = pawn.training.HasLearned(TrainableDefOf.Release) && (double)rangeVerb.verbProps.range > 7.0,
				locus = pawn.playerSettings.Master.Position,
				maxRangeFromLocus = Traverse.Create(__instance).Method("GetFlagRadius", pawn).GetValue<float>(),
                maxRegions = 50
			}, out dest);

			if (!canShootCondition)
			{
				//Log.Warning("I can't move to shooting position");
				
				
				return true;
			}
			
			if (dest == pawn.Position)
			{
				//Log.Warning("I will stay here and attack");
				__result = new Job(DefDatabase<JobDef>.GetNamed("AnimalRangeAttack"), enemyTarget, JobGiver_AIFightEnemy.ExpiryInterval_ShooterSucceeded.RandomInRange, true)
				{
					verbToUse = rangeVerb
				};
				return false;
			}
			//Log.Warning("Going to new place");
			__result =  new Job(JobDefOf.Goto, (LocalTargetInfo)dest)
			{
				expiryInterval = JobGiver_AIFightEnemy.ExpiryInterval_ShooterSucceeded.RandomInRange,
				checkOverrideOnExpire = true
			};
			return false;
			}

	}
	

}