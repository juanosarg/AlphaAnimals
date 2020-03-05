using System;
using RimWorld;
using Verse;

namespace NewAlphaAnimalSubproducts
{
    public class JobDriver_AnimalResource : JobDriver_GatherAnimalBodyResources
    {
        protected override float WorkTotal
        {
            get
            {
                return 1700f;
            }
        }

        protected override CompHasGatherableBodyResource GetComp(Pawn animal)
        {
            return animal.TryGetComp<CompAnimalProduct>();
        }
    }
}