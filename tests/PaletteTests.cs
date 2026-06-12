using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using FluentAssertions;
using SoundBoard.PluginApi;
using Xunit;

namespace DraculaThemePlugin.Tests;

public class PaletteTests
{
    // The 25 semantic brush keys every palette must define for the host to
    // render a fully-styled UI. Mirrors the vocabulary in CLAUDE.md; a gap
    // here is exactly what the completeness theory below is meant to catch.
    private static readonly string[] SemanticKeys =
    {
        "SidebarBackground", "ContentBackground",
        "PanelBackground1", "PanelBackground2", "PanelBackground3", "SubtleBorder",
        "PrimaryAccent", "PrimaryAccentHover", "OnPrimaryAccent", "SecondaryAccent",
        "TextPrimary", "TextSecondary",
        "SuccessBackground", "SuccessForeground",
        "DangerBackground", "DangerForeground",
        "InfoBackground", "InfoForeground",
        "WarningBackground", "WarningForeground",
        "DropZoneHighlight", "WaveformBrush",
        "LoopInheritForeground", "LoopForceOnForeground", "LoopForceOffForeground",
    };

    private static readonly (string Id, string Name, string Uri)[] ExpectedPalettes =
    {
        ("dracula", "Dracula",
            "avares://DraculaThemePlugin/Themes/Dracula.axaml"),
        ("dracula-alabaster", "Dracula Alabaster",
            "avares://DraculaThemePlugin/Themes/DraculaAlabaster.axaml"),
    };

    private static List<ThemePalette> GetPalettes() =>
        new DraculaThemePlugin().GetPalettes().ToList();

    private static ResourceDictionary Load(string uri)
    {
        var source = new Uri(uri);
        var include = new ResourceInclude(source) { Source = source };
        return (ResourceDictionary)include.Loaded;
    }

    // ── Palette catalog ──────────────────────────────────────────────────
    [Fact]
    public void GetPalettes_returns_the_shipped_catalog()
    {
        var palettes = GetPalettes();

        palettes.Should().HaveCount(ExpectedPalettes.Length);
        palettes.Select(p => (p.Id, p.Name, Uri: p.ResourceUris.Single()))
            .Should().BeEquivalentTo(ExpectedPalettes);
    }

    // ── Resources resolve ────────────────────────────────────────────────
    [AvaloniaTheory]
    [MemberData(nameof(PaletteUris))]
    public void Palette_dictionary_loads_and_is_not_empty(string uri)
    {
        var dict = Load(uri);
        dict.Count.Should().BeGreaterThan(0);
    }

    // ── Semantic-key completeness — the important test ───────────────────
    [AvaloniaTheory]
    [MemberData(nameof(PaletteKeyMatrix))]
    public void Every_required_key_resolves_to_a_SolidColorBrush(string uri, string key)
    {
        var dict = Load(uri);

        dict.TryGetResource(key, null, out var value)
            .Should().BeTrue($"palette '{uri}' must define '{key}'");
        value.Should().BeOfType<SolidColorBrush>($"'{key}' in '{uri}' must be a SolidColorBrush");
    }

    // ── Flatness guard ───────────────────────────────────────────────────
    [AvaloniaTheory]
    [MemberData(nameof(PaletteUris))]
    public void Palette_is_a_flat_dictionary_with_no_variant_blocks(string uri)
    {
        var dict = Load(uri);

        dict.ThemeDictionaries.Should().BeEmpty(
            "a flat palette must not split colours into Dark/Light ThemeDictionaries");
        dict.MergedDictionaries.Should().BeEmpty(
            "a flat palette declares its brushes inline, not via merged dictionaries");
    }

    public static IEnumerable<object[]> PaletteUris() =>
        ExpectedPalettes.Select(p => new object[] { p.Uri });

    public static IEnumerable<object[]> PaletteKeyMatrix() =>
        from p in ExpectedPalettes
        from key in SemanticKeys
        select new object[] { p.Uri, key };
}
