﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using static Ronin.Menus;
using static Ronin.SpellsManager;

namespace Ronin.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class LaneClear
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, Player.AttackRange, false).Count();
            var source = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderBy(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(Q.Range));
            var qDamage = source.GetDamage(SpellSlot.Q);
            if (count == 0) return;

            if (Q.IsReady() && LaneClearMenu.GetCheckBoxValue("qUse") && source.Health <= ObjectManager.Player.GetSpellDamage(source, SpellSlot.Q) && source.IsValidTarget(140))
            {
                Orbwalker.ForcedTarget = source;
                Q.Cast();
            }

            if (E.IsReady() && LaneClearMenu.GetCheckBoxValue("eUse"))
            {
                E.Cast(source.Position);
            }

        }
    }
}