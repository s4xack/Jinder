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
            foreach (var summary in _summaries) _newId = Math.Max(_newId, summary.Id);
            _newId++;
        }

        public SummaryRepositoryMock() :
            this(new List<Summary>
            {
                new Summary(
                    1,
                    0,
                    new Specialization(0, "Spec1"),
                    new List<Skill> {new Skill(0, "Skill1"), new Skill(1, "Skill2")},
                    "Info"),
                new Summary(
                    3,
                    1,
                    new Specialization(1, "Spec2"),
                    new List<Skill> {new Skill(2, "Skill3"), new Skill(3, "Skill4")},
                    "Info"),
            })
        {
        }

        public IEnumerable<Summary> GetAll()
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
            return _summaries.FirstOrDefault(s => s.UserId == userId) ??
                   throw new ArgumentException($"No summary for user with id {userId}!");
        }

        public Summary Create(Summary summary)
        {
            _summaries.Add(summary);
            return summary;
        }

        public Summary Delete(Int32 summaryId)
        {
            var summary = _summaries.FirstOrDefault(s => s.Id == summaryId) ?? throw new ArgumentException();
            _summaries.Remove(summary);
            return summary;
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return _summaries.Exists(s => s.UserId == userId);
        }

        public Int32 NewId { get => _newId++; }
    }
}