using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaBehavioursAndEvents

{
    [StaticConstructorOnStartup]
    public class Pawn_Detonator : Pawn
    {

        public static readonly Texture2D Detonate = ContentFinder<Texture2D>.Get("UI/Commands/Detonate", true);


        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
            yield return new Command_Action
            {

                defaultLabel = "AA_Detonate".Translate(),
                defaultDesc = "AA_DetonateDesc".Translate(),
                icon = Detonate,
                action = delegate ()
                {
                    
                    this.health.AddHediff(HediffDef.Named("AA_Kamikaze"));
                    HealthUtility.AdjustSeverity(this, HediffDef.Named("AA_Kamikaze"), 1);
                }
            };
            yield break;
        }

    }
}