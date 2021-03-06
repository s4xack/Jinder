﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.Repositories.Mocks
{
    public class VacancySuggestionRepositoryMock : IVacancySuggestionRepository
    {
        private readonly List<VacancySuggestion> _vacancySuggestions;
        private Int32 _newId;

        public VacancySuggestionRepositoryMock(List<VacancySuggestion> vacancySuggestions)
        {
            _vacancySuggestions = vacancySuggestions.Select(s => s.Copy()).ToList();
            _newId = 0;
            foreach (var vacancySuggestion in _vacancySuggestions) 
                vacancySuggestion.Id = _newId++;
        }

        public VacancySuggestionRepositoryMock() : this(new List<VacancySuggestion>())
        {
        }

        public VacancySuggestion Get(Int32 suggestionId)
        {
            return _vacancySuggestions.FirstOrDefault(s => s.Id == suggestionId) ??
                   throw new ArgumentException($"No vacancy suggestion with id {suggestionId}!");
        }

        public IReadOnlyCollection<VacancySuggestion> GetAllForSummary(Int32 summaryId)
        {
            return _vacancySuggestions.Where(s => s.Summary.Id == summaryId).ToList();
        }

        public IReadOnlyCollection<VacancySuggestion> GetForSummaryByState(Int32 summaryId, SuggestionStatus state)
        {
            return _vacancySuggestions
                .Where(s => s.Summary.Id == summaryId)
                .Where(s => s.Status == state)
                .Select(s => s.Copy())
                .ToList();
        }

        public IReadOnlyCollection<VacancySuggestion> Add(IReadOnlyCollection<VacancySuggestion> vacancySuggestions)
        {
            foreach (var vacancySuggestion in vacancySuggestions)
            {
                vacancySuggestion.Id = _newId++;
            }
            _vacancySuggestions.AddRange(vacancySuggestions.Select(s => s.Copy()));
            return vacancySuggestions.ToList();
        }

        public VacancySuggestion Update(VacancySuggestion vacancySuggestion)
        {
            VacancySuggestion oldSuggestion = _vacancySuggestions.Find(s => s.Id == vacancySuggestion.Id);
            _vacancySuggestions.Remove(oldSuggestion);
            _vacancySuggestions.Add(vacancySuggestion.Copy());
            return vacancySuggestion;
        }
    }
}