using System.Text.Json;

namespace MitMediator.AutoApi.HttpMediator.Tests;

public class PrivateSetterResolverTests
{
    private class Person
    {
        public string Name { get; private set; } = "default";

        public int Age { get; private set; } = 0;
    }

    [Fact]
    public void PrivateSetterResolver_ShouldSetPrivateSettersDuringDeserialization()
    {
        // Arrange
        var json = "{\"Name\":\"Dmitriy\",\"Age\":33}";

        var options = new JsonSerializerOptions
        {
            TypeInfoResolver = new PrivateSetterResolver(),
            PropertyNameCaseInsensitive = true
        };

        // Act
        var person = JsonSerializer.Deserialize<Person>(json, options);

        // Assert
        Assert.NotNull(person);
        Assert.Equal("Dmitriy", person.Name);
        Assert.Equal(33, person.Age);
    }

    [Fact]
    public void PrivateSetterResolver_ShouldNotOverrideSettersIfAlreadyDefined()
    {
        var resolver = new PrivateSetterResolver();
        var options = new JsonSerializerOptions();
        var typeInfo = resolver.GetTypeInfo(typeof(Person), options);

        foreach (var prop in typeInfo.Properties)
        {
            Assert.NotNull(prop.Set); // private setters should now be assigned
        }
    }
}