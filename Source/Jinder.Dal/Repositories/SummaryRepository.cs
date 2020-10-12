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

        public IReadOnlyCollection<Summary> Get()
        {
            return _context.Summaries
                .Include(s => s.User)
                .Include(s => s.Specialization)
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .Select(s => s.ToModel())
                .ToList();
        }

        public Summary Get(Int32 summaryId)
        {
            return _context.Summaries
                    .Include(s => s.User)
                    .Include(s => s.Specialization)
                    .Include(s => s.Skills)
                        .ThenInclude(s => s.Skill)
                    .SingleOrDefault(s => s.Id == summaryId)
                    ?.ToModel() ?? throw new ArgumentException($"No summary with id {summaryId}!");
        }

        public Summary GetForUser(Int32 userId)
        {
            return _context.Summaries
                       .Include(s => s.User)
                       .Include(s => s.Specialization)
                       .Include(s => s.Skills)
                            .ThenInclude(s => s.Skill)
                       .SingleOrDefault(s => s.UserId == userId)
                       ?.ToModel() ?? throw new ArgumentException($"No summary for user with id {userId}!");
        }

        public Summary Create(Summary summary)
        {
            DbSummary dbSummary = DbSummary.FromModel(summary);

            dbSummary = _context.Summaries
                .Add(dbSummary)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary with such data!");
            }

            return dbSummary.ToModel();
        }

        public Summary Delete(Int32 summaryId)
        {
            DbSummary dbSummary = _context.Summaries
                                      .SingleOrDefault(s => s.Id == summaryId) ??
                                  throw new ArgumentException($"No summary with id {summaryId}!");

            dbSummary = _context.Summaries
                .Remove(dbSummary)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to delete summary with such data!");
            }

            return dbSummary.ToModel();
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return !(_context.Summaries
                .SingleOrDefault(s => s.UserId == userId) is null);
        }
    }
}