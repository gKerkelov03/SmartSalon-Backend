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
    public Id SalonId { get; set; }
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
        var salonDoesntExist = await _salons.GetByIdAsync(command.SalonId) is null;

        if (salonDoesntExist)
        {
            return Error.NotFound;
        }

        var newCategory = _mapper.Map<Category>(command);
        var section = await _sections.All
            .Include(section => section.Categories)
            .FirstOrDefaultAsync(section => section.Id == command.SectionId);

        if (section is null)
        {
            return Error.NotFound;
        }

        var sectionAlreadyContainsCategory = section.Categories!.Any(category => category.Name == newCategory.Name);

        if (sectionAlreadyContainsCategory)
        {
            return Error.Conflict;
        }

        var orderAtTheEndOfTheList = await _categories.All.FirstOrDefaultAsync() is not null
            ? _categories.All.Max(category => category.Order) + 1
            : 1;

        newCategory.Order = orderAtTheEndOfTheList;

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //salon.Categories!.Add(newCategory);

        await _categories.AddAsync(newCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse(newCategory.Id);
    }
}
