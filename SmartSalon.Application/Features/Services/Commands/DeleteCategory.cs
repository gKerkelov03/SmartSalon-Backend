using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class DeleteCategoryCommand : ICommand
{
    public Id CategoryId { get; set; }
}

internal class DeleteCategoryCommandHandler(IEfRepository<Category> _categories, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categories.GetByIdAsync(command.CategoryId);

        if (category is null)
        {
            return Error.NotFound;
        }

        await _categories.RemoveByIdAsync(category.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
