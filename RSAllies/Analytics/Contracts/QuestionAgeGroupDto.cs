﻿namespace RSAllies.Analytics.Contracts
{
    public class QuestionAgeGroupDto
    {
        public string AgeGroup { get; set; } = string.Empty;
        public int TotalResponses { get; set; }
        public int CorrectResponses { get; set; }
        public int IncorrectResponses { get; set; }
    }
}
