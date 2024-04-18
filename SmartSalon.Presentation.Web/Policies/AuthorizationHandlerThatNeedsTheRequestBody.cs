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
        using var requestBodyReader = new StreamReader(httpContextAccessor.HttpContext!.Request.Body, Encoding.UTF8);
        var body = await requestBodyReader.ReadToEndAsync();

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
    }
}