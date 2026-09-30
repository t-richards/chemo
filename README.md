# Chemo

[![build](https://github.com/t-richards/chemo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/t-richards/chemo/actions/workflows/dotnet.yml)

> I want to bring my computer to the brink of death using an overdose of chemo, only to have it maybe survive and be a normal computer

Chemo is an opinionated setup/debloat utility for Windows. Chemo is my first stop after a fresh install of Windows. Before I install any apps or prepare a system image, I hit it with a dose of Chemo.

<img width="897" height="664" alt="Image" src="https://github.com/user-attachments/assets/6da1382d-3dbf-4fc9-b4f1-d1c0111c98ec" />

## Supported Windows Versions / Editions

Chemo aims to support versions of Windows that are still covered under "Security Support". Unfortunately, many settings applied by Chemo are not respected by the Home edition of Windows. Therefore, we only support "pro and above". Roughly speaking:

|    | Edition                            | Verdict                        |
|:--:|------------------------------------|--------------------------------|
| ✅ | Windows 11 IoT / LTSC / Enterprise | Almost-usable operating system |
| ✅ | Windows 11 Pro                     | Almost-usable operating system |
| ❌ | Windows 11 Home                    | Hot garbage                    |
| ❌ | Windows 11 SE                      | Hot garbage                    |

## Treatments

Chemo applies a set of configuration changes and debloat strategies according to my personal preference. See [treatments.md](doc/treatments.md) for every treatment and what it does.

## Download

The latest release can be [downloaded from the releases section on GitHub](https://github.com/t-richards/chemo/releases).

## License

The application is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
