using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class DeleteSectionCommand(Id id) : ICommand
{
    public Id SectionId => id;
}

internal class DeleteSectionCommandHandler(IEfRepository<Section> _sections, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteSectionCommand>
{
    public async Task<Result> Handle(DeleteSectionCommand command, CancellationToken cancellationToken)
    {
        var section = await _sections.GetByIdAsync(command.SectionId);

        if (section is null)
        {
            return Error.NotFound;
        }

        await _sections.RemoveByIdAsync(section.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
