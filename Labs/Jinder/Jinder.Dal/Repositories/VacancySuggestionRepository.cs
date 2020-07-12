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
                .Include(v => v.Summary)
                    .ThenInclude(v => v.Specialization)
                .Include(v => v.Summary)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(v => v.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(v => v.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(v => v.Skill)
                .SingleOrDefault(v => v.Id == suggestionId)
                ?.ToModel() ?? throw new ArgumentException($"No vacancy suggestion with id {suggestionId}!");
        }

        public IReadOnlyCollection<VacancySuggestion> GetAllForSummary(Int32 summaryId)
        {
            return _context.VacancySuggestions
                .Include(v => v.Summary)
                    .ThenInclude(v => v.Specialization)
                .Include(v => v.Summary)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(v => v.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(v => v.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(v => v.Skill)
                .Where(v => v.SummaryId == summaryId)
                .Select(v => v.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<VacancySuggestion> GetForSummaryByState(Int32 summaryId, SuggestionStatus state)
        {
            return _context.VacancySuggestions
                .Include(v => v.Summary)
                    .ThenInclude(v => v.Specialization)
                .Include(v => v.Summary)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(s => s.Skill)
                .Include(v => v.Vacancy)
                    .ThenInclude(v => v.Specialization)
                .Include(v => v.Vacancy)
                    .ThenInclude(v => v.Skills)
                        .ThenInclude(v => v.Skill)
                .Where(v => v.SummaryId == summaryId)
                .Where(v => v.Status == state)
                .Select(v => v.ToModel())
                .ToList();
        }

        public IReadOnlyCollection<VacancySuggestion> Add(IReadOnlyCollection<VacancySuggestion> vacancySuggestions)
        {
            vacancySuggestions = vacancySuggestions
                .Select(v => _context.VacancySuggestions
                    .Add(DbVacancySuggestion.FromModel(v))
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

            return vacancySuggestions;
        }

        public VacancySuggestion Update(VacancySuggestion vacancySuggestion)
        {
            vacancySuggestion = _context.VacancySuggestions
                .Update(DbVacancySuggestion.FromModel(vacancySuggestion))
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

            return vacancySuggestion;
        }
    }
}