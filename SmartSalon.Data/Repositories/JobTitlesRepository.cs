using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Data.Repositories;

public class JobTitlesRepository : Repository<JobTitle>, IJobTitlesRepository
{
    public JobTitlesRepository(SmartSalonDbContext _dbContext) : base(_dbContext) { }

    public Result<IEnumerable<JobTitle>> GetJobTitlesInSalon(Id salonId, IEnumerable<Id> jobTitlesIds)
    {
        var jobTitlesFound = _dbSet
            .Where(jobTitle => jobTitle.SalonId == salonId && jobTitlesIds.Contains(jobTitle.Id))
            .ToList();

        foreach (var jobTitleId in jobTitlesIds)
        {
            var jobTitleNotFound = !jobTitlesFound.Any(jobTitle => jobTitle.Id == jobTitleId);

            if (jobTitleNotFound)
            {
                return Result<IEnumerable<JobTitle>>.Failure(new Error("Some of the jobTitles don't belong to this salon or don't even exist"));
            }
        };

        return jobTitlesFound;
    }
}