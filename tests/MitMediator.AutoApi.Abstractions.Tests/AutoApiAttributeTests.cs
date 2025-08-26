namespace MitMediator.AutoApi.Abstractions.Tests;

public class AutoApiAttributeTests
{
    [Fact]
    public void Constructor_SetsAllPropertiesCorrectly_WithoutSuffix()
    {
        // Arrange
        var attr = new AutoApiAttribute("tag", "v1", "desc", customPattern: "custom", httpMethodType: HttpMethodType.Post, customResponseContentType: "customResponseContentType");
        
        // Act & Assert
        Assert.Equal("tag", attr.Tag);
        Assert.Equal("v1", attr.Version);
        Assert.Equal("desc", attr.Description);
        Assert.Null(attr.PatternSuffix);
        Assert.Equal("custom", attr.CustomPattern);
        Assert.Equal(HttpMethodType.Post, attr.HttpMethodType);
        Assert.Equal("customResponseContentType", attr.CustomResponseContentType);
    }
    
    [Fact]
    public void Constructor_SetsAllPropertiesCorrectly_WithoutCustomPattern()
    {
        // Arrange
        var attr = new AutoApiAttribute("tag", "v1", "desc", patternSuffix: "suffix", httpMethodType: HttpMethodType.Post);
        
        // Act & Assert
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
        // Arrange
        var ex = Assert.Throws<Exception>(() => new AutoApiAttribute("tag", customPattern:"custom-pattern", patternSuffix:"suffix"));
        
        // Act & Assert
        Assert.Equal("Suffix can't be specified when a custom pattern is provided.", ex.Message);
    }
}
