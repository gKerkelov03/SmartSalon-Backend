using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Application.Commands;

public class RegisterCommand : ICommand<RegisterCommandResponse>, IMapTo<Profile>
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
