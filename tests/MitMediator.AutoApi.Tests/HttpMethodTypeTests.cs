using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests;

public class HttpMethodTypeTests
{
    [Theory]
    [InlineData("GetQuery", HttpMethodType.Get)]
    [InlineData("GetItemQuery", HttpMethodType.Get)]
    [InlineData("LoadItemQuery", HttpMethodType.Get)]
    [InlineData("DownloadItemQuery", HttpMethodType.Get)]
    
    [InlineData("UpdateEntryCommand", HttpMethodType.Put)]
    [InlineData("ChangeEntryCommand", HttpMethodType.Put)]
    [InlineData("EditEntryCommand", HttpMethodType.Put)]
    [InlineData("ModifyEntryCommand", HttpMethodType.Put)]
    [InlineData("PutEntryCommand", HttpMethodType.Put)]

    [InlineData("PostEntryCommand", HttpMethodType.Post)]

    [InlineData("AddItem", HttpMethodType.PostCreate)]
    [InlineData("CreateItem", HttpMethodType.PostCreate)]
    [InlineData("UploadItem", HttpMethodType.PostCreate)]
    
    [InlineData("DeleteEntry", HttpMethodType.Delete)]
    [InlineData("RemoveEntry", HttpMethodType.Delete)]
    [InlineData("DropEntry", HttpMethodType.Delete)]
    public void GetHttpMethodType_ShouldReturnExpected(string name, HttpMethodType expected)
    {
        var type = TypeFactory.CreateTypeWithName(name);
        var result = Helpers.GetHttpMethodType(type);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetHttpMethodType_UnknownName_ReturnsDefault()
    {
        var type = TypeFactory.CreateTypeWithName("Unrecognized");
        var result = Helpers.GetHttpMethodType(type);
        Assert.Equal(HttpMethodType.Get, result);
    }
}
