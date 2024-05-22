using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class DeleteImageCommand : ICommand
{
    public Id ImageId { get; set; }
}

internal class DeleteImageCommandHandler(IEfRepository<Image> _images, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteImageCommand>
{
    public async Task<Result> Handle(DeleteImageCommand command, CancellationToken cancellationToken)
    {
        var image = await _images.GetByIdAsync(command.ImageId);

        if (image is null)
        {
            return Error.NotFound;
        }

        await _images.RemoveByIdAsync(image.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
