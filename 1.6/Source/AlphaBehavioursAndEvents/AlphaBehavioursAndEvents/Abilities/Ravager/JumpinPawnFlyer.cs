﻿
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using UnityEngine.UIElements;
using Verse;
using Verse.Sound;
using VEF;
using VEF.Abilities;

namespace AlphaBehavioursAndEvents
{
    public class JumpingPawn : AbilityPawnFlyer
    {
        /*protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            FlyingPawn.DynamicDrawPhaseAt(DrawPhase.Draw,GetDrawPos(), flip);
        }*/

        protected override void Tick()
        {
            base.Tick();
            if (this.Map != null && Find.TickManager.TicksGame % 3 == 0)
            {
                var map = this.Map;
                
            }
        }

        private Vector3 GetDrawPos()
        {
            var x = ticksFlying / (float)ticksFlightTime;
            var drawPos = GetDrawPos();
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