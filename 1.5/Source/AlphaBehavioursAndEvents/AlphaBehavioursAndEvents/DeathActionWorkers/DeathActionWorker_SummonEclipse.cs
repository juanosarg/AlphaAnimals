
using RimWorld;
using Verse;
using Verse.AI.Group;
namespace AlphaBehavioursAndEvents
{
    public class DeathActionWorker_SummonEclipse : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse, Lord previousLord)
        {
            if (corpse.Map != null)
            {
                IncidentDef localDef = InternalDefOf.AA_Eclipse;
                IncidentParms parms = StorytellerUtility.DefaultParmsNow(localDef.category, corpse.Map);
                localDef.Worker.TryExecute(parms);
            }

        }


    }
}
