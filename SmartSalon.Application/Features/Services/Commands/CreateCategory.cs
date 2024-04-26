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
    public Id SalonId { get; set; }
    public Id? SectionId { get; set; }
}

public class CreateCategoryCommandResponse(Id id)
{
    public Id CreatedCategoryId => id;
}

internal class CreateCategoryCommandHandler(
    IEfRepository<Category> _categories,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
)
    : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        //TODO: set order last
        var newCategory = _mapper.Map<Category>(command);

        var salon = await _salons.All
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsCategory = salon.Categories!.Any();

        if (salonAlreadyContainsCategory)
        {
            return Error.Conflict;
        }

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //salon.Categories!.Add(newService);

        await _categories.AddAsync(newCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse(newCategory.Id);
    }
}
