using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
	public class RitualOutcomeEffectWorker_Warping : RitualOutcomeEffectWorker_FromQuality
	{
		public RitualOutcomeEffectWorker_Warping()
		{
		}

		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
		}
		public RitualOutcomeEffectWorker_Warping(RitualOutcomeEffectDef def) : base(def)
		{
		}

		public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
		{
			float quality = base.GetQuality(jobRitual, progress);
			OutcomeChance outcome = this.GetOutcome(quality, jobRitual);
			LookTargets lookTargets = jobRitual.selectedTarget;




			string text = null;
			if (jobRitual.Ritual != null)
			{
				this.ApplyAttachableOutcome(totalPresence, jobRitual, outcome, out text, ref lookTargets);
			}
			
			Log.Message(outcome.positivityIndex.ToString());

			Log.Message("Should be spawning now");
			PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named("AA_FissionMouseSecond"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true, false, 1f, false, true, true, false, false);
			Pawn pawn = PawnGenerator.GeneratePawn(request);
			GenSpawn.Spawn(pawn, jobRitual.selectedTarget.Cell, jobRitual.Map, WipeMode.Vanish);

			if (outcome.positivityIndex == -1)
			{
			


			}
		}

		
		
	}
}
