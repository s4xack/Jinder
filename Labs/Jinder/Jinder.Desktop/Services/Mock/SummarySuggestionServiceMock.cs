using System;
using System.Linq;
using Jinder.Desktop.Mock;
using Jinder.Poco.Dto;
using Jinder.Poco.Types;

namespace Jinder.Desktop.Services.Mock
{
    public class SummarySuggestionServiceMock : ISummarySuggestionService
    {
        public SummarySuggestionDto GetForMe(Guid token)
        {
            return Recruiter.SummarySuggestions
                .FirstOrDefault(s => s.Status == SuggestionStatus.Ready);
        }

        public void Accept(Guid token, Int32 suggestionId)
        {
            var suggestion = Recruiter.SummarySuggestions
                .FirstOrDefault(s => s.Id == suggestionId);
            if (!(suggestion is null))
            {
                Recruiter.SummarySuggestions.Remove(suggestion);
                if (suggestion.Summary.Id == 1)
                    Candidate.LikeTest = true;
                suggestion = new SummarySuggestionDto(suggestion.Id, suggestion.Summary, SuggestionStatus.Accepted);
            }
        }

        public void Reject(Guid token, Int32 suggestionId)
        {
            var suggestion = Recruiter.SummarySuggestions
                .FirstOrDefault(s => s.Id == suggestionId);
            if (!(suggestion is null))
            {
                Recruiter.SummarySuggestions.Remove(suggestion);
                suggestion = new SummarySuggestionDto(suggestion.Id, suggestion.Summary, SuggestionStatus.Rejected);
            }
        }

        public void Skip(Guid token, Int32 suggestionId)
        {
            var suggestion = Recruiter.SummarySuggestions
                .FirstOrDefault(s => s.Id == suggestionId);
            if (!(suggestion is null))
            {
                Recruiter.SummarySuggestions.Remove(suggestion);
                suggestion = new SummarySuggestionDto(suggestion.Id, suggestion.Summary, SuggestionStatus.Skipped);
            }
        }
    }
}