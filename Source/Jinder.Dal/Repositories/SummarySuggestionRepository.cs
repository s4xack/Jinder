using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class SummarySuggestionRepository : ISummarySuggestionRepository
    {
        private readonly JinderContext _context;

        public SummarySuggestionRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public SummarySuggestion Get(Int32 suggestionId)
        {
            return _context.SummarySuggestions
                .Include(s => s.Summary)
                    .ThenInclude(s => s.User)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Specialization)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.User)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .SingleOrDefault(s => s.Id == suggestionId)
                ?.ToModel() ?? throw new ArgumentException($"No summary suggestion with id {suggestionId}!");
        }

        public IReadOnlyCollection<SummarySuggestion> GetAllForVacancy(Int32 vacancyId)
        {
            return _context.SummarySuggestions
                .Include(s => s.Summary)
                    .ThenInclude(s => s.User)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Specialization)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.User)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .Where(s => s.VacancyId == vacancyId)
                .Select(s => s.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<SummarySuggestion> GetForVacancyByState(Int32 vacancyId, SuggestionStatus state)
        {
            return _context.SummarySuggestions
                .Include(s => s.Summary)
                    .ThenInclude(s => s.User)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Specialization)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.User)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(s => s.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .Where(s => s.VacancyId == vacancyId)
                .Where(s => s.Status == state)
                .Select(s => s.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<SummarySuggestion> Add(IReadOnlyCollection<SummarySuggestion> summarySuggestions)
        {
            List<DbSummarySuggestion> dbSummarySuggestions = summarySuggestions
                .Select(DbSummarySuggestion.FromModel)
                .ToList();

            dbSummarySuggestions = dbSummarySuggestions
                .Select(s => _context.SummarySuggestions
                    .Add(s)
                    .Entity)
                .ToList();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary suggestions with such data!");
            }

            return dbSummarySuggestions
                .Select(s => s.ToModel())
                .ToList();
        }

        public SummarySuggestion Update(SummarySuggestion summarySuggestion)
        {
            DbSummarySuggestion dbSummarySuggestion = _context.SummarySuggestions
                                                          .SingleOrDefault(s => s.Id == summarySuggestion.Id) ??
                                                      throw new ArgumentException($"No summary suggestion with id {summarySuggestion.Id}!");
            
            dbSummarySuggestion.Status = summarySuggestion.Status;

            dbSummarySuggestion = _context.SummarySuggestions
                .Update(dbSummarySuggestion)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary suggestions with such data!");
            }

            return dbSummarySuggestion.ToModel();
        }
    }
}