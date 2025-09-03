using System.Reflection;
using System.Reflection.Emit;

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
}