using System;
using RimWorld;
using Verse;

namespace NewAlphaAnimalSubproducts
{
    public class WorkGiver_AnimalResource : WorkGiver_GatherAnimalBodyResources
    {
        protected override JobDef JobDef
        {
            get
            {
                return LocalJobDefOf.GR_AnimalResource;
            }
        }

        protected override CompHasGatherableBodyResource GetComp(Pawn animal)
        {
            return animal.TryGetComp<CompAnimalProduct>();
        }
    }
}
