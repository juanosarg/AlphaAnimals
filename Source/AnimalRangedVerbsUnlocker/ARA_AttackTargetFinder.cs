using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;


//This is for testing purposes
namespace AnimalRangeAttack
{
	public static class ARA_AttackTargetFinder
	{
		private static List<IAttackTarget> tmpTargets = new List<IAttackTarget>();

		private static List<Pair<IAttackTarget, float>> availableShootingTargets = new List<Pair<IAttackTarget, float>>();

		private static List<float> tmpTargetScores = new List<float>();

		private static List<bool> tmpCanShootAtTarget = new List<bool>();

		private static List<IntVec3> tempDestList = new List<IntVec3>();

		private static List<IntVec3> tempSourceList = new List<IntVec3>();

		public static IAttackTarget BestAttackTarget(IAttackTargetSearcher searcher, TargetScanFlags flags, Predicate<Thing> validator = null, float minDist = 0f, float maxDist = 9999f, IntVec3 locus = default(IntVec3), float maxTravelRadiusFromLocus = 3.40282347E+38f, bool canBash = false)
		{
			Thing searcherThing = searcher.Thing;
			Pawn searcherPawn = searcher as Pawn;
			Verb verb = searcher.CurrentEffectiveVerb;
			if (verb == null)
			{
				//Log.Error("BestAttackTarget with " + searcher + " who has no attack verb.");
				return null;
			}
			bool onlyTargetMachines = verb != null && verb.IsEMP();
			float minDistanceSquared = minDist * minDist;
			float num = maxTravelRadiusFromLocus + verb.verbProps.range;
			float maxLocusDistSquared = num * num;
			Func<IntVec3, bool> losValidator = null;
			if ((byte)(flags & TargetScanFlags.LOSBlockableByGas) != 0)
			{
				losValidator = delegate(IntVec3 vec3)
				{
					Gas gas = vec3.GetGas(searcherThing.Map);
					return gas == null || !gas.def.gas.blockTurretTracking;
				};
			}
			Predicate<IAttackTarget> innerValidator = delegate(IAttackTarget t)
			{
				Thing thing = t.Thing;
				if (t == searcher)
				{
					return false;
				}
				if (minDistanceSquared > 0f && (float)(searcherThing.Position - thing.Position).LengthHorizontalSquared < minDistanceSquared)
				{
					return false;
				}
				if (maxTravelRadiusFromLocus < 9999f && (float)(thing.Position - locus).LengthHorizontalSquared > maxLocusDistSquared)
				{
					return false;
				}
				if (!searcherThing.HostileTo(thing))
				{
					return false;
				}
				if (validator != null && !validator(thing))
				{
					return false;
				}
				if ((byte)(flags & TargetScanFlags.NeedLOSToAll) != 0 && !searcherThing.CanSee(thing, losValidator))
				{
					if (t is Pawn)
					{
						if ((byte)(flags & TargetScanFlags.NeedLOSToPawns) != 0)
						{
							return false;
						}
					}
					else if ((byte)(flags & TargetScanFlags.NeedLOSToNonPawns) != 0)
					{
						return false;
					}
				}
				if ((byte)(flags & TargetScanFlags.NeedThreat) != 0 && t.ThreatDisabled(searcher))
				{
					return false;
				}
				Pawn pawn = t as Pawn;
				if (onlyTargetMachines && pawn != null && pawn.RaceProps.IsFlesh)
				{
					return false;
				}
				if ((byte)(flags & TargetScanFlags.NeedNonBurning) != 0 && thing.IsBurning())
				{
					return false;
				}
				if (searcherThing.def.race != null && searcherThing.def.race.intelligence >= Intelligence.Humanlike)
				{
					CompExplosive compExplosive = thing.TryGetComp<CompExplosive>();
					if (compExplosive != null && compExplosive.wickStarted)
					{
						return false;
					}
				}
				if (thing.def.size.x == 1 && thing.def.size.z == 1)
				{
					if (thing.Position.Fogged(thing.Map))
					{
						return false;
					}
				}
				else
				{
					bool flag2 = false;
					CellRect.CellRectIterator iterator = thing.OccupiedRect().GetIterator();
					while (!iterator.Done())
					{
						if (!iterator.Current.Fogged(thing.Map))
						{
							flag2 = true;
							break;
						}
						iterator.MoveNext();
					}
					if (!flag2)
					{
						return false;
					}
				}
				return true;
			};
			if (ARA_AttackTargetFinder.HasRangedAttack(searcher))
			{
				//Log.Warning("Finder: Range detected. Verb is " + verb);
				//Log.Warning("Finder: Pawn " + searcherPawn.Faction);
				ARA_AttackTargetFinder.tmpTargets.Clear();

				//This needs to be fixed. Can't use searcherThing. Doing this the hard way.
				//Set request for all attackable.
				ThingRequest thingReq = ThingRequest.ForGroup(ThingRequestGroup.AttackTarget);
				IEnumerable<Thing> searchSet = searcherThing.Map.listerThings.ThingsMatching(thingReq);
				
				foreach (IAttackTarget iTarget in searchSet)
				{
					ARA_AttackTargetFinder.tmpTargets.Add(iTarget);
				}
				
				if ((byte)(flags & TargetScanFlags.NeedReachable) != 0)
				{
					Predicate<IAttackTarget> oldValidator = innerValidator;
					innerValidator = ((IAttackTarget t) => oldValidator(t) && ARA_AttackTargetFinder.CanReach(searcherThing, t.Thing, canBash));
				}
				bool flag = false;
				if (searcherThing.Faction != Faction.OfPlayer)
				{
					//Log.Warning("Finder: Target available : " + ARA_AttackTargetFinder.tmpTargets.Count);
					for (int i = 0; i < ARA_AttackTargetFinder.tmpTargets.Count; i++)
					{
						IAttackTarget attackTarget = ARA_AttackTargetFinder.tmpTargets[i];
						if (attackTarget.Thing.Position.InHorDistOf(searcherThing.Position, maxDist) && innerValidator(attackTarget) && ARA_AttackTargetFinder.CanShootAtFromCurrentPosition(attackTarget, searcher, verb))
						{
							//Log.Warning("Finder: flag is true");
							flag = true;
							break;
						}
					}
				}
				
				IAttackTarget result;
				if (flag)
				{
					//Log.Warning("Finder: FlagTrue result");
					ARA_AttackTargetFinder.tmpTargets.RemoveAll((IAttackTarget x) => !x.Thing.Position.InHorDistOf(searcherThing.Position, maxDist) || !innerValidator(x));
					//Log.Warning("Finder: Target Avaliable : " + ARA_AttackTargetFinder.tmpTargets.Count);
					result = ARA_AttackTargetFinder.GetRandomShootingTargetByScore(ARA_AttackTargetFinder.tmpTargets, searcher, verb);
				}
				else
				{
					Predicate<Thing> validator2;
					if ((byte)(flags & TargetScanFlags.NeedReachableIfCantHitFromMyPos) != 0 && (byte)(flags & TargetScanFlags.NeedReachable) == 0)
					{
						//Log.Warning("Finder: Needs reachable");
						validator2 = ((Thing t) => innerValidator((IAttackTarget)t) && (ARA_AttackTargetFinder.CanReach(searcherThing, t, canBash) || ARA_AttackTargetFinder.CanShootAtFromCurrentPosition((IAttackTarget)t, searcher, verb)));
					}
					else
					{
						//Log.Warning("Finder: Running normal validator");
						validator2 = ((Thing t) => innerValidator((IAttackTarget)t));
					}
					result = (IAttackTarget)GenClosest.ClosestThing_Global(searcherThing.Position, ARA_AttackTargetFinder.tmpTargets, maxDist, validator2, null);
				}
				ARA_AttackTargetFinder.tmpTargets.Clear();
				//Log.Warning("Trying to return result " + result);
				return result;
			}
			//Log.Warning("Returning Null");
			return null;
		}

		private static bool CanReach(Thing searcher, Thing target, bool canBash)
		{
			Pawn pawn = searcher as Pawn;
			if (pawn != null)
			{
				if (!pawn.CanReach(target, PathEndMode.Touch, Danger.Some, canBash, TraverseMode.ByPawn))
				{
					return false;
				}
			}
			else
			{
				TraverseMode mode = (!canBash) ? TraverseMode.NoPassClosedDoors : TraverseMode.PassDoors;
				if (!searcher.Map.reachability.CanReach(searcher.Position, target, PathEndMode.Touch, TraverseParms.For(mode, Danger.Deadly, false)))
				{
					return false;
				}
			}
			return true;
		}


		private static bool HasRangedAttack(IAttackTargetSearcher t)
		{
			Verb currentEffectiveVerb = t.CurrentEffectiveVerb;
			return currentEffectiveVerb != null && !currentEffectiveVerb.verbProps.IsMeleeAttack;
		}

		private static bool CanShootAtFromCurrentPosition(IAttackTarget target, IAttackTargetSearcher searcher, Verb verb)
		{
			return verb != null && verb.CanHitTargetFrom(searcher.Thing.Position, target.Thing);
		}

		private static IAttackTarget GetRandomShootingTargetByScore(List<IAttackTarget> targets, IAttackTargetSearcher searcher, Verb verb)
		{
			Pair<IAttackTarget, float> pair;
			if (ARA_AttackTargetFinder.GetAvailableShootingTargetsByScore(targets, searcher, verb).TryRandomElementByWeight((Pair<IAttackTarget, float> x) => x.Second, out pair))
			{
				return pair.First;
			}
			return null;
		}

		private static List<Pair<IAttackTarget, float>> GetAvailableShootingTargetsByScore(List<IAttackTarget> rawTargets, IAttackTargetSearcher searcher, Verb verb)
		{
			ARA_AttackTargetFinder.availableShootingTargets.Clear();
			if (rawTargets.Count == 0)
			{
				return ARA_AttackTargetFinder.availableShootingTargets;
			}
			ARA_AttackTargetFinder.tmpTargetScores.Clear();
			ARA_AttackTargetFinder.tmpCanShootAtTarget.Clear();
			float num = 0f;
			IAttackTarget attackTarget = null;
			for (int i = 0; i < rawTargets.Count; i++)
			{
				ARA_AttackTargetFinder.tmpTargetScores.Add(-3.40282347E+38f);
				ARA_AttackTargetFinder.tmpCanShootAtTarget.Add(false);
				if (rawTargets[i] != searcher)
				{
					bool flag = ARA_AttackTargetFinder.CanShootAtFromCurrentPosition(rawTargets[i], searcher, verb);
					ARA_AttackTargetFinder.tmpCanShootAtTarget[i] = flag;
					if (flag)
					{
						float shootingTargetScore = ARA_AttackTargetFinder.GetShootingTargetScore(rawTargets[i], searcher, verb);
						ARA_AttackTargetFinder.tmpTargetScores[i] = shootingTargetScore;
						if (attackTarget == null || shootingTargetScore > num)
						{
							attackTarget = rawTargets[i];
							num = shootingTargetScore;
						}
					}
				}
			}
			if (num < 1f)
			{
				if (attackTarget != null)
				{
					ARA_AttackTargetFinder.availableShootingTargets.Add(new Pair<IAttackTarget, float>(attackTarget, 1f));
				}
			}
			else
			{
				float num2 = num - 30f;
				for (int j = 0; j < rawTargets.Count; j++)
				{
					if (rawTargets[j] != searcher)
					{
						if (ARA_AttackTargetFinder.tmpCanShootAtTarget[j])
						{
							float num3 = ARA_AttackTargetFinder.tmpTargetScores[j];
							if (num3 >= num2)
							{
								float second = Mathf.InverseLerp(num - 30f, num, num3);
								ARA_AttackTargetFinder.availableShootingTargets.Add(new Pair<IAttackTarget, float>(rawTargets[j], second));
							}
						}
					}
				}
			}
			return ARA_AttackTargetFinder.availableShootingTargets;
		}

		private static float GetShootingTargetScore(IAttackTarget target, IAttackTargetSearcher searcher, Verb verb)
		{
			float num = 60f;
			num -= Mathf.Min((target.Thing.Position - searcher.Thing.Position).LengthHorizontal, 40f);
			if (target.TargetCurrentlyAimingAt == searcher.Thing)
			{
				num += 10f;
			}
			if (searcher.LastAttackedTarget == target.Thing && Find.TickManager.TicksGame - searcher.LastAttackTargetTick <= 300)
			{
				num += 40f;
			}
			num -= CoverUtility.CalculateOverallBlockChance(target.Thing.Position, searcher.Thing.Position, searcher.Thing.Map) * 10f;
			Pawn pawn = target as Pawn;
			if (pawn != null && pawn.RaceProps.Animal && pawn.Faction != null && !pawn.IsFighting())
			{
				num -= 50f;
			}
			return num + ARA_AttackTargetFinder.FriendlyFireShootingTargetScoreOffset(target, searcher, verb);
		}

		private static float FriendlyFireShootingTargetScoreOffset(IAttackTarget target, IAttackTargetSearcher searcher, Verb verb)
		{
			if (verb.verbProps.ai_AvoidFriendlyFireRadius <= 0f)
			{
				return 0f;
			}
			Map map = target.Thing.Map;
			IntVec3 position = target.Thing.Position;
			int num = GenRadial.NumCellsInRadius(verb.verbProps.ai_AvoidFriendlyFireRadius);
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				IntVec3 intVec = position + GenRadial.RadialPattern[i];
				if (intVec.InBounds(map))
				{
					bool flag = true;
					List<Thing> thingList = intVec.GetThingList(map);
					for (int j = 0; j < thingList.Count; j++)
					{
						if (thingList[j] is IAttackTarget && thingList[j] != target)
						{
							if (flag)
							{
								if (!GenSight.LineOfSight(position, intVec, map, true, null, 0, 0))
								{
									break;
								}
								flag = false;
							}
							float num3;
							if (thingList[j] == searcher)
							{
								num3 = 40f;
							}
							else if (thingList[j] is Pawn)
							{
								num3 = ((!thingList[j].def.race.Animal) ? 18f : 7f);
							}
							else
							{
								num3 = 10f;
							}
							if (searcher.Thing.HostileTo(thingList[j]))
							{
								num2 += num3 * 0.6f;
							}
							else
							{
								num2 -= num3;
							}
						}
					}
				}
			}
			return Mathf.Min(num2, 0f);
		}

		public static IAttackTarget BestShootTargetFromCurrentPosition(IAttackTargetSearcher searcher, Predicate<Thing> validator, float maxDistance, float minDistance, TargetScanFlags flags)
		{
			return ARA_AttackTargetFinder.BestAttackTarget(searcher, flags, validator, minDistance, maxDistance, default(IntVec3), 3.40282347E+38f, false);
		}

		public static bool CanSee(this Thing seer, Thing target, Func<IntVec3, bool> validator = null)
		{
			ShootLeanUtility.CalcShootableCellsOf(ARA_AttackTargetFinder.tempDestList, target);
			for (int i = 0; i < ARA_AttackTargetFinder.tempDestList.Count; i++)
			{
				if (GenSight.LineOfSight(seer.Position, ARA_AttackTargetFinder.tempDestList[i], seer.Map, true, validator, 0, 0))
				{
					return true;
				}
			}
			ShootLeanUtility.LeanShootingSourcesFromTo(seer.Position, target.Position, seer.Map, ARA_AttackTargetFinder.tempSourceList);
			for (int j = 0; j < ARA_AttackTargetFinder.tempSourceList.Count; j++)
			{
				for (int k = 0; k < ARA_AttackTargetFinder.tempDestList.Count; k++)
				{
					if (GenSight.LineOfSight(ARA_AttackTargetFinder.tempSourceList[j], ARA_AttackTargetFinder.tempDestList[k], seer.Map, true, validator, 0, 0))
					{
						return true;
					}
				}
			}
			return false;
		}

	}
}
