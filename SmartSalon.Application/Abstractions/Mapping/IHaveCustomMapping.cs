using AutoMapper;

namespace SmartSalon.Application.Abstractions.Mapping;

public interface IHaveCustomMapping
{
    void CreateMapping(IProfileExpression config);
}
