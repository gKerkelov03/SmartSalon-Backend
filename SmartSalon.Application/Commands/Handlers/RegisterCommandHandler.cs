using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class RegisterCommandHandler(ProfileManager _profileManager)
    : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var profileToCreate = command.MapTo<Profile>();
        var identityResult = await _profileManager.CreateAsync(profileToCreate);

        if (!identityResult.Succeeded)
        {
            return Error.Conflict(identityResult.GetErrorMessage());
        }

        return new RegisterCommandResponse() { Id = profileToCreate.Id };
    }
}
