﻿using System;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class HediffCompProperties_TurnWhenDead : HediffCompProperties
    {
        public string thingToTurnTo = "";
        public float severityToTurn = 0.85f;
        public List<int> numberOfSpawn = null;
        public bool isHostile = true;
        public bool keepGender = false;

        public HediffCompProperties_TurnWhenDead()
        {
            this.compClass = typeof(HediffComp_TurnWhenDead);
        }
    }
}
