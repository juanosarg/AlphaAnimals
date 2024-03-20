using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class Ability_Summon_Extension : DefModExtension
    {     
        public PawnKindDef pawnToSpawn;
        public bool enrage;
        public bool playerFaction;
        public FactionDef factionIfNotOfPlayer;
        public int numberCreated;
        public bool allowAnAmount = false;
        public int allowedAmount;
    }
}
