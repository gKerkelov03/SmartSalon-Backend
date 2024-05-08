
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartCategory.Application.Features.Services.Commands;

public class UpdateCategoryCommand : ICommand
{
    public Id CategoryId { get; set; }
    public required string Name { get; set; }
    public int Order { get; set; }
}

internal class UpdateCategoryCommandHandler(IEfRepository<Category> _categories, IUnitOfWork _unitOfWork) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categories.All
            .Include(category => category.Section)
            .ThenInclude(section => section!.Categories)
            .FirstOrDefaultAsync(category => category.Id == command.CategoryId);

        if (category is null)
        {
            return Error.NotFound;
        }

        //TODO: check all update handlers if they check for the conflict case
        var sectionAlreadyContainsCategory = category.Section!.Categories!.Any(category => category.Name == command.Name);

        if (sectionAlreadyContainsCategory)
        {
            return Error.Conflict;
        }

        category.MapAgainst(command);
        _categories.Update(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
