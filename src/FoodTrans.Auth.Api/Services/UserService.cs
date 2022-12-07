using System.Security.Claims;

namespace Api.Services;

internal sealed class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        Guid result = Guid.Empty;

        if (_httpContextAccessor.HttpContext is not null)
        {
            var uid = _httpContextAccessor.HttpContext.User.FindFirstValue("uid");
            result = Guid.Parse(uid);
        }

        return result;
    }

    public string GetUsername()
    {
        string result = string.Empty;

        if (_httpContextAccessor.HttpContext is not null)
        {
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        return result;
    }
}