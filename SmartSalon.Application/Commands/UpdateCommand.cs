using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Commands;

public class UpdateCommand : ICommand
{
    public required Id Id { get; set; }

    public required string Name { get; set; }

    public required int Age { get; set; }
}