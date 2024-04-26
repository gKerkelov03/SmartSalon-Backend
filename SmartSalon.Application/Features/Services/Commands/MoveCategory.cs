
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartCategory.Application.Features.Services.Commands;

public class MoveCategoryCommand : ICommand
{
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
    public Id? SectionId { get; set; }
}

internal class MoveCategoryCommandHandler(IEfRepository<Category> _categories, IUnitOfWork _unitOfWork)
    : ICommandHandler<MoveCategoryCommand>
{
    public async Task<Result> Handle(MoveCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categories.GetByIdAsync(command.CategoryId);

        if (category is null)
        {
            return Error.NotFound;
        }

        category.MapAgainst(command);
        _categories.Update(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
