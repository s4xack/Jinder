using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface ISummarySuggestionService
    {
        List<SummarySuggestionDto> SuggestAllForUser(Int32 userId);
        SummarySuggestionDto SuggestForUser(Int32 userId);
        void AcceptSuggestionForUser(Int32 userId, Int32 suggestionId);
        void RejectSuggestionForUser(Int32 userId, Int32 suggestionId);
        void SkipSuggestionForUser(Int32 userId, Int32 suggestionId);
    }
}