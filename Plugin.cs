using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using VoiceChat;
using CommandSystem;

namespace SCPVoiceChat
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCPVoiceChat";
        public override string Author => "Zazar";
        public override string Prefix => "scpvoicechat";
        public override Version Version => new Version(1, 4, 2);

        public static Plugin Instance;
        internal Dictionary<int, bool> HumanVoiceMode = new Dictionary<int, bool>();

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.VoiceChatting += OnVoiceChatting;
            Exiled.Events.Handlers.Player.Destroying += OnPlayerDestroying;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.VoiceChatting -= OnVoiceChatting;
            Exiled.Events.Handlers.Player.Destroying -= OnPlayerDestroying;
            HumanVoiceMode.Clear();
            base.OnDisabled();
        }

        private void OnPlayerDestroying(DestroyingEventArgs ev)
        {
            if (HumanVoiceMode.ContainsKey(ev.Player.Id))
                HumanVoiceMode.Remove(ev.Player.Id);
        }

        private void OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            if (!Config.IsEnabled)
                return;

            bool isScp = ev.Player.Role.Team == Team.SCPs;
            bool isAllowedScp = (Config.OnlySpecificScps && IsSpecificScp(ev.Player.Role.Type))
                || (!Config.OnlySpecificScps && isScp);

            if (!isAllowedScp)
                return;
            bool humanMode = HumanVoiceMode.TryGetValue(ev.Player.Id, out bool enabled) && enabled;
            if (!humanMode && ev.VoiceMessage.Channel == VoiceChatChannel.Proximity)
            {
                ev.IsAllowed = false;
                return;
            }
            if (humanMode && ev.VoiceMessage.Channel == VoiceChatChannel.Proximity)
            {
                ev.IsAllowed = true;
            }
        }

        private bool IsSpecificScp(RoleTypeId role)
        {
            return role == RoleTypeId.Scp049
                || role == RoleTypeId.Scp079
                || role == RoleTypeId.Scp096
                || role == RoleTypeId.Scp106
                || role == RoleTypeId.Scp173
                || role == RoleTypeId.Scp939
                || role == RoleTypeId.Scp3114
                || role == RoleTypeId.Scp0492;
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class VoiceCommand : ICommand
    {
        public string Command => "voice";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Переключает режим голосового чата SCP между SCP-режимом и Человеческим режимом.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!(sender is CommandSender commandSender) || !Player.TryGet(commandSender.SenderId, out Player player))
            {
                response = "Только для игроков!";
                return false;
            }

            if (player.Role.Team != Team.SCPs)
            {
                response = "Только SCP могут использовать эту команду!";
                return false;
            }

            var plugin = Plugin.Instance;
            if (plugin == null)
            {
                response = "Плагин не инициализирован.";
                return false;
            }

            bool current = plugin.HumanVoiceMode.TryGetValue(player.Id, out bool mode) && mode;
            bool newMode = !current;
            plugin.HumanVoiceMode[player.Id] = newMode;

            if (newMode)
                response = "Теперь вы говорите с людьми через голосовой чат (Proximity).";
            else
                response = "Теперь вы говорите только с SCP через SCP-канал.";
            return true;
        }
    }
}
