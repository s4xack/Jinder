using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class SummaryRepository : ISummaryRepository
    {
        private readonly JinderContext _context;

        public SummaryRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyCollection<Summary> GetAll()
        {
            return _context.Summaries
                .Include(s => s.Specialization)
                .Include(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Select(s => s.ToModel())
                .ToList();
        }

        public Summary Get(Int32 summaryId)
        {
            return _context.Summaries
                    .Include(s => s.Specialization)
                    .Include(s => s.Skills)
                       .ThenInclude(s => s.Skill)
                    .SingleOrDefault(s => s.Id == summaryId)
                    ?.ToModel() ?? throw new ArgumentException($"No summary with id {summaryId}!");
        }

        public Summary GetForUser(Int32 userId)
        {
            return _context.Summaries
                       .Include(s => s.Specialization)
                       .Include(s => s.Skills)
                            .ThenInclude(s => s.Skill)
                       .SingleOrDefault(s => s.UserId == userId)
                       ?.ToModel() ?? throw new ArgumentException($"No summary dor user with id {userId}!");
        }

        public Summary Create(Summary summary)
        {
            summary = _context.Summaries
                .Add(DbSummary.FromModel(summary))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary with such data!");
            }

            return summary;
        }

        public Summary Delete(Int32 summaryId)
        {
            Summary summary = Get(summaryId);

            _context.Summaries
                .Remove(DbSummary.FromModel(summary))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to delete summary with such data!");
            }

            return summary;
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return !(_context.Summaries
                .SingleOrDefault(s => s.UserId == userId) is null);
        }
    }
}