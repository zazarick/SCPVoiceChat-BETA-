using System.Collections.Generic;

namespace SCPVoiceChat
{
    public static class Translations
    {
        public enum Language
        {
            Russian,
            English,
            Chinese
        }

        public static Language CurrentLanguage { get; set; } = Language.Russian;

        private static readonly Dictionary<string, (string ru, string en, string zh)> Messages = new Dictionary<string, (string ru, string en, string zh)>
        {
            ["OnlyPlayers"] = (
                "Только для игроков!",
                "Players only!",
                "仅限玩家使用！"
            ),
            ["OnlyScp"] = (
                "Только SCP могут использовать эту команду!",
                "Only SCPs can use this command!",
                "只有SCP可以使用此命令！"
            ),
            ["PluginNotInitialized"] = (
                "Плагин не инициализирован.",
                "Plugin is not initialized.",
                "插件未初始化。"
            ),
            ["NowHumanMode"] = (
                "Теперь вы говорите с людьми через голосовой чат (Proximity).",
                "You can now talk to humans via proximity voice chat.",
                "你现在可以通过临近语音与人类交流。"
            ),
            ["NowSCPMode"] = (
                "Теперь вы говорите только с SCP через SCP-канал.",
                "You now talk only to SCPs via SCP channel.",
                "你现在只能通过SCP频道与其他SCP交流。"
            ),
            ["VoiceProximityDenied"] = (
                "SCP может говорить через Proximity только в человеческом режиме!",
                "SCP can speak via Proximity only in human mode!",
                "SCP只有在人类模式下才能使用临近语音！"
            )
        };

        public static string Get(string key)
        {
            if (!Messages.ContainsKey(key))
                return key;

            var tuple = Messages[key];
            if (CurrentLanguage == Language.English)
            {
                return tuple.en;
            }
            else if (CurrentLanguage == Language.Chinese)
            {
                return tuple.zh;
            }
            else // Russian or default
            {
                return tuple.ru;
            }
        }
    }
}
