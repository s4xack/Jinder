using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.Repositories.Mocks
{
    public class SummarySuggestionRepositoryMock : ISummarySuggestionRepository
    {
        private readonly List<SummarySuggestion> _summarySuggestions;
        private Int32 _newId;

        public SummarySuggestionRepositoryMock(List<SummarySuggestion> summarySuggestions)
        {
            _summarySuggestions = summarySuggestions.Select(s => s.Copy()).ToList();
            _newId = 0;
            foreach (var summarySuggestion in _summarySuggestions)
                summarySuggestion.Id = _newId++;
        }

        public SummarySuggestionRepositoryMock() : this(new List<SummarySuggestion>())
        {
        }

        public SummarySuggestion Get(Int32 suggestionId)
        {
            return _summarySuggestions.FirstOrDefault(s => s.Id == suggestionId)?.Copy() ??
                   throw new ArgumentException($"No summary suggestion with id {suggestionId}!");
        }

        public IReadOnlyCollection<SummarySuggestion> GetAllForVacancy(Int32 vacancyId)
        {
            return _summarySuggestions.Where(s => s.VacancyId == vacancyId).Select(s => s.Copy()).ToList();
        }

        public IReadOnlyCollection<SummarySuggestion> GetForVacancyByState(Int32 vacancyId, SuggestionStatus state)
        {
            return _summarySuggestions
                .Where(s => s.VacancyId == vacancyId)
                .Where(s => s.Status == state)
                .Select(s => s.Copy())
                .ToList();
        }

        public IReadOnlyCollection<SummarySuggestion> Add(IReadOnlyCollection<SummarySuggestion> summarySuggestions)
        {
            foreach (var summarySuggestion in summarySuggestions)
            {
                summarySuggestion.Id = _newId++;
            }
            _summarySuggestions.AddRange(summarySuggestions.Select(s => s.Copy()));
            return summarySuggestions.Select(s => s.Copy()).ToList();
        }

        public SummarySuggestion Update(SummarySuggestion summarySuggestion)
        {
            SummarySuggestion oldSuggestion = _summarySuggestions.Find(s => s.Id == summarySuggestion.Id);
            _summarySuggestions.Remove(oldSuggestion);
            _summarySuggestions.Add(summarySuggestion.Copy());
            return summarySuggestion;
        }
    }
}