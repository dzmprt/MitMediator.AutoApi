using System.Reflection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests;

public class SampleActionAttribute : BaseActionAttribute
{
    public SampleActionAttribute(string tag, string? version, string? description, string? customPattern)
        : base(tag, version, description, customPattern)
    {
    }
}


public class BaseActionAttributeTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var tag = "Create";
        var version = "v1";
        var description = "Creates something";
        var pattern = "api/create";

        // Act
        var attribute = new SampleActionAttribute(tag, version, description, pattern);

        // Assert
        Assert.Equal(tag, attribute.Tag);
        Assert.Equal(version, attribute.Version);
        Assert.Equal(description, attribute.Description);
        Assert.Equal(pattern, attribute.CustomPattern);
    }

    public class GetBasePatternTests
    {
        [Fact]
        public void ReturnsCustomPattern_IfProvided()
        {
            var attr = new SampleActionAttribute("resource", null, null, customPattern: "custom/pattern");

            var result = InvokeGetBasePattern(attr);
            Assert.Equal("custom/pattern", result);
        }

        [Fact]
        public void ReturnsVersionAndLowercaseTag_IfVersionProvided()
        {
            var attr = new SampleActionAttribute("Resource", "v1", null, null);
            var result = InvokeGetBasePattern(attr);
            Assert.Equal("v1/resource", result);
        }

        [Fact]
        public void ReturnsLowercaseTag_IfVersionIsNull()
        {
            var attr = new SampleActionAttribute("Users", null, null, null);

            var result = InvokeGetBasePattern(attr);
            Assert.Equal("users", result);
        }

        [Fact]
        public void ReturnsLowercaseTag_IfVersionIsEmpty()
        {
            var attr = new SampleActionAttribute("Orders", " ", null, null);

            var result = InvokeGetBasePattern(attr);
            Assert.Equal("orders", result);
        }

        private static string InvokeGetBasePattern(BaseActionAttribute attr)
        {
            var method = typeof(Helpers)
                .GetMethod("GetBasePattern", BindingFlags.NonPublic | BindingFlags.Static);
            return (string)method!.Invoke(null, new object[] { attr })!;
        }
    }
}