namespace MitMediator.AutoApi.HttpMediator.Tests;

public class HttpQueryStringsExtensionsTests
{
    private class SimpleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public Guid Token { get; set; }
    }

    private class NestedModel
    {
        public SimpleModel? Inner { get; set; }
        public string? Comment { get; set; }
    }

    private enum Status { None, Active, Disabled }

    private class EnumModel
    {
        public Status Status { get; set; }
    }

    [Fact]
    public void NullObject_ReturnsEmpty()
    {
        object? obj = null;
        var result = obj.ToQueryString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void EmptyObject_ReturnsEmpty()
    {
        var obj = new SimpleModel();
        var result = obj.ToQueryString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void PrimitiveValues_AreSerialized()
    {
        var obj = new SimpleModel
        {
            Id = 42,
            Name = "Test",
            IsActive = true,
            Created = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            Token = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        var result = obj.ToQueryString();

        Assert.Contains("Id=42", result);
        Assert.Contains("Name=Test", result);
        Assert.Contains("IsActive=true", result);
        Assert.Contains("Created=2024-12-31T00%3A00%3A00.000Z", result);
        Assert.Contains("Token=11111111-1111-1111-1111-111111111111", result);
    }

    [Fact]
    public void NestedObject_IsFlattened()
    {
        var obj = new NestedModel
        {
            Comment = "Hello",
            Inner = new SimpleModel
            {
                Id = 1,
                Name = "Nested",
                IsActive = true
            }
        };

        var result = obj.ToQueryString();

        Assert.Contains("Comment=Hello", result);
        Assert.Contains("Inner.Id=1", result);
        Assert.Contains("Inner.Name=Nested", result);
        Assert.Contains("Inner.IsActive=true", result);
    }

    [Fact]
    public void Collection_IsSerialized()
    {
        var list = new[] { "one", "two" };
        var result = list.ToQueryString();

        Assert.Contains("one", result);
        Assert.Contains("two", result);
    }

    [Fact]
    public void EmptyCollection_IsIgnored()
    {
        var list = Array.Empty<string>();
        var result = list.ToQueryString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Enum_IsSerialized()
    {
        var obj = new EnumModel { Status = Status.Active };
        var result = obj.ToQueryString();
        Assert.Contains("Status=Active", result);
    }

    [Fact]
    public void DefaultValues_AreIgnored()
    {
        var obj = new SimpleModel
        {
            Id = 0,
            Name = "",
            IsActive = false,
            Created = DateTime.MinValue,
            Token = Guid.Empty
        };

        var result = obj.ToQueryString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void NullableTypes_AreHandled()
    {
        var obj = new { Value = (int?)null };
        var result = obj.ToQueryString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void DateTimeOffset_IsSerializedCorrectly()
    {
        var dto = new DateTimeOffset(2024, 12, 31, 0, 0, 0, TimeSpan.Zero);
        var obj = new { Timestamp = dto };
        var result = obj.ToQueryString();
        Assert.Contains("Timestamp=2024-12-31T00%3A00%3A00.000Z", result);
    }
}