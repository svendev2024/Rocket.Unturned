﻿using Rocket.API.Eventing;
using Rocket.API.Player;
using Rocket.Core.Player.Events;
using UnityEngine;

namespace Rocket.Unturned.Player.Events
{
    public class UnturnedPlayerDeadEvent : OnlinePlayerEvent
    {
        public Vector3 Position { get; }

        public UnturnedPlayerDeadEvent(IOnlinePlayer player, Vector3 position) : base(player)
        {
            Position = position;
        }
        public UnturnedPlayerDeadEvent(IOnlinePlayer player, Vector3 position, bool global = true) : base(player, global)
        {
            Position = position;
        }
        public UnturnedPlayerDeadEvent(IOnlinePlayer player, Vector3 position, EventExecutionTargetContext executionTarget = EventExecutionTargetContext.Sync, bool global = true) : base(player, executionTarget, global)
        {
            Position = position;
        }
        public UnturnedPlayerDeadEvent(IOnlinePlayer player, Vector3 position, string name = null, EventExecutionTargetContext executionTarget = EventExecutionTargetContext.Sync, bool global = true) : base(player, name, executionTarget, global)
        {
            Position = position;
        }
    }
}