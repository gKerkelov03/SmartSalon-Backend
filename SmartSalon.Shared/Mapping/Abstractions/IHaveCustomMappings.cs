using AutoMapper;

namespace SmartSalon.Shared.Mapping.Abstractions;

public interface IHaveCustomMappings
{
    void CreateMappings(IProfileExpression config);
}
