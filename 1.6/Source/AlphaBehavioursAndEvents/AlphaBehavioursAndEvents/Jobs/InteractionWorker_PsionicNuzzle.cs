﻿using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    public class InteractionWorker_PsionicNuzzle : InteractionWorker
    {
        public override void Interacted(Pawn initiator, Pawn recipient, List<RulePackDef> extraSentencePacks, out string letterText, out string letterLabel, out LetterDef letterDef, out LookTargets lookTargets)
        {
            this.AddNuzzledThought(initiator, recipient);
          
            letterText = null;
            letterLabel = null;
            letterDef = null;
            lookTargets = null;
        }

        private void AddNuzzledThought(Pawn initiator, Pawn recipient)
        {
            Thought_Memory newThought = (Thought_Memory)ThoughtMaker.MakeThought(InternalDefOf.AA_BeenPsionicallyNuzzled);
            newThought.SetForcedStage(NuzzleUtility.GetNuzzleStageIndex(initiator));
            if (recipient.needs.mood != null)
            {
                recipient.needs.mood.thoughts.memories.TryGainMemory(newThought);
            }
           
            recipient.health.AddHediff(InternalDefOf.AA_PsionicallyNuzzled);

        }

       
    }
}
