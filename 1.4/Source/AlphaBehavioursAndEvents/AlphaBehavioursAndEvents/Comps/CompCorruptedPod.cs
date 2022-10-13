using System;
using RimWorld.Planet;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
	public class CompCorruptedPod : CompDryadHolder
	{
		public bool Full
		{
			get
			{
				return this.innerContainer.Count >= 3;
			}
		}

		public override void PostDeSpawn(Map map)
		{
			if (Find.TickManager.TicksGame < this.tickComplete)
			{
				this.innerContainer.TryDropAll(this.parent.Position, map, ThingPlaceMode.Near, null, null, true);
			}
		}

		public override void TryAcceptPawn(Pawn p)
		{
			base.TryAcceptPawn(p);
			if (this.Full)
			{
				Pawn connectedPawn = this.tree.TryGetComp<CompTreeConnection>().ConnectedPawn;
				this.tickComplete = Find.TickManager.TicksGame + (int)(60000f * base.Props.daysToComplete);
			}
		}

		protected override void Complete()
		{
			this.tickComplete = Find.TickManager.TicksGame;
			CompTreeConnection compTreeConnection = this.tree.TryGetComp<CompTreeConnection>();

			if (compTreeConnection != null)
			{
				int CounterFixed = this.innerContainer.Count;
				for (int i = 0; i < CounterFixed; i++)
				{
					
					Pawn pawn;
					
					if ((pawn = (this.innerContainer[i] as Pawn)) != null)
					{
						compTreeConnection.RemoveDryad(pawn);
						
						
					}
				}
				foreach (Pawn p in innerContainer)
                {
					Find.WorldPawns.PassToWorld(p, PawnDiscardDecideMode.Discard);
				}
				
				((Plant)GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("AA_Plant_CorruptedPodGauranlen"), null), this.parent.Position, this.parent.Map, WipeMode.Vanish)).Growth = 1f;
			}
			this.parent.Destroy(DestroyMode.Vanish);
		}

		public override string CompInspectStringExtra()
		{
			string text = base.CompInspectStringExtra();
			if (this.innerContainer.Count < 3)
			{
				if (!text.NullOrEmpty())
				{
					text += "\n";
				}
				text = string.Concat(new object[]
				{
					text,
					GenLabel.BestKindLabel(PawnKindDef.Named("AA_Dryad_Corruptor"), Gender.Male, true, -1).CapitalizeFirst(),
					": ",
					this.innerContainer.Count,
					"/",
					3
				});
			}
			return text;
		}
	}
}
