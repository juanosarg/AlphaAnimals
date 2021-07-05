
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_SummonEclipse : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse)
        {
            if (corpse.Map != null)
            {
                IncidentDef localDef = IncidentDef.Named("AA_Eclipse");
                IncidentParms parms = StorytellerUtility.DefaultParmsNow(localDef.category, corpse.Map);
                localDef.Worker.TryExecute(parms);
            }

        }


    }
}
