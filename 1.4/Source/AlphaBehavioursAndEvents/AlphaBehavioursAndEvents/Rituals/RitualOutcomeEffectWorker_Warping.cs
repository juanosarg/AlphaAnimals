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
			Pawn sacrifice = jobRitual.PawnWithRole("animal");
			float size = sacrifice.RaceProps.baseBodySize;
			PawnGenerationRequest request = new PawnGenerationRequest();
			string str = outcome.label +" "+ jobRitual.Ritual.Label;
			TaggedString taggedString = outcome.description.Formatted(jobRitual.Ritual.Label);
			switch (outcome.positivityIndex) { 

				case -2:
					if (size < 0.5)
                    {
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_Eyeling"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);
						
					}
					else if (size < 1.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_Eyeling"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else
                    {
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_RedGoo"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					break;
				case -1:
					if (size < 0.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_RedGoo"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else if (size < 1.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_OcularJelly"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_OcularNightling"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					break;
				case 1:
					if (size < 0.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_RedSpore"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else if (size < 1.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_OcularNightling"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_EngorgedTentacularAberration"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					break;
				case 2:
					if (size < 0.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_OcularJelly"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else if (size < 1.5)
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_EngorgedTentacularAberration"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					else
					{
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_UnblinkingEye"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					}
					break;
				case 3:
					
						request = new PawnGenerationRequest(PawnKindDef.Named("AA_UnblinkingEye"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					
					break;
				default:
					request = new PawnGenerationRequest(PawnKindDef.Named("AA_OcularJelly"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true,  1f, false, true, true, false, false);

					break;

			}
			
				Pawn pawn = PawnGenerator.GeneratePawn(request);
				GenSpawn.Spawn(pawn, jobRitual.selectedTarget.Cell, jobRitual.Map, WipeMode.Vanish);
				ChoiceLetter let = LetterMaker.MakeLetter(str, taggedString, LetterDefOf.RitualOutcomePositive, lookTargets, null, null, null);
				Find.LetterStack.ReceiveLetter(let, null);




		}

		
		
	}
}
