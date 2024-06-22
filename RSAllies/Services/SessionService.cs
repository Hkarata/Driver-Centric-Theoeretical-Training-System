using Newtonsoft.Json;
using RSAllies.Contracts.Responses;

namespace RSAllies.Services;

public class SessionService(IHttpContextAccessor httpContextAccessor)
{
    public bool Check()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        return !string.IsNullOrEmpty(session);
    }

    public bool CheckAdmin()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("AdminSession");
        return !string.IsNullOrEmpty(session);
    }

    public UserDto? GetUserData()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        var data = JsonConvert.DeserializeObject<UserDto>(session!);
        return data;
    }

    public Task SetUserData(UserDto user)
    {
        var data = JsonConvert.SerializeObject(user);
        httpContextAccessor.HttpContext?.Session.SetString("UserSession", data);
        return Task.CompletedTask;
    }

    public Task Clear()
    {
        httpContextAccessor.HttpContext?.Session.Remove("UserSession");
        return Task.CompletedTask;
    }

    public Task SetAdminData(AdminDto admin)
    {
        var data = JsonConvert.SerializeObject(admin);
        httpContextAccessor.HttpContext?.Session.SetString("AdminSession", data);
        return Task.CompletedTask;
    }

    public Guid GetUserId()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        if (session is null)
        {
            return Guid.Empty;
        }
        var data = JsonConvert.DeserializeObject<UserDto>(session!);
        return data!.Id;
    }
}