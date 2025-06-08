# SCPVoiceChat

**SCPVoiceChat** — плагин для SCP: Secret Laboratory (Exiled), позволяющий SCP-персонажам гибко управлять режимом голосового чата с поддержкой русского, английского и китайского языков.

---

## Языки / Languages / 语言

- [Русский](#русский)
- [English](#english)
- [中文](#中文)

---

## Русский

### Возможности

- **Два режима для SCP:**
  - Только SCP-канал (общение только с SCP).
  - "Человеческий" режим — можно говорить с людьми через Proximity.
- **Быстрое переключение командой** `.voice` (или `/voice`).
- **Ограничение по ролям**: функция может работать только для определённых SCP или для всех SCP.
- **Мультиязычность**: поддерживаются русский, английский и китайский языки (выбирается в конфиге).
- **Все сообщения плагина и команды локализованы.**

### Установка

1. Скопируйте `SCPVoiceChat.dll` в папку `Exiled/Plugins` вашего сервера SCP:SL.
2. Перезапустите сервер.
3. (Опционально) Настройте язык и параметры в конфиге.

### Использование

- SCP может использовать команду `.voice` (или `/voice`), чтобы переключаться между режимами.
- После переключения игрок видит уведомление на выбранном языке.

### Конфиг (`config.yml`)

```yaml
SCPVoiceChat:
  IsEnabled: true            # Включение/отключение плагина
  Debug: false               # Включить/отключить режим отладки
  ScpVoiceRadius: 12.0       # Радиус слышимости SCP (информационно)
  OnlySpecificScps: true     # true — функция только для определённых SCP, false — для всех SCP
  Language: "Russian"        # Язык сообщений: "Russian", "English", "Chinese"
```

### Требования

- SCP:SL (актуальная версия)
- Exiled (совместимая версия для вашего сервера)

### Поддержка

Вопросы и предложения — пишите [автору](https://github.com/zazarick) или создайте issue.

---

## English

### Features

- **Two modes for SCP:**
  - SCP channel only (talk to SCPs only).
  - "Human" mode — allows talking to humans via Proximity.
- **Quick switching with `.voice` (or `/voice`) command.**
- **Role limitation:** Limit function to specific SCPs or all SCPs.
- **Multilingual:** Russian, English, and Chinese languages supported (configurable).
- **All plugin and command messages are localized.**

### Installation

1. Copy `SCPVoiceChat.dll` to your server's `Exiled/Plugins` folder.
2. Restart your server.
3. (Optionally) Configure language and options in the config.

### Usage

- SCPs can use the `.voice` (or `/voice`) command to switch between modes.
- After switching, the player receives a notification in the selected language.

### Config (`config.yml`)

```yaml
SCPVoiceChat:
  IsEnabled: true            # Enable/disable the plugin
  Debug: false               # Enable/disable debug mode
  ScpVoiceRadius: 12.0       # SCP hearing radius (informational)
  OnlySpecificScps: true     # true — only certain SCPs, false — all SCPs
  Language: "English"        # Message language: "Russian", "English", "Chinese"
```

### Requirements

- SCP:SL (latest version)
- Exiled (supported version for your server)

### Support

Questions and suggestions — contact [the author](https://github.com/zazarick) or open an issue.

---

## 中文

### 功能

- **SCP 语音两种模式：**
  - 仅限SCP频道（只能与其他SCP交流）。
  - “人类模式” — 可通过临近语音与人类交流。
- **使用 `.voice`（或 `/voice`）命令快速切换。**
- **可限制为特定SCP角色或所有SCP使用。**
- **多语言支持：** 中文、俄语、英语（可在配置中选择）。
- **所有插件与命令消息均已本地化。**

### 安装

1. 将 `SCPVoiceChat.dll` 复制到你服务器的 `Exiled/Plugins` 文件夹。
2. 重启服务器。
3. （可选）在配置文件中设置语言和参数。

### 使用方法

- SCP 玩家可以使用 `.voice` 或 `/voice` 命令在模式间切换。
- 切换后，玩家会收到所选语言的通知。

### 配置文件 (`config.yml`)

```yaml
SCPVoiceChat:
  IsEnabled: true            # 启用/禁用插件
  Debug: false               # 启用/禁用调试模式
  ScpVoiceRadius: 12.0       # SCP 听音半径（信息用）
  OnlySpecificScps: true     # true — 仅部分SCP能用，false — 所有SCP都能用
  Language: "Chinese"        # 消息语言："Russian"、"English"、"Chinese"
```

### 要求

- SCP:SL（最新版）
- Exiled（与你服务器兼容的版本）

### 支持

有疑问或建议？请联系[作者](https://github.com/zazarick)或提交 issue。

---

**Автор / Author / 作者**: zazarick
