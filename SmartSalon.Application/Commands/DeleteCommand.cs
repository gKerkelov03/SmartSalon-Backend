using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Commands;

public class DeleteCommand : ICommand
{
    public required Id Id { get; set; }
}