# Chemo

[![build](https://github.com/t-richards/chemo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/t-richards/chemo/actions/workflows/dotnet.yml)

> I want to bring my computer to the brink of death using an overdose of chemo, only to have it maybe survive and be a normal computer

Chemo is an opinionated setup/debloat utility for Windows. Chemo is your first stop after a fresh install of Windows. Before you install any apps or prepare your system image, hit it with a dose of Chemo.

## Supported Windows Versions / Editions

Chemo aims to support versions of Windows that are still covered under "Security Support". Unfortunately, many settings applied by Chemo are not respected by the Home edition of Windows. Therefore, we only support "pro and above". Roughly speaking:

|    | Edition                            | Verdict                        |
|:--:|------------------------------------|--------------------------------|
| ✅ | Windows 11 IoT / LTSC / Enterprise | Almost-usable operating system |
| ✅ | Windows 11 Pro                     | Almost-usable operating system |
| ❌ | Windows 11 Home                    | Hot garbage                    |
| ❌ | Windows 11 SE                      | Hot garbage                    |

## Treatments

- **Appearance**
  - Use dark mode
  - Turn off transparency
  - Turn off animations
  - Replace the Spotlight wallpaper
- **Apps**
  - Remove preinstalled apps
  - Keep removed apps from coming back
  - Remove OneDrive
- **Copilot & AI**
  - Remove Copilot and Windows AI
- **Devices**
  - Stop devices from installing their own apps
  - Block apps hidden in your PC's firmware
- **File Explorer**
  - Show file extensions, hidden files, and more
  - Remove Home and Gallery
  - Bring back the full right-click menu
  - Use the same view for every folder
- **Keyboard & mouse**
  - Turn off the Sticky Keys shortcut
  - Turn off mouse acceleration
- **Lock screen & sign-in**
  - Lock after 15 minutes away
  - Require Ctrl+Alt+Del to sign in
  - Skip the lock screen
  - Turn off lock screen tips
- **Network**
  - Make your current network private
- **Performance**
  - Trim background services
  - Stop Store apps running in the background
- **Power**
  - Use the High performance power plan
  - Turn off hibernation
- **Privacy**
  - Turn off telemetry
  - Turn off location tracking
- **Sound**
  - Turn off system sounds
  - Don't lower other sounds during calls
- **Start**
  - Turn off web results in search
  - Turn off Store results in search
  - Turn off app recommendations
  - Unpin everything from Start
  - Hide recent items in Start
  - Bring back the previous Start menu
- **Taskbar**
  - Remove Widgets
  - Hide the search box and Task View
  - Unpin everything but File Explorer
  - Move taskbar icons to the left
  - Combine taskbar buttons only when it's full
  - Show all tray icons
  - Add End task to the taskbar
- **Time**
  - Use a 24-hour clock
  - Use year-month-day dates
  - Keep the hardware clock in UTC
- **Updates**
  - Delay restarts after updates

## Download

The latest release can be [downloaded from the releases section on GitHub](https://github.com/t-richards/chemo/releases).

## License

The application is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
