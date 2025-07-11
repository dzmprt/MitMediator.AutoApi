using System.Reflection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests;

public class KeysRequestHelperTests
{
    [Fact]
    public void GetKeysCount_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKeysCount), BindingFlags.Public | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal("Invalid number of generic arguments for type String. Type must implement IKeyRequest<> interface.", ex.InnerException.Message);
    }

    [Fact]
    public void GetKeyType_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKeyType), BindingFlags.Public | BindingFlags.Static)!;
        
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
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKey2Type), BindingFlags.Public | BindingFlags.Static)!;
        
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
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKey3Type), BindingFlags.Public | BindingFlags.Static)!;
        
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
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKey4Type), BindingFlags.Public | BindingFlags.Static)!;
        
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
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKey5Type), BindingFlags.Public | BindingFlags.Static)!;
        
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
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKey6Type), BindingFlags.Public | BindingFlags.Static)!;
        
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
        var method = typeof(RequestHelper)
            .GetMethod(nameof(RequestHelper.GetKey7Type), BindingFlags.Public | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,,,,,> interface.", ex.InnerException.Message);
    }
}