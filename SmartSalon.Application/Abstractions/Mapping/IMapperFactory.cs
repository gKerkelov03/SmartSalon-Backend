using System.Reflection;
using AutoMapper;

namespace SmartSalon.Application.Abstractions.Mapping;

public interface IMapperFactory
{
    IMapper CreateMapper(params Assembly[] assemblies);
}