using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompChangeWeather : ThingComp
    {




        public CompProperties_ChangeWeather Props
        {
            get
            {
                return (CompProperties_ChangeWeather)this.props;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            if (this.parent.Map != null)
            {
                if (this.parent.Map.weatherManager.curWeather != WeatherDef.Named(Props.weatherDef))
                {
                    this.parent.Map.weatherManager.curWeather = WeatherDef.Named(Props.weatherDef);
                    this.parent.Map.weatherManager.TransitionTo(WeatherDef.Named(Props.weatherDef));
                }
            }
        }




    }
}