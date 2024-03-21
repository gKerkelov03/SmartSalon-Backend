using AutoMapper;

namespace SmartSalon.Application.Mapping;

public interface IHaveCustomMappings
{
    void CreateMappings(IProfileExpression config);
}
