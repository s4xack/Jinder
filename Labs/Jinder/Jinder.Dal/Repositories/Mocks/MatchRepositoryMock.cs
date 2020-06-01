﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class MatchRepositoryMock : IMatchRepository
    {
        private readonly List<Match> _matches;
        private Int32 _newId;

        public MatchRepositoryMock(List<Match> matches)
        {
            _matches = matches;
            _newId = 0;
            foreach (var match in matches)
                match.Id = _newId++;
        }

        public MatchRepositoryMock() : this(new List<Match>())
        {
        }

        public Match Get(Int32 matchId)
        {
            return _matches.FirstOrDefault(m => m.Id == matchId) ??
                   throw new ArgumentException($"No natch with id {matchId}!");
        }

        public IReadOnlyCollection<Match> GetAllForSummary(Int32 summaryId)
        {
            return _matches.Where(m => m.SummaryId == summaryId).ToList();
        }

        public IReadOnlyCollection<Match> GetAllForVacancy(Int32 vacancyId)
        {
            return _matches.Where(m => m.VacancyId == vacancyId).ToList();
        }

        public Match Add(Match match)
        {
            match.Id = _newId++;
            _matches.Add(match);
            return match;
        }
    }
}