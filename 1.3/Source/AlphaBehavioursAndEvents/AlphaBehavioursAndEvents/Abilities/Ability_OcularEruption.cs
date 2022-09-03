using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;
using System.Linq;

namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_OcularEruption : Ability
    {

        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo target in targets)
            {
                Pawn pawn = target.Thing as Pawn;
                IntRange numberOfEyes = new IntRange(4,7);

                List<BodyPartRecord> pieces = pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).ToList();

                if (pieces!=null && pieces.Count > 0) {
                    for (int i = 0; i < numberOfEyes.RandomInRange; i++)
                    {
                        BodyPartRecord part = pieces.RandomElement();
                        var hediff = HediffMaker.MakeHediff(HediffDef.Named("AA_MalevolentEye"), pawn, part);
                        pawn.health.AddHediff(hediff,part);
                    }

                }
                
                


            }
        }


    }
}