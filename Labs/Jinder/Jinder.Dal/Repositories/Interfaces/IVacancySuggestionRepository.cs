using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface IVacancySuggestionRepository
    {
        VacancySuggestion Get(Int32 suggestionId);
        IEnumerable<VacancySuggestion> GetAllForSummary(Int32 summaryId);
        IEnumerable<VacancySuggestion> Add(IEnumerable<VacancySuggestion> summarySuggestions);
        Int32 NewId { get; }
    }
}