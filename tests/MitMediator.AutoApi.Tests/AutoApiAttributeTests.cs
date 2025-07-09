using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests;

public class AutoApiAttributeTests
{
    [Fact]
    public void Constructor_SetsAllPropertiesCorrectly_WithoutSuffix()
    {
        var attr = new AutoApiAttribute("tag", "v1", "desc", customPattern: "custom", httpMethodType: HttpMethodType.Post);
        Assert.Equal("tag", attr.Tag);
        Assert.Equal("v1", attr.Version);
        Assert.Equal("desc", attr.Description);
        Assert.Null(attr.PatternSuffix);
        Assert.Equal("custom", attr.CustomPattern);
        Assert.Equal(HttpMethodType.Post, attr.HttpMethodType);
    }
    
    [Fact]
    public void Constructor_SetsAllPropertiesCorrectly_WithoutCustomPattern()
    {
        var attr = new AutoApiAttribute("tag", "v1", "desc", patternSuffix: "suffix", httpMethodType: HttpMethodType.Post);
        Assert.Equal("tag", attr.Tag);
        Assert.Equal("v1", attr.Version);
        Assert.Equal("desc", attr.Description);
        Assert.Equal("suffix", attr.PatternSuffix);
        Assert.Null(attr.CustomPattern);
        Assert.Equal(HttpMethodType.Post, attr.HttpMethodType);
    }

    [Fact]
    public void GetPattern_WithConflictingSuffixAndCustom_Throws()
    {
        var ex = Assert.Throws<Exception>(() => new AutoApiAttribute("tag", customPattern:"custom-pattern", patternSuffix:"suffix"));
        Assert.Equal("Suffix can't be specified when a custom pattern is provided.", ex.Message);
    }
}
