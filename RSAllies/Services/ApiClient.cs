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
            var response = await httpClient.PostAsJsonAsync("/api/checks/check-nida", nida);
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

        public async Task<Result<AdminDto>> AdminLogin(AdminLogin request)
        {
            var response = await httpClient.PostAsJsonAsync<AdminLogin>("/api/admin", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<AdminDto>>(content)!;
            return result;
        }
    }
}
