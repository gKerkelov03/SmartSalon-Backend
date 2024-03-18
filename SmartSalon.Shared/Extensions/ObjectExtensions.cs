
namespace SmartSalon.Shared.Extensions;

public static class ObjectExtensions
{
    public static TTarget CastTo<TTarget>(this object objectToCast)
        => (TTarget)objectToCast;
}