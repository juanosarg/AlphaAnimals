using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class Ability_Damage_Extension : DefModExtension
    {
        public DamageDef damageDef = null;
        public float damage;
        public float armourPen;
        public int repeatAmount = 1;
        public bool teleport = false;
        public bool jump = false;
        public string mote;
        public string moteFailed;
        public BodyPartDef bodyPart = null;
        public bool cleaveAttack = false;
        public bool instakill = false;

    }
}
