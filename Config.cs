using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCPVoiceChat
{
    public class Config : IConfig
    {
        [Description("Включение или выключение плагина SCPVoiceChat.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Включить или выключить режим отладки.")]
        public bool Debug { get; set; } = false;

        [Description("Радиус слышимости SCP при разговоре (в метрах). Не влияет на работу, используется для информирования.")]
        public float ScpVoiceRadius { get; set; } = 12f;

        [Description("Включить только для определённых SCP (если false — для всех SCP ролей).")]
        public bool OnlySpecificScps { get; set; } = true;

        [Description("Язык плагина: Russian, English или Chinese")]
        public string Language { get; set; } = "Russian";
    }
}
