
using RimWorld;
using System;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
	public class CompCreateOcularPlants : ThingComp
	{
		private int plantHarmAge;
		private int changeWeatherCounter;
		private const int ADay = 60000;
		private int ticksToPlantHarm;

		List<string> listOfUnaffectedTrees = new List<string>() { "GU_AlienTree", "AA_AlienTree", "Plant_TreeAnima", "Plant_TreeGauranlen", "AA_ElderAlienTree" };
		List<string> listOfUnaffectedCrops = new List<string>() { "GU_AlienGrass", "GU_RedLeaves", "GU_RedPlantsTall", "AA_AlienGrass", "AA_RedLeaves", "AA_RedPlantsTall",
		"Plant_GrassAnima","Plant_MossGauranlen"};
		List<string> listOfUnaffectedTerrains = new List<string>() { "GU_AlienSand", "GU_RichAlienSand", "GU_AlienSandFine", "GU_RedWaterShallow", "GU_RedWaterDeep", "GU_MossyRed",
		"GU_RedQuartzBase"};


		protected CompProperties_CreateOcularPlants Props => (CompProperties_CreateOcularPlants)props;

		public float AgeDays => (float)plantHarmAge / 60000f;

		public float CurrentRadius => Props.radiusPerDayCurve.Evaluate(AgeDays);

		public override void PostExposeData()
		{
			Scribe_Values.Look(ref plantHarmAge, "plantHarmAge", 0);
			Scribe_Values.Look(ref ticksToPlantHarm, "ticksToPlantHarm", 0);
			Scribe_Values.Look(ref changeWeatherCounter, "changeWeatherCounter", 0);
		}

		public override string CompInspectStringExtra()
		{
			return (string)("AA_PlantConversionRadius".Translate() + ": " + CurrentRadius.ToString("0.0") + "\n" + "AA_RadiusExpandRate".Translate() + ": ") + Math.Round(Props.radiusPerDayCurve.Evaluate(AgeDays + 1f) - Props.radiusPerDayCurve.Evaluate(AgeDays)) + "/" + "day".Translate();
		}

		public override void CompTick()
		{
			if (!parent.Spawned)
			{
				return;
			}
			plantHarmAge++;
			changeWeatherCounter++;
			ticksToPlantHarm--;
			if (ticksToPlantHarm <= 0)
			{
				float currentRadius = CurrentRadius;
				float num = (float)Math.PI * currentRadius * currentRadius * Props.harmFrequencyPerArea;
				float num2 = 60f / num;
				int num3;
				if (num2 >= 1f)
				{
					ticksToPlantHarm = GenMath.RoundRandom(num2);
					num3 = 1;
				}
				else
				{
					ticksToPlantHarm = 1;
					num3 = GenMath.RoundRandom(1f / num2);
				}
				for (int i = 0; i < num3; i++)
				{
					ConvertRandomPlantInRadius(currentRadius);
				}
			}
			if (changeWeatherCounter >= ADay)
			{
				if (ModLister.HasActiveModWithName("Alpha Biomes"))
				{
					Random rand = new Random();
                    if (rand.NextDouble() < 0.3)
                    {
						if (this.parent.Map.weatherManager.curWeather != WeatherDef.Named("AB_RedFog"))
						{
							
							this.parent.Map.weatherManager.curWeather = WeatherDef.Named("AB_RedFog");
							this.parent.Map.weatherManager.TransitionTo(WeatherDef.Named("AB_RedFog"));
						}
					}

				}

				changeWeatherCounter = 0;
			}

		}

		private void ConvertRandomPlantInRadius(float radius)
		{
			IntVec3 c = parent.Position + (Rand.InsideUnitCircleVec3 * radius).ToIntVec3();
			if (!c.InBounds(parent.Map))
			{
				return;
			}

			if (ModLister.HasActiveModWithName("Alpha Biomes"))
			{
				TerrainDef terrain = c.GetTerrain(parent.Map);
				if (!listOfUnaffectedTerrains.Contains(terrain.defName) && ((!terrain.HasTag("Floor")||terrain.IsWater)))
                {
					if (terrain == TerrainDefOf.Soil)
					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_AlienSand"));
					}
					else if (terrain == TerrainDefOf.SoilRich)

					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_RichAlienSand"));
					}
					else if (terrain.defName == "Mud")

					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_MossyRed"));
					}
					else if (terrain == TerrainDefOf.Sand)

					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_AlienSandFine"));
					}
					else if (terrain.IsWater)

					{
						if(terrain ==TerrainDefOf.WaterDeep|| terrain == TerrainDefOf.WaterOceanDeep)
                        {
							parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_RedWaterDeep")); 

						}
                        else { parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_RedWaterShallow"));}
						
					}
					else if (terrain == TerrainDefOf.Ice)

					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_MossyRed"));
					}
					else if (terrain == TerrainDefOf.Gravel || terrain.label.Contains("rough"))

					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_RedQuartzBase"));
					}
					
					else
					{
						parent.Map.terrainGrid.SetTerrain(c, TerrainDef.Named("GU_AlienSand"));
					}
				}
                

			}






			Plant plant = c.GetPlant(parent.Map);
			if (plant == null)
			{
				return;
			}
			if (plant.LeaflessNow)
			{
				Random rand = new Random();
				if (plant.def.plant.IsTree && !listOfUnaffectedTrees.Contains(plant.def.defName))
				{
					Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named("AA_AlienTree"), plant.Position, plant.Map, WipeMode.Vanish);
					Plant thingToDestroy = plant;
					thing2.Growth = thingToDestroy.Growth;
					plant.Kill();
				}
				else if (!plant.def.plant.IsTree && !listOfUnaffectedCrops.Contains(plant.def.defName)){
					if (rand.NextDouble() < 0.4)
					{
						Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named("AA_AlienGrass"), plant.Position, plant.Map, WipeMode.Vanish);
						Plant thingToDestroy = plant;
						thing2.Growth = thingToDestroy.Growth;
						plant.Kill();
					}
					else if (rand.NextDouble() > 0.4 && rand.NextDouble() < 0.7)
					{
						Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named("AA_RedLeaves"), plant.Position, plant.Map, WipeMode.Vanish);
						Plant thingToDestroy = plant;
						thing2.Growth = thingToDestroy.Growth;
						plant.Kill();
					}
					else if (rand.NextDouble() > 0.7)
					{
						Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named("AA_RedPlantsTall"), plant.Position, plant.Map, WipeMode.Vanish);
						Plant thingToDestroy = plant;
						thing2.Growth = thingToDestroy.Growth;
						plant.Kill();
					}
				}
				
				
			}
			else
			{
				if(!listOfUnaffectedTrees.Contains(plant.def.defName)&& !listOfUnaffectedCrops.Contains(plant.def.defName)) {
					plant.MakeLeafless(Plant.LeaflessCause.Poison);
				}
				
			}
		}
	}
}
