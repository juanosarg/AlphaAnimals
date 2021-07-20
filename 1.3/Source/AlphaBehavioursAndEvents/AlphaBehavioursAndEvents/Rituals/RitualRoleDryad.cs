using System;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
	public class RitualRoleDryad : RitualRole
	{
		public override bool Animal
		{
			get
			{
				return true;
			}
		}

		public override bool AppliesToPawn(Pawn p, out string reason, LordJob_Ritual ritual = null, RitualRoleAssignments assignments = null, Precept_Ritual precept = null)
		{
			if (!p.RaceProps.Animal)
			{
				reason = "MessageRitualRoleMustBeAnimal".Translate(base.LabelCap);
				return false;
			}
			if (p.def.defName != "AA_Dryad_Ocular")
			{
				reason = "AA_MessageRitualRoleNeedsDryad".Translate();
				return false;
			}
			if (!p.Faction.IsPlayerSafe())
			{
				reason = "MessageRitualRoleMustBeColonist".Translate(base.Label);
				return false;
			}
			reason = null;
			return true;
		}

		public override bool AppliesToRole(Precept_Role role, out string reason, Precept_Ritual ritual = null, Pawn p = null)
		{
			reason = null;
			return false;
		}

		
	}
}
