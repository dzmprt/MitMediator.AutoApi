namespace MitMediator.AutoApi.HttpMediator.Tests;

public class HttpQueryStringsTests
{
    [Fact]
    public void ToQueryString_ShouldReturnEmptyForNullObject()
    {
        object? obj = null;

        var result = HttpQueryStrings.ToQueryString(obj!);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToQueryString_ShouldIgnoreDefaultValues()
    {
        var obj = new
        {
            IntValue = 0,
            DateValue = default(DateTime)
        };

        var result = HttpQueryStrings.ToQueryString(obj);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToQueryString_ShouldFormatSimpleValues()
    {
        var obj = new
        {
            Name = "Dmitriy",
            Age = 35
        };

        var result = HttpQueryStrings.ToQueryString(obj);

        Assert.Equal("?Name=Dmitriy&Age=35", result);
    }

    [Fact]
    public void ToQueryString_ShouldFormatDateTime()
    {
        var date = new DateTime(2024, 12, 31);
        var obj = new { Date = date };

        var result = HttpQueryStrings.ToQueryString(obj);

        Assert.Equal("?Date=2024-12-31", result);
    }

    [Fact]
    public void ToQueryString_ShouldHandleStringArray()
    {
        var obj = new { Tags = new[] { "api", "test" } };

        var result = HttpQueryStrings.ToQueryString(obj);

        Assert.Equal("?Tags=api&Tags=test", result);
    }

    [Fact]
    public void ToQueryString_ShouldHandleDateTimeArray()
    {
        var obj = new
        {
            Dates = new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2023, 6, 6)
            }
        };

        var result = HttpQueryStrings.ToQueryString(obj);

        Assert.Equal("?Dates=2022-01-01&Dates=2023-06-06", result);
    }

    [Fact]
    public void ToQueryString_ShouldHandleNestedObjects()
    {
        var obj = new
        {
            Filter = new
            {
                From = new DateTime(2024, 1, 1),
                Keyword = "core"
            }
        };

        var result = HttpQueryStrings.ToQueryString(obj);

        Assert.Equal("?Filter.From=2024-01-01&Filter.Keyword=core", result);
    }
    
    private class TestObject
    {
        public string? NullableProperty { get; set; } = null;
        public int NonZeroInt => 5;
    }
    
    [Fact]
    public void BuildQueryString_ShouldIgnoreNullProperty()
    {
        // Arrange
        var obj = new TestObject();

        // Act
        var query = HttpQueryStrings.ToQueryString(obj);

        // Assert
        Assert.DoesNotContain("NullableProperty", query); // проверяем, что null не сериализуется
        Assert.Contains("NonZeroInt=5", query); // контрольный параметр для уверенности
    }
}