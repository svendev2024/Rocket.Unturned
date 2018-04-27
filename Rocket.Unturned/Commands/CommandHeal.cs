﻿using System;
using Rocket.API;
using Rocket.API.Chat;
using Rocket.API.Commands;
using Rocket.API.I18N;
using Rocket.API.Permissions;
using Rocket.API.Player;
using Rocket.Unturned.Player;

namespace Rocket.Unturned.Commands
{
    public class CommandHeal : ICommand
    {
        public bool SupportsCaller(ICommandCaller caller)
        {
            return caller is IOnlinePlayer;
        }

        public void Execute(ICommandContext context)
        {
            IPermissionProvider permissions = context.Container.Get<IPermissionProvider>();
            ITranslationLocator translations = ((UnturnedImplementation)context.Container.Get<IImplementation>()).ModuleTranslations;
            IChatManager chatManager = context.Container.Get<IChatManager>();

            IOnlinePlayer target;
            if (permissions.CheckPermission(context.Caller, Permission + ".Others") == PermissionResult.Grant 
                && context.Parameters.Length >= 1)
                target = context.Parameters.Get<IOnlinePlayer>(0);
            else
                target = (IOnlinePlayer) context.Caller;

            if (!(target is UnturnedPlayer uPlayer))
            {
                context.Caller.SendMessage($"Could not heal {((ICommandCaller)target).Name}", ConsoleColor.Red);
                return;
            }

            uPlayer.Heal(100);
            uPlayer.Bleeding = false;
            uPlayer.Broken = false;
            uPlayer.Infection = 0;
            uPlayer.Hunger = 0;
            uPlayer.Thirst = 0;

            if (target == context.Caller)
            {
                chatManager.SendLocalizedMessage(translations, (IOnlinePlayer)context.Caller, "command_heal_success");
                return;
            }

            chatManager.SendLocalizedMessage(translations, (IOnlinePlayer)context.Caller, "command_heal_success_me", ((ICommandCaller)target).Name);
            chatManager.SendLocalizedMessage(translations, target, "command_heal_success_other", context.Caller.Name);
        }

        public string Name => "Heal";
        public string Description => "Heals yourself or somebody else";
        public string Permission => "Rocket.Unturned.Heal";
        public string Syntax => "[player]";
        public ISubCommand[] ChildCommands => null;
        public string[] Aliases => null;
    }
}