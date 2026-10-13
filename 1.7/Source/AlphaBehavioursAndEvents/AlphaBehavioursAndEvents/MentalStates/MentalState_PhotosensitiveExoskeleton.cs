
using RimWorld;
using Verse;
using Verse.AI;

namespace AlphaBehavioursAndEvents
{
    public class MentalState_PhotosensitiveExoskeleton : MentalState
    {
        private int lastSunlightSeenTick = -1;

        protected override bool CanEndBeforeMaxDurationNow => false;

        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }

        public override void MentalStateTick(int delta)
        {
            base.MentalStateTick(delta);
            if (pawn.IsHashIntervalTick(30, delta))
            {
                if (lastSunlightSeenTick < 0 || ThoughtWorker_PhotosensitiveExoskeleton.InSunlight(pawn))
                {
                    lastSunlightSeenTick = Find.TickManager.TicksGame;
                }
                if (lastSunlightSeenTick >= 0 && Find.TickManager.TicksGame >= lastSunlightSeenTick + def.minTicksBeforeRecovery)
                {
                    RecoverFromState();
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lastSunlightSeenTick, "lastSunlightSeenTick", -1);
        }

        public override TaggedString GetBeginLetterText()
        {
            if (def.beginLetter.NullOrEmpty())
            {
                return null;
            }
           
            return def.beginLetter.Formatted(pawn.NameShortColored, pawn.Named("PAWN")).AdjustedFor(pawn).Resolve()
                .CapitalizeFirst();
        }
    }
}
