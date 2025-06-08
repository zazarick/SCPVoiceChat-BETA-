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
        public override Version Version => new Version(1, 5, 1);

        public static Plugin Instance;

        // Храним у кого активен "человеческий" режим (true) или SCP-режим (false/нет записи)
        internal Dictionary<int, bool> HumanVoiceMode = new Dictionary<int, bool>();

        public override void OnEnabled()
        {
            Instance = this;
            // Set language from config
            if (Enum.TryParse<Translations.Language>(Config.Language, true, out var lang))
                Translations.CurrentLanguage = lang;
            else
                Translations.CurrentLanguage = Translations.Language.Russian;

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
            // Очищаем словарь при выходе игрока
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

            // По умолчанию SCP говорит только с SCP, если не включен HumanVoiceMode
            // Если HumanVoiceMode включён — разрешаем говорить с людьми (Proximity)
            bool humanMode = HumanVoiceMode.TryGetValue(ev.Player.Id, out bool enabled) && enabled;

            // Если SCP и не включен человеческий режим — запрещаем Proximity
            if (!humanMode && ev.VoiceMessage.Channel == VoiceChatChannel.Proximity)
            {
                ev.IsAllowed = false;
                ev.Player.ShowHint(Translations.Get("VoiceProximityDenied"), 2f);
                return;
            }

            // Если SCP и включен человеческий режим — разрешаем Proximity для SCP
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
                response = Translations.Get("OnlyPlayers");
                return false;
            }

            if (player.Role.Team != Team.SCPs)
            {
                response = Translations.Get("OnlyScp");
                return false;
            }

            var plugin = Plugin.Instance;
            if (plugin == null)
            {
                response = Translations.Get("PluginNotInitialized");
                return false;
            }

            bool current = plugin.HumanVoiceMode.TryGetValue(player.Id, out bool mode) && mode;
            bool newMode = !current;
            plugin.HumanVoiceMode[player.Id] = newMode;

            if (newMode)
                response = Translations.Get("NowHumanMode");
            else
                response = Translations.Get("NowSCPMode");
            return true;
        }
    }
}
