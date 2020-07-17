using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class SummaryRepositoryMock : ISummaryRepository
    {
        private readonly List<Summary> _summaries;
        private Int32 _newId;

        public SummaryRepositoryMock(List<Summary> summaries)
        {
            _summaries = summaries;
            _newId = 0;
            foreach (var summary in _summaries)
                summary.Id = _newId++;
        }

        public SummaryRepositoryMock() : this(new List<Summary>())
        {
        }

        public IReadOnlyCollection<Summary> GetAll()
        {
            return _summaries;
        }

        public Summary Get(Int32 summaryId)
        {
            return _summaries.FirstOrDefault(s => s.Id == summaryId) ??
                   throw new ArgumentException($"No summary with id {summaryId}!");
        }

        public Summary GetForUser(Int32 userId)
        {
            return _summaries.FirstOrDefault(s => s.User.Id == userId) ??
                   throw new ArgumentException($"No summary for user with id {userId}!");
        }

        public Summary Create(Summary summary)
        {
            summary.Id = _newId++;
            _summaries.Add(summary);
            return summary;
        }

        public Summary Delete(Int32 summaryId)
        {
            Summary summary = _summaries.FirstOrDefault(s => s.Id == summaryId) ?? throw new ArgumentException();
            _summaries.Remove(summary);
            return summary;
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return _summaries.Exists(s => s.User.Id == userId);
        }
    }
}