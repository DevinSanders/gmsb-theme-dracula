# gmsb-theme-dracula

Dracula theme pack for [Game Master Sound Board](https://github.com/DevinSanders/game-master-soundboard).

The canonical [Dracula](https://draculatheme.com) dark theme — high-contrast purple/pink/cyan on charcoal — used across hundreds of editors and terminals. Two independent, flat palettes, each its own dropdown entry:

| Palette            | Look  | Notes |
|--------------------|-------|-------|
| Dracula            | Dark  | Canonical Dracula. Charcoal #282A36, purple #BD93F9, pink #FF79C6, cyan #8BE9FD. |
| Dracula Alabaster  | Light | Alabaster cream surfaces with darker shades of Dracula's purple/pink accents for legibility. |

Each palette is a flat set of colours — one selectable look in the host's theme dropdown (shown as "Dracula: Dracula" and "Dracula: Dracula Alabaster"). There is no Dark/Light variant: the host applies the palette regardless of the active Avalonia variant and infers light/dark Fluent chrome (scrollbars, popups, focus rings) from the background luminance on its own.

## Install

Drop the released `.zip` onto Settings → Plugin Manager. Themes activate live — no restart needed. Pick the palette from Settings → Appearance → Theme.

Pre-built zips are attached to each [GitHub Release](../../releases).

## Build

```powershell
dotnet build src/DraculaThemePlugin.csproj
pwsh scripts/package.ps1
# → dist/github.DevinSanders-theme.dracula-1.0.0.zip
```

Requires .NET 10 SDK. `SoundBoard.PluginApi` is restored from NuGet automatically — no sibling checkout needed.

## Plugin manifest

| Field     | Value                         |
|-----------|-------------------------------|
| publisher | `github.DevinSanders`         |
| id        | `theme.dracula`               |
| entryDll  | `DraculaThemePlugin.dll`      |
| isTheme   | `true`                        |

## Attribution

Canonical Dracula color values from the official style guide at https://spec.draculatheme.com/. The Dracula color scheme is © Zeno Rocha and contributors, distributed under the MIT license.

Dracula Alabaster is an original light palette for this pack — designed to share Dracula's purple/pink identity in a light-friendly form. Inspired by Tonsky's [Alabaster](https://github.com/tonsky/sublime-scheme-alabaster) minimalism but with Dracula's accent vocabulary.

## License

Released under the [MIT License](LICENSE).

Dracula colors are © Zeno Rocha and contributors, licensed under MIT — see https://github.com/dracula/dracula-theme/blob/master/LICENSE.
