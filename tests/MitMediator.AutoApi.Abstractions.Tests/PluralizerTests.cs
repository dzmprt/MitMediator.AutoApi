namespace MitMediator.AutoApi.Abstractions.Tests;

public class PluralizerTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Pluralize_ReturnsOriginal_IfEmpty(string input)
    {
        Assert.Equal(input, Pluralizer.Pluralize(input));
    }

    [Theory]
    [InlineData("deer")]
    [InlineData("fish")]
    [InlineData("species")]
    [InlineData("aircraft")]
    [InlineData("Series")]
    [InlineData("Moose")]
    public void Pluralize_ReturnsOriginal_IfAlreadyPlural(string input)
    {
        Assert.Equal(input, Pluralizer.Pluralize(input));
    }

    [Theory]
    [InlineData("man", "men")]
    [InlineData("child", "children")]
    [InlineData("tooth", "teeth")]
    [InlineData("foot", "feet")]
    [InlineData("mouse", "mice")]
    [InlineData("goose", "geese")]
    [InlineData("person", "people")]
    [InlineData("louse", "lice")]
    [InlineData("focus", "foci")]
    [InlineData("fungus", "fungi")]
    [InlineData("nucleus", "nuclei")]
    [InlineData("syllabus", "syllabi")]
    [InlineData("diagnosis", "diagnoses")]
    [InlineData("ellipsis", "ellipses")]
    [InlineData("appendix", "appendices")]
    [InlineData("phenomenon", "phenomena")]
    [InlineData("medium", "media")]
    [InlineData("bacterium", "bacteria")]
    [InlineData("ox", "oxen")]
    [InlineData("woman", "women")]
    [InlineData("cactus", "cacti")]
    [InlineData("analysis", "analyses")]
    [InlineData("index", "indices")]
    [InlineData("criterion", "criteria")]
    [InlineData("datum", "data")]
    [InlineData("formula", "formulas")]
    [InlineData("thesis", "theses")]
    [InlineData("parenthesis", "parentheses")]
    [InlineData("wife", "wives")]
    [InlineData("leaf", "leaves")]
    [InlineData("loaf", "loaves")]
    [InlineData("elf", "elves")]
    [InlineData("shelf", "shelves")]
    [InlineData("thief", "thieves")]
    [InlineData("wolf", "wolves")]
    [InlineData("scarf", "scarves")]
    [InlineData("Life", "Lives")]
    [InlineData("curriculum", "curricula")]
    [InlineData("basis", "bases")]
    [InlineData("quiz", "quizzes")]
    [InlineData("calf", "calves")]
    [InlineData("tomato", "tomatoes")]
    [InlineData("echo", "echoes")]
    [InlineData("veto", "vetoes")]
    [InlineData("cargo", "cargoes")]
    [InlineData("mosquito", "mosquitoes")]
    [InlineData("tornado", "tornadoes")]
    [InlineData("volcano", "volcanoes")]
    [InlineData("embargo", "embargoes")]
    [InlineData("deer", "deer")]
    public void Pluralize_IrregularCases_ReturnsCorrectPlural(string input, string expected)
    {
        Assert.Equal(expected, Pluralizer.Pluralize(input));
    }

    [Theory]
    [InlineData("city", "cities")]
    [InlineData("berry", "berries")]
    [InlineData("puppy", "puppies")]
    public void Pluralize_EndsWithY_ReturnsIes(string input, string expected)
    {
        Assert.Equal(expected, Pluralizer.Pluralize(input));
    }

    [Theory]
    [InlineData("bus", "buses")]
    [InlineData("box", "boxes")]
    [InlineData("buzz", "buzzes")]
    [InlineData("church", "churches")]
    [InlineData("brush", "brushes")]
    public void Pluralize_Sibilants_ReturnsEs(string input, string expected)
    {
        Assert.Equal(expected, Pluralizer.Pluralize(input));
    }

    [Theory]
    [InlineData("car", "cars")]
    [InlineData("book", "books")]
    [InlineData("cat", "cats")]
    public void Pluralize_Default_AddsS(string input, string expected)
    {
        Assert.Equal(expected, Pluralizer.Pluralize(input));
    }

    [Theory]
    [InlineData("dogs", true)]
    [InlineData("cats", true)]
    [InlineData("users", true)]
    [InlineData("boss", false)]
    [InlineData("campus", false)]
    [InlineData("crisis", false)]
    [InlineData("fish", true)]
    [InlineData("aircraft", true)]
    public void IsPlural_CorrectlyIdentifiesPluralWords(string input, bool expected)
    {
        Assert.Equal(expected, Pluralizer.IsPlural(input));
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    public void IsPlural_ReturnsFalse_IfEmpty(string input, bool expected)
    {
        Assert.Equal(expected, Pluralizer.IsPlural(input));
    }

    [Fact]
    public void ReplaceCase_PreservesCapitalization()
    {
        Assert.Equal("Men", Pluralizer.Pluralize("Man"));
        Assert.Equal("men", Pluralizer.Pluralize("man"));
    }
}