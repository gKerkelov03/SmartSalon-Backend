using System.Reflection;

namespace SmartSalon.Application.Extensions;

public static class TypeExtensions
{
    public static Assembly GetAssembly(this Type type)
        => type.GetTypeInfo().Assembly;

    public static bool IsNotAbsctractOrInterface(this Type type)
        => !type
            .GetTypeInfo()
            .IsAbstract &&

           !type
            .GetTypeInfo()
            .IsInterface;

    public static bool IsBaseTypeOf<TDerived>(this Type type, TDerived derived)
     => type
        .GetTypeInfo()
        .IsAssignableFrom(derived?.GetType());

}