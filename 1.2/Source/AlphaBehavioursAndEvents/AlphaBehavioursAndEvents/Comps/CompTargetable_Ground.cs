using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;


namespace AlphaBehavioursAndEvents
{
    public class CompTargetable_Ground : CompTargetable
    {

        private LocalTargetInfo target;

        protected override bool PlayerChoosesTarget
        {
            get
            {
                return true;
            }
        }

        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetLocations = true
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
            yield break;
        }

        public override bool SelectedUseOption(Pawn p)
        {
           
                Find.Targeter.BeginTargeting(this.GetTargetingParameters(), delegate (LocalTargetInfo t)
                {
                    this.target = t;
                    this.parent.GetComp<CompUsable>().TryStartUseJob(p, this.target);

                }, p, null, null);
                return true;
            
           
        }

        public override void DoEffect(Pawn user)
        {
            
          
            Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("AA_PlantAnimaTreeJob"), this.target, this.parent);
            job.count = 1;
            user.jobs.TryTakeOrderedJob(job, JobTag.Misc);
          
        }
    }
}
