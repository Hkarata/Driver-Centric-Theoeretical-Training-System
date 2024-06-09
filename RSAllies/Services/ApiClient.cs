using Newtonsoft.Json;
using RSAllies.Contracts.Requests;
using RSAllies.Contracts.Responses;
using RSAllies.HelperTypes;
using RSAllies.Requests;
using RSAllies.Responses;

namespace RSAllies.Services
{
    public class ApiClient(HttpClient httpClient)
    {

        public async Task<Result<bool>> CheckNames(string firstName, string middleName, string lastName)
        {
            var request = new Names { FirstName = firstName, MiddleName = middleName, LastName = lastName };
            var response = await httpClient.PostAsJsonAsync<Names>("/api/checks/check-names", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<bool>>(content)!;
            return result;
        }

        public async Task<Result<bool>> CheckNIDA(string nida)
        {
            var response = await httpClient.GetAsync($"/api/checks/check-nida/{nida}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<bool>>(content)!;
            return result;
        }

        public async Task<Result<Guid>> AddUserData(CreateUserDto request)
        {
            var response = await httpClient.PostAsJsonAsync<CreateUserDto>("/api/user", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<Guid>>(content)!;
            return result;
        }

        public async Task<Result<AccountCheckResult>> CheckAccount(string phone, string email)
        {
            var request = new AccountDto { PhoneNumber = phone, Email = email };
            var response = await httpClient.PostAsJsonAsync<AccountDto>("/api/checks/check-account", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<AccountCheckResult>>(content)!;
            return result;
        }

        public async Task<Result<Contracts.Responses.AdminDto>> AdminLogin(AdminLogin request)
        {
            var response = await httpClient.PostAsJsonAsync<AdminLogin>("/api/admin/authenticate", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<Contracts.Responses.AdminDto>>(content)!;
            return result;
        }

        public async Task<Result> CreateAccount(CreateAccountDto request)
        {
            var response = await httpClient.PostAsJsonAsync<CreateAccountDto>("/api/account", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result<UserDto>> AuthenticateUser(AuthenticateDto request)
        {
            var response = await httpClient.PostAsJsonAsync<AuthenticateDto>("/api/authenticate", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<UserDto>>(content)!;
            return result;
        }

        public async Task<Result<BookingDto>> GetCurrentUserBooking(Guid userId)
        {
            var response = await httpClient.GetAsync($"/api/booking/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<BookingDto>>(content)!;
            return result;
        }

        public async Task<Result> CreateAdmin(Contracts.Requests.AdminDto admin)
        {
            var response = await httpClient.PostAsJsonAsync("/api/admin", admin);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> CreateVenue(CreateVenueDto venue)
        {
            var response = await httpClient.PostAsJsonAsync<CreateVenueDto>("/api/venue", venue);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result<List<SVenueDto>>> GetVenues()
        {
            var response = await httpClient.GetAsync("/api/venues");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<SVenueDto>>>(content)!;
            return result;
        }

        public async Task<Result<VenueDto>> GetVenue(Guid id)
        {
            var response = await httpClient.GetAsync($"/api/venue/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<VenueDto>>(content)!;
            return result;
        }

        public async Task<Result> CreateSession(CreateSessionDto session)
        {
            var response = await httpClient.PostAsJsonAsync<CreateSessionDto>("/api/session", session);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result<List<Admin>>> GetAdmins()
        {
            var response = await httpClient.GetAsync("/api/admins");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<Admin>>>(content)!;
            return result;
        }

        public async Task<Result<Admin>> GetAdmin(Guid id)
        {
            var response = await httpClient.GetAsync($"/api/admin/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<Admin>>(content)!;
            return result;
        }
    }
}