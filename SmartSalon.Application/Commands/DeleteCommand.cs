using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Queries;

public class DeleteCommand : ICommand
{
    public Id Id { get; set; }

    public DeleteCommand(Id id)
        => Id = id;
}