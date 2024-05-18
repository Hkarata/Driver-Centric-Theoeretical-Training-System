using Newtonsoft.Json;
using RSAllies.Contracts.Responses;

namespace RSAllies.Services;

public class SessionChecker(IHttpContextAccessor httpContextAccessor)
{
    public bool Check()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        return !string.IsNullOrEmpty(session);
    }

    public UserDto? GetUserData()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        var data = JsonConvert.DeserializeObject<UserDto>(session!);
        return data;
    }
}