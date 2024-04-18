using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public abstract class AuthorizationHandlerThatNeedsTheRequestBody
{
    protected IAuthorizationRequirement? GetRequirement<TRequirement>(AuthorizationHandlerContext context)
        => context.PendingRequirements.FirstOrDefault(requirement => requirement is TRequirement);

    protected async Task<IDictionary<string, string>?> GetRequestBodyMapAsync(IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext!.Request;
        request.EnableBuffering();

        using var requestBodyReader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await requestBodyReader.ReadToEndAsync();
        request.Body.Position = 0;

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
    }
}