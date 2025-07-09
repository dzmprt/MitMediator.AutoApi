using System.Reflection;
using MitMediator.AutoApi.Abstractions;
using RequestsForTests;
using RequestsForTests.Test.Queries.GetByKey;
using RequestsForTests.Test.Queries.GetEmpty;

namespace MitMediator.AutoApi.Tests;

public class KeysRequestHelperTests
{
    [Fact]
    public void GetKeysCount_MustThrowException_WhenIncorrectType()
    {
        // Arrange
        var type = typeof(string);
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKeysCount), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKeyType), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKey2Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKey3Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKey4Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKey5Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKey6Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
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
        var method = typeof(Helpers)
            .GetMethod(nameof(Helpers.GetKey7Type), BindingFlags.NonPublic | BindingFlags.Static)!;
        
        // Act & Assert
        var ex = Assert.Throws<TargetInvocationException>(() =>
            method.Invoke(null, new object[] { type }));
        
        Assert.NotNull(ex.InnerException);
        Assert.Equal($"{type.Name} must implement IKeyRequest<,,,,,,> interface.", ex.InnerException.Message);
    }
}