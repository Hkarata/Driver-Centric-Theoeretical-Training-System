using Newtonsoft.Json;
using RSAllies.Contracts.Requests;
using RSAllies.HelperTypes;

namespace RSAllies.Services
{
    public class ApiClient(HttpClient httpClient)
    {

        public async Task<Result<bool>> CheckNames(string firstName, string middleName, string lastName)
        {
            var request = new Names { FirstName = firstName, MiddleName = middleName, LastName = lastName };
            var response = await httpClient.PostAsJsonAsync<Names>("/api/check-names", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<bool>>(content)!;
            return result;
        }

        public async Task<Result<bool>> CheckNIDA(string nida)
        {
            var response = await httpClient.PostAsJsonAsync("/api/check-nida", nida);
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
    }
}
