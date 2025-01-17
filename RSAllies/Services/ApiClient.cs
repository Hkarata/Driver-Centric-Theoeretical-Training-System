﻿using Newtonsoft.Json;
using RSAllies.Contracts.Requests;
using RSAllies.Contracts.Responses;
using RSAllies.HelperTypes;
using RSAllies.Models;
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

        public async Task<Result<List<UserData>>> GetSessionUsers(Guid sessionId)
        {
            var response = await httpClient.GetAsync($"/api/sessions/{sessionId}/users");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<UserData>>>(content)!;
            return result;
        }

        public async Task<Result<List<ASessionDto>>> GetSessionsAsync(CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync("/api/sessions", cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<Result<List<ASessionDto>>>(content)!;
            return result;
        }

        public async Task<Result<List<ASessionDto>>> GetSessionByRegionAndDate(string regionId)
        {
            var response = await httpClient.GetAsync($"/api/sessions/{regionId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<ASessionDto>>>(content)!;
            return result;
        }

        public async Task<Result<List<ASessionDto>>> GetSessionByDate(DateTime date)
        {
            var x = date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var response = await httpClient.GetAsync($"/api/sessions/{x}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<ASessionDto>>>(content)!;
            return result;
        }

        public async Task<Result> CreateBooking(CreateBookingDto booking)
        {
            var response = await httpClient.PostAsJsonAsync<CreateBookingDto>("/api/booking", booking);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> ConfirmBooking(string bookingId)
        {
            var response = await httpClient.GetAsync($"/api/booking/{bookingId}/confirm");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> CreateQuestion(CreateQuestionDto question)
        {
            var response = await httpClient.PostAsJsonAsync<CreateQuestionDto>("/api/question", question);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result<List<QuestionDto>>> GetEnglishQuestions()
        {
            var response = await httpClient.GetAsync("/api/questions/english");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<QuestionDto>>>(content)!;
            return result;
        }

        public async Task<Result<List<QuestionDto>>> GetSwahiliQuestions()
        {
            var response = await httpClient.GetAsync("/api/questions/swahili");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<QuestionDto>>>(content)!;
            return result;
        }

        public async Task<Result<TimeSpan>> CheckBooking(string userId)
        {
            var response = await httpClient.GetAsync($"/api/check-booking/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<TimeSpan>>(content)!;
            return result;
        }

        public async Task<Result<List<BookingDto>>> GetUserBookings(Guid userId)
        {
            var response = await httpClient.GetAsync($"/api/user-bookings/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<BookingDto>>>(content)!;
            return result;
        }

        public async Task<Result> DeleteBooking(string bookingId)
        {
            var response = await httpClient.DeleteAsync($"/api/booking/{bookingId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> UpdatePassword(UpdatePasswordDto request)
        {
            var response = await httpClient.PostAsJsonAsync<UpdatePasswordDto>("/api/admin/update-password", request);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> ActivateAdmin(string userId)
        {
            var response = await httpClient.GetAsync($"/api/admin/activate/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> DeactivateAdmin(string userId)
        {
            var response = await httpClient.GetAsync($"/api/admin/deactivate/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result> DeleteQuestion(string questionId)
        {
            var response = await httpClient.DeleteAsync($"/api/question/{questionId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<Result<Question>> GetQuestion(string questionId)
        {
            var response = await httpClient.GetAsync($"/api/question/{questionId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<Question>>(content)!;
            return result;
        }

        public async Task<Result<List<Question>>> GetAllQuestions()
        {
            var response = await httpClient.GetAsync("/api/questions");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<List<Question>>>(content)!;
            return result;
        }

        public async Task<Result> EditQuestion(string questionId, CreateQuestionDto questionDto)
        {
            var response = await httpClient.PutAsJsonAsync<CreateQuestionDto>($"/api/question/{questionId}", questionDto);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<object>>(content)!;
            return result;
        }

        public async Task<HttpResponseMessage> PostUserResponses(UserResponseDto userResponse)
		{
			var response = await httpClient.PostAsJsonAsync<UserResponseDto>("/api/user-responses", userResponse);
			return response;
		}

        public async Task<Result<ScoreDto>> GetUserScore(Guid userId)
        {
            var response = await httpClient.GetAsync($"/api/score/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<ScoreDto>>(content)!;
            return result;
        }

        public async Task DeleteSession(Guid sessionId)
        {
            await httpClient.DeleteAsync($"/api/session/{sessionId}");
        }
	}
}