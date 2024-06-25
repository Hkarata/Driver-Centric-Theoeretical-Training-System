using Newtonsoft.Json;
using RSAllies.Analytics.Contracts;
using RSAllies.HelperTypes;

namespace RSAllies.Analytics
{
    /// <summary>
    /// Represents an API service for retrieving analytics data.
    /// </summary>
    public class ApiService(HttpClient httpClient)
    {
        /// <summary>
        /// Retrieves the age groups.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the age groups.</returns>
        public async Task<Result<List<AgeGroupDto>>> GetAgeGroups()
        {
            var response = await httpClient.GetAsync("/api/age-groups");
            var content = await response.Content.ReadAsStringAsync();
            var ageGroups = JsonConvert.DeserializeObject<Result<List<AgeGroupDto>>>(content)!;
            return ageGroups;
        }

        /// <summary>
        /// Retrieves the average sessions.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the average sessions.</returns>
        public async Task<Result<List<AverageSessionDto>>> GetAverageSessions()
        {
            var response = await httpClient.GetAsync("/api/average-sessions");
            var content = await response.Content.ReadAsStringAsync();
            var averageSessions = JsonConvert.DeserializeObject<Result<List<AverageSessionDto>>>(content)!;
            return averageSessions;
        }

        /// <summary>
        /// Retrieves the booking rate.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the booking rate.</returns>
        public async Task<Result<List<BookingRateDto>>> GetBookingRate()
        {
            var response = await httpClient.GetAsync("/api/booking-rate");
            var content = await response.Content.ReadAsStringAsync();
            var bookingRate = JsonConvert.DeserializeObject<Result<List<BookingRateDto>>>(content)!;
            return bookingRate;
        }

        /// <summary>
        /// Retrieves the booking status counts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the booking status counts.</returns>
        public async Task<Result<List<BookingStatusCountDto>>> GetBookingStatusCounts()
        {
            var response = await httpClient.GetAsync("/api/booking-status-counts");
            var content = await response.Content.ReadAsStringAsync();
            var bookingStatusCounts = JsonConvert.DeserializeObject<Result<List<BookingStatusCountDto>>>(content)!;
            return bookingStatusCounts;
        }

        /// <summary>
        /// Retrieves the cancellation rates.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the cancellation rates.</returns>
        public async Task<Result<List<CancellationRateDto>>> GetCancellationRates()
        {
            var response = await httpClient.GetAsync("/api/cancellation-rates");
            var content = await response.Content.ReadAsStringAsync();
            var cancellationRates = JsonConvert.DeserializeObject<Result<List<CancellationRateDto>>>(content)!;
            return cancellationRates;
        }

        /// <summary>
        /// Retrieves the confirmation rates.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the confirmation rates.</returns>
        public async Task<Result<List<ConfirmationRateDto>>> GetConfirmationRates()
        {
            var response = await httpClient.GetAsync("/api/confirmation-rates");
            var content = await response.Content.ReadAsStringAsync();
            var confirmationRates = JsonConvert.DeserializeObject<Result<List<ConfirmationRateDto>>>(content)!;
            return confirmationRates;
        }

        /// <summary>
        /// Retrieves the education level count.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the education level count.</returns>
        public async Task<Result<List<EducationLevelDto>>> GetEducationLevelCount()
        {
            var response = await httpClient.GetAsync("/api/education-level-count");
            var content = await response.Content.ReadAsStringAsync();
            var educationLevelCounts = JsonConvert.DeserializeObject<Result<List<EducationLevelDto>>>(content)!;
            return educationLevelCounts;
        }

        /// <summary>
        /// Retrieves the gender count.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the gender count.</returns>
        public async Task<Result<List<GenderDto>>> GetGenderCount()
        {
            var response = await httpClient.GetAsync("/api/gender-count");
            var content = await response.Content.ReadAsStringAsync();
            var genderCounts = JsonConvert.DeserializeObject<Result<List<GenderDto>>>(content)!;
            return genderCounts;
        }

        /// <summary>
        /// Retrieves the gender test analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the gender test analysis.</returns>
        public async Task<Result<GenderTestDto>> GetGenderTestAnalysis()
        {
            var response = await httpClient.GetAsync("/api/gender-test-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var genderTestAnalysis = JsonConvert.DeserializeObject<Result<GenderTestDto>>(content)!;
            return genderTestAnalysis;
        }

        /// <summary>
        /// Retrieves the least popular venues.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the least popular venues.</returns>
        public async Task<Result<List<LeastPopularVenueDto>>> GetLeastPopularVenues()
        {
            var response = await httpClient.GetAsync("/api/least-popular-venues");
            var content = await response.Content.ReadAsStringAsync();
            var leastPopularVenues = JsonConvert.DeserializeObject<Result<List<LeastPopularVenueDto>>>(content)!;
            return leastPopularVenues;
        }

        /// <summary>
        /// Retrieves the license counts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the license counts.</returns>
        public async Task<Result<List<LicenseDto>>> GetLicenseCounts()
        {
            var response = await httpClient.GetAsync("/api/license-counts");
            var content = await response.Content.ReadAsStringAsync();
            var licenseCounts = JsonConvert.DeserializeObject<Result<List<LicenseDto>>>(content)!;
            return licenseCounts;
        }

        /// <summary>
        /// Retrieves the most popular venues.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the most popular venues.</returns>
        public async Task<Result<List<MostPopularVenueDto>>> GetMostPopularVenues()
        {
            var response = await httpClient.GetAsync("/api/most-popular-venues");
            var content = await response.Content.ReadAsStringAsync();
            var mostPopularVenues = JsonConvert.DeserializeObject<Result<List<MostPopularVenueDto>>>(content)!;
            return mostPopularVenues;
        }

        /// <summary>
        /// Retrieves the peak booking days.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the peak booking days.</returns>
        public async Task<Result<List<PeakBookingDaysDto>>> GetPeekBookingDays()
        {
            var response = await httpClient.GetAsync("/api/peak-booking-days");
            var content = await response.Content.ReadAsStringAsync();
            var peakBookingDays = JsonConvert.DeserializeObject<Result<List<PeakBookingDaysDto>>>(content)!;
            return peakBookingDays;
        }

        /// <summary>
        /// Retrieves the peak booking months.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the peak booking months.</returns>
        public async Task<Result<List<PeakBookingMonthDto>>> GetPeekBookingMonths()
        {
            var response = await httpClient.GetAsync("/api/peak-booking-months");
            var content = await response.Content.ReadAsStringAsync();
            var peakBookingMonths = JsonConvert.DeserializeObject<Result<List<PeakBookingMonthDto>>>(content)!;
            return peakBookingMonths;
        }

        /// <summary>
        /// Retrieves the peak booking times.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the peak booking times.</returns>
        public async Task<Result<List<PeakBookingTimesDto>>> GetPeakBookingTimes()
        {
            var response = await httpClient.GetAsync("/api/peak-booking-times");
            var content = await response.Content.ReadAsStringAsync();
            var peakBookingTimes = JsonConvert.DeserializeObject<Result<List<PeakBookingTimesDto>>>(content)!;
            return peakBookingTimes;
        }

        /// <summary>
        /// Retrieves the peak booking years.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the peak booking years.</returns>
        public async Task<Result<List<PeakBookingYearDto>>> PeakBookingYears()
        {
            var response = await httpClient.GetAsync("/api/peak-booking-years");
            var content = await response.Content.ReadAsStringAsync();
            var peakBookingYears = JsonConvert.DeserializeObject<Result<List<PeakBookingYearDto>>>(content)!;
            return peakBookingYears;
        }

        /// <summary>
        /// Retrieves the question age group analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the question age group analysis.</returns>
        public async Task<Result<List<QuestionAgeGroupDto>>> GetQuestionAgeGroupAnalysis()
        {
            var response = await httpClient.GetAsync("/api/question-age-group-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var questionAgeGroupAnalysis = JsonConvert.DeserializeObject<Result<List<QuestionAgeGroupDto>>>(content)!;
            return questionAgeGroupAnalysis;
        }

        /// <summary>
        /// Retrieves the question analyses.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the question analyses.</returns>
        public async Task<Result<List<QuestionAnalysisDto>>> GetQuestionAnalyses()
        {
            var response = await httpClient.GetAsync("/api/question-analyses");
            var content = await response.Content.ReadAsStringAsync();
            var questionAnalyses = JsonConvert.DeserializeObject<Result<List<QuestionAnalysisDto>>>(content)!;
            return questionAnalyses;
        }

        /// <summary>
        /// Retrieves the question difficultness.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the question difficultness.</returns>
        public async Task<Result<List<QuestionDifficultyDto>>> GetQuestionDifficultness()
        {
            var response = await httpClient.GetAsync("/api/question-difficultness");
            var content = await response.Content.ReadAsStringAsync();
            var questionDifficultness = JsonConvert.DeserializeObject<Result<List<QuestionDifficultyDto>>>(content)!;
            return questionDifficultness;
        }

        /// <summary>
        /// Retrieves the question gender analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the question gender analysis.</returns>
        public async Task<Result<List<QuestionGenderDto>>> GetQuestionGenderAnalysis()
        {
            var response = await httpClient.GetAsync("/api/question-gender-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var questionGenderAnalysis = JsonConvert.DeserializeObject<Result<List<QuestionGenderDto>>>(content)!;
            return questionGenderAnalysis;
        }

        /// <summary>
        /// Retrieves the repeated bookings count.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the repeated bookings count.</returns>
        public async Task<Result<int>> GetRepeatedBookingsCount()
        {
            var response = await httpClient.GetAsync("/api/repeated-bookings-count");
            var content = await response.Content.ReadAsStringAsync();
            var repeatedBookingsCount = JsonConvert.DeserializeObject<Result<int>>(content)!;
            return repeatedBookingsCount;
        }

        /// <summary>
        /// Retrieves the scores analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the scores analysis.</returns>
        public async Task<Result<List<ScoresDto>>> GetScoresAnalysis()
        {
            var response = await httpClient.GetAsync("/api/scores-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var scoresAnalysis = JsonConvert.DeserializeObject<Result<List<ScoresDto>>>(content)!;
            return scoresAnalysis;
        }

        /// <summary>
        /// Retrieves the test retake gender analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the test retake gender analysis.</returns>
        public async Task<Result<List<TestGenderDto>>> GetTestRetakeGenderAnalysis()
        {
            var response = await httpClient.GetAsync("/api/test-retake-gender-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var testRetakeGenderAnalysis = JsonConvert.DeserializeObject<Result<List<TestGenderDto>>>(content)!;
            return testRetakeGenderAnalysis;

        }

        /// <summary>
        /// Retrieves the test pass age group analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the test pass age group analysis.</returns>
        public async Task<Result<List<TestPassAgeGroupDto>>> GetTestPassAgeGroupAnalysis()
        {
            var response = await httpClient.GetAsync("/api/test-pass-age-group-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var testPassAgeGroupAnalysis = JsonConvert.DeserializeObject<Result<List<TestPassAgeGroupDto>>>(content)!;
            return testPassAgeGroupAnalysis;
        }

        /// <summary>
        /// Retrieves the test retake age group analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the test retake age group analysis.</returns>
        public async Task<Result<List<TestRetakeAgeGroupDto>>> GetTestRetakeAgeGroupAnalysis()
        {
            var response = await httpClient.GetAsync("/api/test-retake-age-group-analysis");
            var content = await response.Content.ReadAsStringAsync();
            var testRetakeAgeGroupAnalysis = JsonConvert.DeserializeObject<Result<List<TestRetakeAgeGroupDto>>>(content)!;
            return testRetakeAgeGroupAnalysis;
        }

        /// <summary>
        /// Retrieves the test retake analysis.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the test retake analysis.</returns>
        public async Task<Result<int>> GetTestRetakeAnalysis()
        {
            var response = await httpClient.GetAsync("/api/test-retake");
            var content = await response.Content.ReadAsStringAsync();
            var testRetakeAnalysis = JsonConvert.DeserializeObject<Result<int>>(content)!;
            return testRetakeAnalysis;
        }

        /// <summary>
        /// Retrieves the venue utilization.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the venue utilization.</returns>
        public async Task<Result<List<VenueUtilizationDto>>> GetVenueUtilization()
        {
            var response = await httpClient.GetAsync("/api/venue-utilizations");
            var content = await response.Content.ReadAsStringAsync();
            var venueUtilization = JsonConvert.DeserializeObject<Result<List<VenueUtilizationDto>>>(content)!;
            return venueUtilization;
        }

        /// <summary>
        /// Retrieves the venue booking status counts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the venue booking status counts.</returns>
        public async Task<Result<VenueBookingStatusCount>> GetVenueBookingStatusCounts()
        {
            var response = await httpClient.GetAsync("/api/venue-booking-status-counts");
            var content = await response.Content.ReadAsStringAsync();
            var venueBookingStatusCounts = JsonConvert.DeserializeObject<Result<VenueBookingStatusCount>>(content)!;
            return venueBookingStatusCounts;
        }
    }
}
