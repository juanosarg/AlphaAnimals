using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld.Planet;

using RimWorld.QuestGen;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
	public class QuestNode_Root_MimeWandererJoin_WalkIn : QuestNode_Root_WandererJoin
	{
		protected override void RunInt()
		{
			base.RunInt();
			Quest quest = QuestGen.quest;
			quest.Delay(60000, delegate
			{
				quest.End(QuestEndOutcome.Fail, 0, null, null, QuestPart.SignalListenMode.OngoingOnly, false);
			}, null, null, null, false, null, null, false, null, null, null, false, QuestPart.SignalListenMode.OngoingOnly);
		}

		public override Pawn GeneratePawn()
		{
			Slate slate = QuestGen.slate;
			Gender? fixedGender = null;
			PawnGenerationRequest request;
			if (!slate.TryGet<PawnGenerationRequest>("overridePawnGenParams", out request, false))
			{

				request = new PawnGenerationRequest(PawnKindDefOf.Villager, null, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true,  20f, false, true, false, true, false, false, false, false,false, 0f, 0f,null, 1f, null, null, null, null, null, null, null, fixedGender, null, null, null);
			}
			Pawn pawn = PawnGenerator.GeneratePawn(request);
            if (AlphaAnimalsEvents_Mod.settings.flagMime) { pawn.health.AddHediff(HediffDef.Named("AA_MimeHediff"), null, null, null); }
			
			if (!pawn.IsWorldPawn())
			{
				Find.WorldPawns.PassToWorld(pawn, PawnDiscardDecideMode.Decide);
			}
			return pawn;
		}

		protected override void AddSpawnPawnQuestParts(Quest quest, Map map, Pawn pawn)
		{
			this.signalAccept = QuestGenUtility.HardcodedSignalWithQuestID("Accept");
			this.signalReject = QuestGenUtility.HardcodedSignalWithQuestID("Reject");
			quest.Signal(this.signalAccept, delegate
			{
				quest.SetFaction(Gen.YieldSingle<Pawn>(pawn), Faction.OfPlayer, null);
				quest.PawnsArrive(Gen.YieldSingle<Pawn>(pawn), null, map.Parent, null, false, null, null, null, null, null, false, false, true);
				quest.End(QuestEndOutcome.Success, 0, null, null, QuestPart.SignalListenMode.OngoingOnly, false);
			}, null, QuestPart.SignalListenMode.OngoingOnly);
			quest.Signal(this.signalReject, delegate
			{
				quest.GiveDiedOrDownedThoughts(pawn, PawnDiedOrDownedThoughtsKind.DeniedJoining, null);
				quest.End(QuestEndOutcome.Fail, 0, null, null, QuestPart.SignalListenMode.OngoingOnly, false);
			}, null, QuestPart.SignalListenMode.OngoingOnly);
		}

		public override void SendLetter(Quest quest, Pawn pawn)
		{
			TaggedString label = "LetterLabelWandererJoins".Translate(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
			TaggedString text = "LetterWandererJoins".Translate(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
			QuestNode_Root_WandererJoin_WalkIn.AppendCharityInfoToLetter("JoinerCharityInfo".Translate(pawn), ref text);
			PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref text, ref label, pawn);
			ChoiceLetter_AcceptJoiner choiceLetter_AcceptJoiner = (ChoiceLetter_AcceptJoiner)LetterMaker.MakeLetter(label, text, LetterDefOf.AcceptJoiner, null, null);
			choiceLetter_AcceptJoiner.signalAccept = this.signalAccept;
			choiceLetter_AcceptJoiner.signalReject = this.signalReject;
			choiceLetter_AcceptJoiner.quest = quest;
			choiceLetter_AcceptJoiner.StartTimeout(60000);
			Find.LetterStack.ReceiveLetter(choiceLetter_AcceptJoiner, null);
		}

		public static void AppendCharityInfoToLetter(TaggedString charityInfo, ref TaggedString letterText)
		{
			if (ModsConfig.IdeologyActive)
			{
				IEnumerable<Pawn> source = IdeoUtility.AllColonistsWithCharityPrecept();
				if (source.ToList().Count>0)
				{
					letterText += "\n\n" + charityInfo + "\n\n" + "PawnsHaveCharitableBeliefs".Translate() + ":";
					foreach (IGrouping<Ideo, Pawn> grouping in from c in source
															   group c by c.Ideo)
					{
						letterText += "\n  - " + "BelieversIn".Translate(grouping.Key.name.Colorize(grouping.Key.Color), grouping.Select((Pawn f) => f.NameShortColored.Resolve()).ToCommaList(false, false));
					}
				}
			}
		}

		private const int TimeoutTicks = 60000;

		public const float RelationWithColonistWeight = 20f;

		private string signalAccept;

		private string signalReject;
	}
}
