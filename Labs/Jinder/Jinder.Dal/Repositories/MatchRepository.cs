using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly JinderContext _context;

        public MatchRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Match Get(Int32 matchId)
        {
            return _context.Matches
                .Include(m => m.Summary)
                    .ThenInclude(s => s.Specialization)
                .Include(m => m.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(m => m.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(m => m.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .SingleOrDefault(m => m.Id == matchId)
                ?.ToModel() ?? throw new ArgumentException($"No natch with id {matchId}!");
        }

        public IReadOnlyCollection<Match> GetAllForSummary(Int32 summaryId)
        {
            return _context.Matches
                .Include(m => m.Summary)
                    .ThenInclude(s => s.Specialization)
                .Include(m => m.Summary)
                    .ThenInclude(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Include(m => m.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(m => m.Vacancy)
                        .ThenInclude(v => v.Skills)
                            .ThenInclude(s => s.Skill)
                .Where(m => m.SummaryId == summaryId)
                .Select(m => m.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<Match> GetAllForVacancy(Int32 vacancyId)
        {
            return _context.Matches
                .Include(m => m.Summary)
                .ThenInclude(s => s.Specialization)
                .Include(m => m.Summary)
                .ThenInclude(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Include(m => m.Vacancy)
                .ThenInclude(v => v.Specialization)
                .Include(m => m.Vacancy)
                .ThenInclude(v => v.Skills)
                .ThenInclude(s => s.Skill)
                .Where(m => m.VacancyId == vacancyId)
                .Select(m => m.ToModel())
                .ToList();
        }

        public Match Add(Match match)
        {
            match = _context.Matches
                .Add(DbMatch.FromModel(match))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create Match with such data!");
            }

            return match;
        }

        public Match Update(Match match)
        {
            match = _context.Matches
                .Update(DbMatch.FromModel(match))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary suggestions with such data!");
            }

            return match;
        }
    }
}