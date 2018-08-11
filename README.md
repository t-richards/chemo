# Chemo

[![Build status](https://ci.appveyor.com/api/projects/status/pr8btfa4knxwsfgc?svg=true)](https://ci.appveyor.com/project/t-richards/chemo)

> I want to bring my computer to the brink of death using an overdose of chemo,
> only to have it maybe survive and be a normal computer

Chemo rids your Windows 10 machine of the pre-installed junk you don't want. We
can't guarantee that it will stay gone forever, but you may always come back for
another round of treatment, if you desire.

## :warning: Warning :warning:

Chemo can and will eat your lunch. Before using this tool, please make a
snapshot, system restore point, or other backup of some kind.

## Download

The latest development build can be [downloaded directly from AppVeyor](https://ci.appveyor.com/project/t-richards/chemo/build/artifacts).

## System Requirements

 - Windows 10

Note: This program is designed strictly for Windows 10. Use on other versions of
Microsoft Windows is not supported.

## Available Treatments

![screenshot](https://user-images.githubusercontent.com/3905798/42413333-cf818f54-81eb-11e8-937e-a392eec35cba.png)

While using Chemo, hover over any treatment in the tree to display a brief
description of what the treatment does.

### Apps

Treatments related to store apps or other apps.

#### `Remove Windows Store Apps`

This treatment removes most pre-installed Windows Store apps. For the complete
list of removals, please see:

https://github.com/t-richards/chemo/blob/master/Chemo/Treatment/RemoveStoreApps.cs#L16-L66

#### `Deprovision Windows Store Packages`

Deprovisions all packages. This prevents Windows Store applications from
re-appearing when a new user is created, or when a feature update is applied.

#### `Remove OneDrive`

Completely removes OneDrive and ALL ONEDRIVE DATA.

#### `Disable Cortana`

Prevents Cortana from appearing in the taskbar. A sign out is required to
complete this operation.

### Config

Opinionated configuration changes.

#### `Disable Force-Reboot After Windows Update`

Prevents Windows from automatically rebooting after applying Windows Updates

#### `Require Ctrl+Alt+Del At Sign In`

Requires the user to press Ctrl+Alt+Del at the sign-in screen for security reasons.

#### `Disable Internet Search Results in Start Menu`

Prevents internet junk from appearing when searching apps, files, etc. in the start menu.

#### Set System Clock to UTC

Sets the system's hardware clock to Coordinated Universal Time (UTC). The Windows default is localtime.

### Features

Windows Feature toggles.

### `Disable Internet Explorer`

Turns off the Windows feature, "Internet Explorer 11". A system reboot is
required to complete this operation.

### Privacy

Re-gain control of your privacy. (TBD)

## Contributing

Bug reports and pull requests are welcome on GitHub at
https://github.com/t-richards/chemo

## License

MIT License.
