using Microsoft.OpenApi;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests;

public class OpenApiParameterGeneratorTests
{
    [Fact]
    public void GenerateFromType_MapsScalarsAndNestedValues()
    {
        var parameters = OpenApiParameterGenerator.GenerateFromType(typeof(OpenApiRequest));

        Assert.Equal(
            [
                "Text", "Int32", "Int64", "Int16", "Single", "Double", "Boolean", "DateTime", "DateTimeOffset",
                "Guid", "Byte", "OptionalInt", "Status", "Statuses", "Numbers", "DecimalNumbers", "ChildList",
                "ChildList[].Name", "ChildList[].Rank", "IntList", "Children.Name", "Children.Rank"
            ],
            parameters.Select(parameter => parameter.Name));

        AssertParameter(parameters, "Text", JsonSchemaType.String, null, required: true);
        AssertParameter(parameters, "Int32", JsonSchemaType.Integer, "int32", required: true);
        AssertParameter(parameters, "Int64", JsonSchemaType.Integer, "int64", required: true);
        AssertParameter(parameters, "Int16", JsonSchemaType.Integer, null, required: true);
        AssertParameter(parameters, "Single", JsonSchemaType.Number, "float", required: true);
        AssertParameter(parameters, "Double", JsonSchemaType.Number, "double", required: true);
        AssertParameter(parameters, "Boolean", JsonSchemaType.Boolean, null, required: true);
        AssertParameter(parameters, "DateTime", JsonSchemaType.String, "date-time", required: true);
        AssertParameter(parameters, "DateTimeOffset", JsonSchemaType.String, "date-time", required: true);
        AssertParameter(parameters, "Guid", JsonSchemaType.String, "uuid", required: true);
        AssertParameter(parameters, "Byte", JsonSchemaType.String, null, required: true);
        AssertParameter(parameters, "OptionalInt", JsonSchemaType.Integer, "int32", required: false);
        AssertEnumParameter(parameters, "Status", required: true, "Ready", "Done");
        AssertParameter(parameters, "Statuses", JsonSchemaType.Array, null, required: true);
        AssertParameter(parameters, "Children.Name", JsonSchemaType.String, null, required: true);
        AssertEnumParameter(parameters, "Children.Rank", required: false, "Ready", "Done");
    }

    [Fact]
    public void GenerateFromType_MapsArraysAndComplexCollections()
    {
        var parameters = OpenApiParameterGenerator.GenerateFromType(typeof(OpenApiRequest));

        var numbers = AssertParameter(parameters, "Numbers", JsonSchemaType.Array, null, required: true);
        Assert.Equal(ParameterStyle.Form, numbers.Style);
        Assert.True(numbers.Explode);
        Assert.Equal(JsonSchemaType.Integer, numbers.Schema!.Items!.Type);
        Assert.Equal("int32", numbers.Schema.Items.Format);

        var decimalNumbers = AssertParameter(parameters, "DecimalNumbers", JsonSchemaType.Array, null, required: true);
        Assert.Equal(JsonSchemaType.Number, decimalNumbers.Schema!.Items!.Type);
        Assert.Equal("decimal", decimalNumbers.Schema.Items.Format);

        var statuses = AssertParameter(parameters, "Statuses", JsonSchemaType.Array, null, required: true);
        Assert.Equal(["Ready", "Done"], statuses.Schema!.Items!.Enum!.Select(value => value!.GetValue<string>()));

        var childList = AssertParameter(parameters, "ChildList", JsonSchemaType.Array, null, required: true);
        Assert.Equal(JsonSchemaType.String, childList.Schema!.Items!.Type);
        Assert.Contains(parameters, parameter => parameter.Name == "ChildList[].Name");
        Assert.Contains(parameters, parameter => parameter.Name == "ChildList[].Rank");

        var intList = AssertParameter(parameters, "IntList", JsonSchemaType.Array, null, required: true);
        Assert.Equal(JsonSchemaType.Integer, intList.Schema!.Items!.Type);
    }

    [Fact]
    public void GenerateFromType_DoesNotMapKeyPropertiesAsQueryParameters()
    {
        var parameters = OpenApiParameterGenerator.GenerateFromType(typeof(KeyOpenApiRequest));

        Assert.Equal(["Text"], parameters.Select(parameter => parameter.Name));
    }

    private static OpenApiParameter AssertParameter(
        IEnumerable<OpenApiParameter> parameters,
        string name,
        JsonSchemaType type,
        string? format,
        bool required)
    {
        var parameter = Assert.Single(parameters, parameter => parameter.Name == name);

        Assert.Equal(ParameterLocation.Query, parameter.In);
        Assert.Equal(required, parameter.Required);
        Assert.Equal(type, parameter.Schema!.Type);
        Assert.Equal(format, parameter.Schema.Format);
        return parameter;
    }

    private static void AssertEnumParameter(
        IEnumerable<OpenApiParameter> parameters,
        string name,
        bool required,
        params string[] values)
    {
        var parameter = AssertParameter(parameters, name, JsonSchemaType.String, null, required);
        Assert.Equal(values, parameter.Schema!.Enum!.Select(value => value!.GetValue<string>()));
    }

    private sealed class OpenApiRequest
    {
        public string Text { get; set; } = string.Empty;
        public int Int32 { get; set; }
        public long Int64 { get; set; }
        public short Int16 { get; set; }
        public float Single { get; set; }
        public double Double { get; set; }
        public bool Boolean { get; set; }
        public DateTime DateTime { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public Guid Guid { get; set; }
        public byte Byte { get; set; }
        public int? OptionalInt { get; set; }
        public TestStatus Status { get; set; }
        public TestStatus[] Statuses { get; set; } = [];
        public int[] Numbers { get; set; } = [];
        public decimal[] DecimalNumbers { get; set; } = [];
        public List<Child> ChildList { get; set; } = [];
        public List<int> IntList { get; set; } = [];
        public Child Children { get; set; } = new();
    }

    private sealed class Child
    {
        public string Name { get; set; } = string.Empty;
        public TestStatus? Rank { get; set; }
    }

    private sealed class KeyOpenApiRequest : IKeyRequest<int>
    {
        public int Key { get; init; }
        public string Text { get; init; } = string.Empty;
    }

    private enum TestStatus
    {
        Ready,
        Done
    }
}