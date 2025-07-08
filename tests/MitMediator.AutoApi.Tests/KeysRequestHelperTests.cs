using System.Reflection;
using MitMediator.AutoApi.Abstractions;
using RequestsForTests;

namespace MitMediator.AutoApi.Tests;

public class KeysRequestHelperTests
{
    [Fact]
    public void GetKeysCount_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKeysCount), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"Invalid number of generic arguments for type String. Type must implement IKeyRequest<> interface.", ex.InnerException.Message);
    }

    [Fact]
    public void GetKeyType_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKeyType), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<> interface.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKey2Type_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKey2Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,> interface.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKey3Type_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKey3Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,> interface.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKey4Type_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKey4Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,,> interface.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKey5Type_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKey5Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,,,> interface.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKey6Type_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKey6Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,,,,> interface.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKey7Type_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKey7Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,,,,,> interface.", ex.InnerException.Message);
    }

    [Fact]
    public void GetKeyPattern_MustThrowException_WhenCustomPatternIncorrect_ForOneKey()
    {
        // Arrange
        var queryType =  typeof(TestGetByKeyQuery);
        var attr = new GetByKeyAttribute("test", null, null, "incorrectPattern");
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKeyPattern), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { attr, queryType }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"Custom pattern must contain '{{key}}'.", ex.InnerException.Message);
    }
    
    [Fact]
    public void GetKeyPattern_MustThrowException_WhenCustomPatternIncorrect_ForManyKeys()
    {
        // Arrange
        var queryType =  typeof(TestGetByKey2Query);
        var attr = new GetByKeyAttribute("test", null, null, "incorrectPattern");
        var method = typeof(KeysRequestHelper)
            .GetMethod(nameof(KeysRequestHelper.GetKeyPattern), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { attr, queryType }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal("Custom pattern must contain '{key1}'.", ex.InnerException.Message);
    }
}