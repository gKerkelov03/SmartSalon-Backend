using AutoMapper;

namespace SmartSalon.Application.Abstractions.Mapping;

public interface IHaveCustomMappings
{
    void CreateMappings(IProfileExpression config);
}
