using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class VacancySuggestionRepository : IVacancySuggestionRepository
    {
        private readonly JinderContext _context;

        public VacancySuggestionRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public VacancySuggestion Get(Int32 suggestionId)
        {
            return _context.VacancySuggestions
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
                .SingleOrDefault(v => v.Id == suggestionId)
                ?.ToModel() ?? throw new ArgumentException($"No vacancy suggestion with id {suggestionId}!");
        }

        public IReadOnlyCollection<VacancySuggestion> GetAllForSummary(Int32 summaryId)
        {
            return _context.VacancySuggestions
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
                .Where(v => v.SummaryId == summaryId)
                .Select(v => v.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<VacancySuggestion> GetForSummaryByState(Int32 summaryId, SuggestionStatus state)
        {
            return _context.VacancySuggestions
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
                .Where(v => v.SummaryId == summaryId)
                .Where(v => v.Status == state)
                .Select(v => v.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<VacancySuggestion> Add(IReadOnlyCollection<VacancySuggestion> vacancySuggestions)
        {
            List<DbVacancySuggestion> dbVacancySuggestions = vacancySuggestions
                .Select(DbVacancySuggestion.FromModel)
                .ToList();

            dbVacancySuggestions = dbVacancySuggestions
                .Select(v => _context.VacancySuggestions
                    .Add(v)
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

            return dbVacancySuggestions
                .Select(s => s.ToModel())
                .ToList();
        }

        public VacancySuggestion Update(VacancySuggestion vacancySuggestion)
        {
            DbVacancySuggestion dbVacancySuggestion = _context.VacancySuggestions
                                                          .SingleOrDefault(s => s.Id == vacancySuggestion.Id) ??
                                                      throw new ArgumentException($"No vacancy suggestion with id {vacancySuggestion.Id}!");

            dbVacancySuggestion.Status = vacancySuggestion.Status;

            dbVacancySuggestion = _context.VacancySuggestions
                .Update(dbVacancySuggestion)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create summary suggestions with such data!");
            }

            return dbVacancySuggestion.ToModel();
        }
    }
}