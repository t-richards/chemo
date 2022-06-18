# Chemo

[![build](https://github.com/t-richards/chemo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/t-richards/chemo/actions/workflows/dotnet.yml)

> I want to bring my computer to the brink of death using an overdose of chemo,
> only to have it maybe survive and be a normal computer

Chemo rids your Windows 10 machine of the pre-installed junk you don't want. We
can't guarantee that it will stay gone forever, but you may always come back for
another round of treatment, if you desire.

## :warning: Warning :warning:

Chemo can and will eat your lunch. Before using this tool, please make a
snapshot, system restore point, or other backup of some kind.

You probably don't want to use Chemo on your primary machine. Unless you are comfortable with performing a fresh installation of Windows or restoring from a backup image, don't use this tool!

Chemo is currently in a pre-alpha state. Contributions and feedback are greatly appreciated!

## Download

The latest "release" can be [downloaded from the releases section on GitHub](https://github.com/t-richards/chemo/releases).

## System Requirements

 - Windows 10 version 1903 (May 2019 Update "19H1") or newer.

Note: This program is designed strictly for Windows 10.
Use on other versions of Microsoft Windows is not supported.

## Available Treatments

![chemo](https://user-images.githubusercontent.com/3905798/55773602-5f5f8800-5a5f-11e9-8f9f-672de4ffdcb6.png)

While using Chemo, hover over any treatment in the tree to display a brief
description of what the treatment does.

### Apps

Treatments related to store apps or other apps.

#### `Remove Windows Store Apps`

This treatment removes most pre-installed Windows Store apps. For the complete
list of removals, please see:

https://github.com/t-richards/chemo/blob/master/Chemo/Data/StoreApps.cs#L9-L64

#### `Deprovision Windows Store Packages`

Deprovisions most pre-installed Windows Store apps (same list as above). After the treatment is applied, creating a new user or applying a feature update will no longer trigger the re-installation of these junk applications.

> What is a provisioned Windows Store package?

When a new Windows user is created, Windows _will_ automatically install all "provisioned" store packages for that user. When a feature update is applied, Windows _may_ re-install any missing "provisioned" store packages for all users.

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

#### `Set System Clock to UTC`

Sets the system's hardware clock to Coordinated Universal Time (UTC). The Windows default is localtime.

### Features

Windows Feature toggles.

#### `Disable Internet Explorer`

Turns off the Windows feature, "Internet Explorer 11". A system reboot is
required to complete this operation.

### Privacy

Re-gain control of your privacy. (TBD)

## Contributing

Bug reports and pull requests are welcome on GitHub at
https://github.com/t-richards/chemo

## Build Requirements

Building Chemo requires the following tools:

 - Windows 10
 - Visual Studio 2022
 - .NET Framework 4.8
 - NuGet package manager
 - Windows 10 SDK (10.0.18362.0)

## License

The application is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
