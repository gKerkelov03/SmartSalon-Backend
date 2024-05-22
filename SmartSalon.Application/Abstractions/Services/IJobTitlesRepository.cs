
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.ResultObject;

public interface IJobTitlesRepository : IEfRepository<JobTitle>, IScopedLifetime
{
    Result<IEnumerable<JobTitle>> GetJobTitlesInSalon(Id salonId, IEnumerable<Id> jobTitlesIds);
}