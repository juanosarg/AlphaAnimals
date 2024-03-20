
using RimWorld;
using Verse;
namespace AlphaBehavioursAndEvents
{
	public class CompProperties_CreateOcularPlants : CompProperties
	{
		public float harmFrequencyPerArea = 0.011f;



		public SimpleCurve radiusPerDayCurve;

		public CompProperties_CreateOcularPlants()
		{
			compClass = typeof(CompCreateOcularPlants);
		}
	}
}
