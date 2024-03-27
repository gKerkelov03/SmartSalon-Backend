using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;

namespace SmartSalon.Application.Commands;

public class CreateCommand : ICommand<CreateCommandResponse>
{
    public required string Name { get; set; }

    public required int Age { get; set; }
}