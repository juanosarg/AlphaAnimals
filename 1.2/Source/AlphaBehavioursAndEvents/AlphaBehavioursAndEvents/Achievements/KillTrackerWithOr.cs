using System;
using System.Reflection;
using HarmonyLib;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace AchievementsExpanded
{
    public class KillTrackerWithOr : KillTracker
    {
        public KillTrackerWithOr()
        {
        }

        public KillTrackerWithOr(KillTrackerWithOr reference) : base(reference)
        {
            kindDefList = reference.kindDefList;
           
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref kindDefList, "kindDefList", LookMode.Def, LookMode.Value);
           
        }

        public override bool Trigger(Pawn pawn, DamageInfo? dinfo)
        {
            //base.Trigger(pawn, dinfo);
            if (killedThings.Contains(pawn.GetUniqueLoadID()))
                return false;
            else
                killedThings.Add(pawn.GetUniqueLoadID());
            bool instigator = instigatorFactionDefs.NullOrEmpty() || (dinfo?.Instigator?.Faction?.def != null && instigatorFactionDefs.Contains(dinfo.Value.Instigator.Faction.def));
            bool kind = false;
            if (kindDefList != null)
            {
                foreach (KeyValuePair<PawnKindDef, int> set in kindDefList)
                {

                    kind = (pawn.kindDef == set.Key);
                    if (kind) { break; }
                }
            }
            else kind = true;

            bool race = raceDef is null || pawn.def == raceDef;
            bool faction = factionDefs.NullOrEmpty() || (pawn.Faction != null && factionDefs.Contains(pawn.Faction.def));
            return kind && race && faction && instigator && (count <= 1 || ++triggeredCount >= count);
        }

        Dictionary<PawnKindDef, int> kindDefList = new Dictionary<PawnKindDef, int>();
       
    }
}



