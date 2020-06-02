using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.Repositories
{
    public interface IVacancySuggestionRepository
    {
        VacancySuggestion Get(Int32 suggestionId);
        IReadOnlyCollection<VacancySuggestion> GetAllForSummary(Int32 summaryId);
        IReadOnlyCollection<VacancySuggestion> GetForSummaryByState(Int32 summaryId, SuggestionStatus state);
        IReadOnlyCollection<VacancySuggestion> Add(IReadOnlyCollection<VacancySuggestion> summarySuggestions);
        VacancySuggestion Update(VacancySuggestion vacancySuggestion);
    }
}