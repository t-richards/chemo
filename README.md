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
  - Enable Dark Mode
  - Disable Transparency
  - Turn off animations
  - Replace the Spotlight wallpaper
- **Apps**
  - Remove junk apps
  - Uninstall OneDrive
- **Copilot & AI**
  - Remove Copilot, Click to Do, and Recall
- **Devices**
  - Prevent device companion apps
  - Block PC maker software from firmware (WPBT)
- **File Explorer**
  - Show file extensions, hidden files, full paths, and empty drives
  - Remove Home and Gallery
  - Restore the previous right-click menu
  - Disable automatic folder type discovery
- **Keyboard & Mouse**
  - Turn off the Sticky Keys shortcut
  - Turn off mouse acceleration
- **Lock Screen & Sign-in**
  - Lock after 15 minutes away
  - Require Ctrl+Alt+Del at sign-in
  - Skip the lock screen
  - Turn off lock screen tips
- **Network**
  - Make the current network private
- **Performance**
  - Trim background services
  - Disable background apps
- **Power**
  - Use the High performance power plan
  - Disable hibernation
- **Privacy**
  - Disable telemetry
  - Disable location tracking
- **Sound**
  - Turn off system sounds and the startup sound
  - Don't lower other sounds during calls
- **Start**
  - Disable internet search results
  - Disable Store results in search
  - Disable app recommendations
  - Unpin everything from Start
  - Hide recent items in Start
  - Restore previous Start menu layout
- **Taskbar**
  - Remove Widgets
  - Hide search and Task View
  - Unpin everything but File Explorer
  - Align the taskbar to the left
  - Combine taskbar buttons only when full
  - Show all system tray icons
  - Enable End task on right click
- **Time**
  - Use a 24-hour clock
  - Use year-month-day dates
  - Set system clock to UTC.
- **Updates**
  - Delay restarts after Windows Update

## Download

The latest release can be [downloaded from the releases section on GitHub](https://github.com/t-richards/chemo/releases).

## License

The application is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
