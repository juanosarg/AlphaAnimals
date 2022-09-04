
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using UnityEngine.UIElements;
using Verse;
using Verse.Sound;
using VFECore;
using VFECore.Abilities;

namespace AlphaBehavioursAndEvents
{
    public class JumpingPawn : AbilityPawnFlyer
    {
        public override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            FlyingPawn.DrawAt(GetDrawPos(), flip);
        }

        public override void Tick()
        {
            base.Tick();
            if (this.Map != null && Find.TickManager.TicksGame % 3 == 0)
            {
                var map = this.Map;
                //FleckCreationData data = FleckMaker.GetDataStatic(GetDrawPos(), map, VPE_DefOf.VPE_WarlordZap);
                //data.rotation = Rand.Range(0f, 360f);
                //map.flecks.CreateFleck(data);
            }
        }

        private Vector3 GetDrawPos()
        {
            var x = ticksFlying / (float)ticksFlightTime;
            var drawPos = position;
            drawPos.y = AltitudeLayer.Skyfaller.AltitudeFor();
            return drawPos + Vector3.forward * (x - Mathf.Pow(x, 2)) * 15f;
        }
        protected override void RespawnPawn()
        {
            Pawn flyingPawn = FlyingPawn;
            base.RespawnPawn();
            //VPE_DefOf.VPE_PowerLeap_Land.PlayOneShot(flyingPawn);
            FleckMaker.ThrowSmoke(flyingPawn.DrawPos, flyingPawn.Map, 1f);
            FleckMaker.ThrowDustPuffThick(flyingPawn.DrawPos, flyingPawn.Map, 2f, new Color(1f, 1f, 1f, 2.5f));
        }
    }
}