using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class CreateCategoryCommand : ICommand<CreateCategoryCommandResponse>, IMapTo<Category>
{
    public required string Name { get; set; }
    public Id SectionId { get; set; }
}

public class CreateCategoryCommandResponse(Id id)
{
    public Id CreatedCategoryId => id;
}

internal class CreateCategoryCommandHandler(
    IEfRepository<Category> _categories,
    IEfRepository<Section> _sections,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
)
    : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var newCategory = _mapper.Map<Category>(command);

        var section = await _sections.All
            .Include(section => section.Categories)
            .Where(section => section.Id == command.SectionId)
            .FirstOrDefaultAsync();

        if (section is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.GetByIdAsync(section.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        var sectionAlreadyContainsCategory = section.Categories!.Any(category => category.Name == newCategory.Name);

        if (sectionAlreadyContainsCategory)
        {
            return Error.Conflict;
        }

        var atTheEndOfTheList = salon.Categories!.MaxBy(category => category.Order)!.Order + 1;
        newCategory.Order = atTheEndOfTheList;

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //salon.Categories!.Add(newService);
        await _categories.AddAsync(newCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse(newCategory.Id);
    }
}
