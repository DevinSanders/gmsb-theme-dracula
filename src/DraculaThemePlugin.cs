using System.Collections.Generic;
using SoundBoard.PluginApi;

namespace DraculaThemePlugin;

/// <summary>
/// Dracula — the canonical high-contrast dark theme used across hundreds
/// of editors and terminals (https://draculatheme.com).
///
/// <para>This pack exposes two independent, flat selectable palettes:
/// <list type="bullet">
///   <item><b>Dracula</b> — the canonical dark scheme. Dracula's named
///   palette (purple #BD93F9, pink #FF79C6, green #50FA7B, cyan #8BE9FD,
///   etc.) on charcoal #282A36.</item>
///   <item><b>Dracula Alabaster</b> — an independent light look: alabaster
///   cream surfaces accented with darker shades of Dracula's ANSI colors
///   for legibility. Inspired by Tonsky's Alabaster minimalism.</item>
/// </list></para>
///
/// <para>Each palette is a flat set of colours — one selectable look in the
/// host's theme dropdown. There is no Dark/Light variant: the host applies
/// the palette regardless of the active Avalonia variant and infers
/// light/dark Fluent chrome from the background luminance on its own.</para>
///
/// <para>Canonical color values from the Dracula style guide at
/// https://spec.draculatheme.com/.</para>
/// </summary>
public sealed class DraculaThemePlugin : IThemePlugin
{
    public string Id => "theme.dracula";
    public string Name => "Dracula";
    public string Version => PluginVersion.OfAssembly(typeof(DraculaThemePlugin));
    public string Author => "Devin Sanders";
    public string Description => "Dracula — two flat palettes: canonical Dracula (dark charcoal) and Dracula Alabaster (light cream).";

    public void Initialize(IPluginContext context) { }
    public void Shutdown() { }

    public IEnumerable<ThemePalette> GetPalettes() => new[]
    {
        new ThemePalette("dracula",           "Dracula",
            new[] { "avares://DraculaThemePlugin/Themes/Dracula.axaml" }),
        new ThemePalette("dracula-alabaster", "Dracula Alabaster",
            new[] { "avares://DraculaThemePlugin/Themes/DraculaAlabaster.axaml" }),
    };
}
