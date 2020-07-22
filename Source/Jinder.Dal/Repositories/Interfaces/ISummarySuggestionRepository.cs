using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.Repositories
{
    public interface ISummarySuggestionRepository
    {
        SummarySuggestion Get(Int32 suggestionId);
        IReadOnlyCollection<SummarySuggestion> GetAllForVacancy(Int32 vacancyId);
        IReadOnlyCollection<SummarySuggestion> GetForVacancyByState(Int32 vacancyId, SuggestionStatus state);
        IReadOnlyCollection<SummarySuggestion> Add(IReadOnlyCollection<SummarySuggestion> summarySuggestions);
        SummarySuggestion Update(SummarySuggestion summarySuggestion);
    }
}
