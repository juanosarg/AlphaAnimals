using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Ability = VFECore.Abilities.Ability;


namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public class Ability_Summon_Hive : Ability
    {

        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo target in targets)
            {
               

                    ThingDef newThing = ThingDef.Named("AA_HiveEntrance");
                    Thing hive = GenSpawn.Spawn(newThing, target.Cell, target.Map, WipeMode.Vanish);
                    hive.SetFaction(Faction.OfPlayer);

               
            }
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (!base.ValidateTarget(target, showMessages)) return false;

            if (!target.Cell.InBounds(pawn.Map) || target.Cell.GetEdifice(pawn.Map) != null) {
                if (showMessages) Messages.Message("AA_HiveInvalidPosition".Translate(), MessageTypeDefOf.RejectInput, false);
                return false;
            }


            if (this.pawn.Map.listerThings.ThingsOfDef(ThingDef.Named("AA_HiveEntrance")).Count>0)
            {
                if (showMessages) Messages.Message("AA_OnlyOneHivePerMap".Translate(), MessageTypeDefOf.RejectInput, false);
                return false;
            }



            return true;
        }
    }
}