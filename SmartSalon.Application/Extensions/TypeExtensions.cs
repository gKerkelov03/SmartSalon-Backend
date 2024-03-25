using System.Reflection;

namespace SmartSalon.Application.Extensions;

public static class TypeExtensions
{
    public static bool IsNotAbsctractOrInterface(this Type type)
        => !type.IsAbstract && !type.IsInterface;
}