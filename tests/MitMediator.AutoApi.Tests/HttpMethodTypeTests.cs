using MitMediator.AutoApi.Abstractions;
namespace MitMediator.AutoApi.Tests;

public class HttpMethodTypeTests
{
    [Theory]
    [InlineData("GetQuery", MethodType.Get)]
    [InlineData("GetItemQuery", MethodType.Get)]
    [InlineData("LoadItemQuery", MethodType.Get)]
    [InlineData("DownloadItemQuery", MethodType.Get)]
    
    [InlineData("UpdateEntryCommand", MethodType.Put)]
    [InlineData("ChangeEntryCommand", MethodType.Put)]
    [InlineData("EditEntryCommand", MethodType.Put)]
    [InlineData("ModifyEntryCommand", MethodType.Put)]
    [InlineData("PutEntryCommand", MethodType.Put)]

    [InlineData("PostEntryCommand", MethodType.Post)]
    [InlineData("UploadItem", MethodType.Post)]
    [InlineData("ImportItem", MethodType.Post)]
    
    [InlineData("AddItem", MethodType.PostCreate)]
    [InlineData("CreateItem", MethodType.PostCreate)]
    
    [InlineData("DeleteEntry", MethodType.Delete)]
    [InlineData("RemoveEntry", MethodType.Delete)]
    [InlineData("DropEntry", MethodType.Delete)]
    public void GetHttpMethodType_ShouldReturnExpected(string name, MethodType expected)
    {
        var type = TypeFactory.CreateTypeWithName(name);
        var info = new RequestInfo(type);
        Assert.Equal(expected, info.MethodType);
    }

    [Fact]
    public void GetHttpMethodType_UnknownName_ReturnsDefault()
    {
        var type = TypeFactory.CreateTypeWithName("Unrecognized");
        var info = new RequestInfo(type);
        Assert.Equal(MethodType.Get, info.MethodType);
    }
}
