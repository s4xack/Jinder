﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class SummarySuggestionRepositoryMock : ISummarySuggestionRepository
    {
        private readonly List<SummarySuggestion> _summarySuggestions;
        private Int32 _newId;

        public SummarySuggestionRepositoryMock(List<SummarySuggestion> summarySuggestions)
        {
            _summarySuggestions = summarySuggestions;
            _newId = 0;
            foreach (var summarySuggestion in _summarySuggestions)
                summarySuggestion.Id = _newId++;
        }

        public SummarySuggestionRepositoryMock() : this(new List<SummarySuggestion>())
        {
        }

        public SummarySuggestion Get(Int32 suggestionId)
        {
            return _summarySuggestions.FirstOrDefault(s => s.Id == suggestionId) ??
                   throw new ArgumentException($"No summary suggestion with id {suggestionId}!");
        }

        public IReadOnlyCollection<SummarySuggestion> GetAllForVacancy(Int32 vacancyId)
        {
            return _summarySuggestions.Where(s => s.VacancyId == vacancyId).ToList();
        }

        public IReadOnlyCollection<SummarySuggestion> Add(IReadOnlyCollection<SummarySuggestion> summarySuggestions)
        {
            foreach (var summarySuggestion in summarySuggestions)
            {
                summarySuggestion.Id = _newId++;
            }
            _summarySuggestions.AddRange(summarySuggestions);
            return summarySuggestions.ToList();
        }
    }
}