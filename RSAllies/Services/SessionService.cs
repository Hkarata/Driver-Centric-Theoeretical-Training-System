using Newtonsoft.Json;
using RSAllies.Contracts.Responses;

namespace RSAllies.Services;

/// <summary>
/// Service for managing user sessions.
/// </summary>
public class SessionService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionService"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Checks if a user session exists.
    /// </summary>
    /// <returns><c>true</c> if a user session exists; otherwise, <c>false</c>.</returns>
    public bool Check()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        return !string.IsNullOrEmpty(session);
    }

    /// <summary>
    /// Checks if an admin session exists.
    /// </summary>
    /// <returns><c>true</c> if an admin session exists; otherwise, <c>false</c>.</returns>
    public bool CheckAdmin()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("AdminSession");
        return !string.IsNullOrEmpty(session);
    }

    /// <summary>
    /// Gets the user data from the session.
    /// </summary>
    /// <returns>The user data if it exists; otherwise, <c>null</c>.</returns>
    public UserDto? GetUserData()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("UserSession");
        var data = JsonConvert.DeserializeObject<UserDto>(session!);
        return data;
    }

    /// <summary>
    /// Sets the user data in the session.
    /// </summary>
    /// <param name="user">The user data to set.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task SetUserData(UserDto user)
    {
        Clear();
        ClearAdmin();
        var data = JsonConvert.SerializeObject(user);
        httpContextAccessor.HttpContext?.Session.SetString("UserSession", data);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Clears the user session.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task Clear()
    {
        httpContextAccessor.HttpContext?.Session.Remove("UserSession");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Clears the admin session.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task ClearAdmin()
    {
        httpContextAccessor.HttpContext?.Session.Remove("AdminSession");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Sets the admin data in the session.
    /// </summary>
    /// <param name="admin">The admin data to set.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task SetAdminData(AdminDto admin)
    {
        Clear();
        ClearAdmin();
        var data = JsonConvert.SerializeObject(admin);
        httpContextAccessor.HttpContext?.Session.SetString("AdminSession", data);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the user ID from the session.
    /// </summary>
    /// <returns>The user ID if it exists; otherwise, <see cref="Guid.Empty"/>.</returns>
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

    public Guid GetAdminId()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("AdminSession");
        if (session is null)
        {
            return Guid.Empty;
        }
        var data = JsonConvert.DeserializeObject<AdminDto>(session!);
        return data!.Id;
    }

    public bool IsSuperUser()
    {
        var session = httpContextAccessor.HttpContext?.Session.GetString("AdminSession");
        if (session is null)
        {
            return false;
        }
        var data = JsonConvert.DeserializeObject<AdminDto>(session!);
        return data!.Role == "SuperUser";
    }
}
