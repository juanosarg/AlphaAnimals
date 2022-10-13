using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.Sound;
using UnityEngine;

namespace AlphaBehavioursAndEvents
{
	class CompLarvaeToCocoon : ThingComp
	{
		int timeBeforeTransform;

		private CompProperties_LarvaeToCocoon Props
		{
			get
			{
				return (CompProperties_LarvaeToCocoon)this.props;
			}
		}

		

		public override void Initialize(CompProperties props)
		{
			base.Initialize(props);
			this.timeBeforeTransform = Find.TickManager.TicksGame + Props.ticksBeforeBecomingCocoon;
		}

		public override void CompTick()
		{
			base.CompTick();
			if (Find.TickManager.TicksGame == this.timeBeforeTransform && this.parent.Map != null)
			{
				IntVec3 pos = this.parent.Position;
				Map map = this.parent.Map;

				Thing thing = ThingMaker.MakeThing(ThingDef.Named("AA_BlackCocoon"));
				thing.SetFaction(Faction.OfPlayer);

				GenSpawn.Spawn(thing, pos, map);
				for (int i = 0; i < 5; i++)
				{
					IntVec3 c;
					CellFinder.TryFindRandomReachableCellNear(pos, map, 1, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
					FilthMaker.TryMakeFilth(c, map, ThingDefOf.Filth_Slime);
				}
				SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(pos, map));
				this.parent.Destroy();
			}
		}

		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<int>(ref this.timeBeforeTransform, "timeBeforeTransform");
		}
	}
}
