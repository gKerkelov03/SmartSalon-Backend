using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Queries;

public class CreateCommand : ICommand<CreateCommandResponse>
{
    public required string Name { get; set; }

    public required int Age { get; set; }
}