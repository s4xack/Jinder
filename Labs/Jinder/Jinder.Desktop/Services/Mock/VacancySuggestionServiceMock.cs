using System;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using Jinder.Desktop.Mock;
using Jinder.Poco.Dto;
using Jinder.Poco.Types;

namespace Jinder.Desktop.Services.Mock
{
    public class VacancySuggestionServiceMock : IVacancySuggestionService
    {
        public VacancySuggestionDto GetForMe(Guid token)
        {
            return Candidate.VacancySuggestions
                .FirstOrDefault(s => s.Status == SuggestionStatus.Ready);
        }

        public void Accept(Guid token, Int32 suggestionId)
        {
            var suggestion = Candidate.VacancySuggestions
                .FirstOrDefault(s => s.Id == suggestionId);
            if (!(suggestion is null))
            {
                Candidate.VacancySuggestions.Remove(suggestion);
                if (suggestion.Vacancy.Id == 1)
                    Recruiter.LikeTest = true;
                suggestion = new VacancySuggestionDto(suggestion.Id, suggestion.Vacancy, SuggestionStatus.Accepted);
            }
        }

        public void Reject(Guid token, Int32 suggestionId)
        {
            var suggestion = Candidate.VacancySuggestions
                .FirstOrDefault(s => s.Id == suggestionId);
            if (!(suggestion is null))
            {
                Candidate.VacancySuggestions.Remove(suggestion);
                suggestion = new VacancySuggestionDto(suggestion.Id, suggestion.Vacancy, SuggestionStatus.Rejected);
            }
        }

        public void Skip(Guid token, Int32 suggestionId)
        {
            var suggestion = Candidate.VacancySuggestions
                .FirstOrDefault(s => s.Id == suggestionId);
            if (!(suggestion is null))
            {
                Candidate.VacancySuggestions.Remove(suggestion);
                suggestion = new VacancySuggestionDto(suggestion.Id, suggestion.Vacancy, SuggestionStatus.Skipped);
            }
        }
    }
}