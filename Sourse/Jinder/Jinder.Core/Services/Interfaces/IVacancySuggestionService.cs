using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface IVacancySuggestionService
    {
        List<VacancySuggestionDto> SuggestAllForUser(Int32 userId);
        VacancySuggestionDto SuggestForUser(Int32 userId);
        void AcceptSuggestionForUser(Int32 userId, Int32 suggestionId);
        void RejectSuggestionForUser(Int32 userId, Int32 suggestionId);
        void SkipSuggestionForUser(Int32 userId, Int32 suggestionId);
    }
}