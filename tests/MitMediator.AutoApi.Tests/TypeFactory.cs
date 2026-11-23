using System.Reflection;
using System.Reflection.Emit;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests;

public static class TypeFactory
{
    public static Type CreateTypeWithName(string name)
    {
        var typeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(new AssemblyName("DynamicAssembly"), AssemblyBuilderAccess.Run)
            .DefineDynamicModule("Main")
            .DefineType(name, TypeAttributes.Public);
            
        typeBuilder.AddInterfaceImplementation(typeof(IRequest<int>));

        return typeBuilder.CreateTypeInfo();
    }

    public static Type CreateApiRequestType(string name, int keysCount, bool isFileRequest = false)
    {
        if (keysCount is < 0 or > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(keysCount));
        }

        var assemblyName = new AssemblyName($"DynamicAssembly.{Guid.NewGuid():N}");
        if (isFileRequest && keysCount > 0)
        {
            throw new ArgumentException(
                "File requests with keys must be declared in source to implement init-only key contracts.",
                nameof(isFileRequest));
        }

        var baseType = keysCount == 0
            ? typeof(object)
            : keysCount switch
            {
                1 => typeof(KeyRequest<>).MakeGenericType(typeof(int)),
                2 => typeof(KeyRequest<,>).MakeGenericType(typeof(int), typeof(int)),
                3 => typeof(KeyRequest<,,>).MakeGenericType(typeof(int), typeof(int), typeof(int)),
                4 => typeof(KeyRequest<,,,>).MakeGenericType(typeof(int), typeof(int), typeof(int), typeof(int)),
                5 => typeof(KeyRequest<,,,,>).MakeGenericType(
                    typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)),
                6 => typeof(KeyRequest<,,,,,>).MakeGenericType(
                    typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)),
                7 => typeof(KeyRequest<,,,,,,>).MakeGenericType(
                    typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)),
                _ => throw new ArgumentOutOfRangeException(nameof(keysCount))
            };
        var typeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run)
            .DefineDynamicModule("Main")
            .DefineType(
                name,
                TypeAttributes.Public | TypeAttributes.Class,
                isFileRequest ? typeof(FileRequest) : baseType);

        typeBuilder.AddInterfaceImplementation(typeof(IRequest<int>));
        typeBuilder.SetCustomAttribute(
            new CustomAttributeBuilder(
                typeof(TagAttribute).GetConstructor([typeof(string)])!,
                ["matrix"]));
        typeBuilder.SetCustomAttribute(
            new CustomAttributeBuilder(
                typeof(SuffixAttribute).GetConstructor([typeof(string)])!,
                [""]));

        return typeBuilder.CreateTypeInfo()!;
    }

    public static Type CreateSingleKeyApiRequestType(string name, Type keyType)
    {
        var typeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(
                new AssemblyName($"DynamicAssembly.{Guid.NewGuid():N}"),
                AssemblyBuilderAccess.Run)
            .DefineDynamicModule("Main")
            .DefineType(
                name,
                TypeAttributes.Public | TypeAttributes.Class,
                typeof(KeyRequest<>).MakeGenericType(keyType));

        typeBuilder.AddInterfaceImplementation(typeof(IRequest<int>));
        typeBuilder.SetCustomAttribute(
            new CustomAttributeBuilder(
                typeof(TagAttribute).GetConstructor([typeof(string)])!,
                ["matrix"]));
        typeBuilder.SetCustomAttribute(
            new CustomAttributeBuilder(
                typeof(SuffixAttribute).GetConstructor([typeof(string)])!,
                [""]));

        return typeBuilder.CreateTypeInfo()!;
    }
}