using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface ISummarySuggestionRepository
    {
        SummarySuggestion Get(Int32 suggestionId);
        IEnumerable<SummarySuggestion> GetAllForVacancy(Int32 vacancyId);
        IEnumerable<SummarySuggestion> Add(IEnumerable<SummarySuggestion> summarySuggestions);
        Int32 NewId { get; }
    }
}
