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
                    .ThenInclude(s => s.Specialization)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
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
                    .ThenInclude(s => s.Specialization)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
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
                    .ThenInclude(s => s.Specialization)
                .Include(s => s.Summary)
                    .ThenInclude(s => s.Skills)
                        .ThenInclude(s => s.Skill)
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
            summarySuggestions = summarySuggestions
                .Select(s => _context.SummarySuggestions
                    .Add(DbSummarySuggestion.FromModel(s))
                    .Entity
                    .ToModel())
                .ToList();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary suggestions with such data!");
            }

            return summarySuggestions;
        }

        public SummarySuggestion Update(SummarySuggestion summarySuggestion)
        {
            summarySuggestion = _context.SummarySuggestions
                .Update(DbSummarySuggestion.FromModel(summarySuggestion))
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

            return summarySuggestion;
        }
    }
}