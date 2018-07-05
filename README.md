# Chemo

[![Build status](https://ci.appveyor.com/api/projects/status/pr8btfa4knxwsfgc?svg=true)](https://ci.appveyor.com/project/t-richards/chemo)

> I want to bring my computer to the brink of death using an overdose of chemo,
> only to have it maybe survive and be a normal computer

Chemo rids your Windows 10 machine of the pre-installed junk you don't want. We
can't guarantee that it will stay gone forever, but you may always come back for
another round of treatment, if you desire.

## System Requirements

 - Windows 10

Note: This program is designed strictly for Windows 10. Use on other versions
Microsoft Windows is not supported.

## Available Treatments

### `Remove Installed Windows Store Apps`

This treatment removes a select subset of pre-installed Windows Store apps. For
the complete list of removals, please see:

https://github.com/t-richards/chemo/blob/master/Treatment/RemoveStoreApps.cs#L15-L44

### `Deprovision Windows Store Apps`

Uninstalling store apps using the above treatment will only apply to the current
user. The "Deprovision" treatment prevents store apps from being automatically
installed when a new user is created, or when a feature update is applied.

### `Remove OneDrive`

This treatment completely removes OneDrive and ALL ONEDRIVE DATA.

### `Disable Internet Explorer`

Turns off the Windows feature, "Internet Explorer 11".

## Contributing

Bug reports and pull requests are welcome on GitHub at
https://github.com/t-richards/chemo

## License

MIT License.