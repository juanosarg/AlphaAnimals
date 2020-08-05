
using Verse;
using System;

using RimWorld;





namespace AlphaBehavioursAndEvents
{
    class CompExploder : ThingComp
    {


        private float wickCounter = 0f;
        private float wickTimeSecondsAdjusted;
        public Random rand = new Random();
        private bool hasInitialized = false;

        public void wickInitializer()
        {

            if (this.Props.wickTimeVariance != 0)
            {
                int randomNumber = rand.Next(1, this.Props.wickTimeVariance);
                wickTimeSecondsAdjusted = this.Props.wickTimeSeconds + randomNumber;
                // Log.Warning(randomNumber.ToString());
                // Log.Warning(this.Props.wickTimeSeconds.ToString());
                // Log.Warning(wickTimeSecondsAdjusted.ToString());

            }
            else
            {
                wickTimeSecondsAdjusted = this.Props.wickTimeSeconds;
                //Log.Warning(wickTimeSecondsAdjusted.ToString());

            }
            hasInitialized = true;

        }

        public CompProperties_Exploder Props
        {
            get
            {
                return (CompProperties_Exploder)this.props;
            }
        }



        public override void CompTick()
        {
            if (!hasInitialized)
            {
                wickInitializer();
            }
            else
            {
                wickCounter += 1f;
                if (wickCounter > wickTimeSecondsAdjusted * 60f)
                {
                    this.Explode();
                }
            }
        }



        public void Explode()
        {

            GenExplosion.DoExplosion(this.parent.Position, this.parent.Map, this.Props.explosionForce, DamageDefOf.Flame, this.parent, -1, -1, null, null, null, null, null, 0f, 1, false, null, 0f, 1);

            this.parent.Destroy(DestroyMode.Vanish);
        }



    }
}
