using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface ISummaryCompilationService
    {
        List<SummaryDto> GetCompilationForUser(Int32 userId);
        SummaryDto GetSuggestionForUser(Int32 userId);
        List<SummaryDto> CreateForUser(Int32 userId);
        void AcceptSuggestionForUser(Int32 userId, Int32 summaryId);
        void RejectSuggestionForUser(Int32 userId, Int32 summaryId);
        void SkipSuggestionForUser(Int32 userId, Int32 summaryId);
    }
}