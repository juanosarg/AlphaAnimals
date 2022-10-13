using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
	public class RitualStage_InteractWithAnimalNotDryad : RitualStage
	{
		public override TargetInfo GetSecondFocus(LordJob_Ritual ritual)
		{
			return ritual.assignments.Participants.FirstOrDefault((Pawn p) => p.RaceProps.Animal && !p.RaceProps.Dryad);
		}
	}
}
