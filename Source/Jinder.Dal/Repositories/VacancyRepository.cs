using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly JinderContext _context;

        public VacancyRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyCollection<Vacancy> GetAll()
        {
            return _context.Vacancies
                .Include(s => s.User)
                .Include(v => v.Specialization)
                .Include(v => v.Skills)
                .ThenInclude(v => v.Skill)
                .Select(v => v.ToModel())
                .ToList();
        }

        public Vacancy Get(Int32 vacancyId)
        {
            return _context.Vacancies
                    .Include(s => s.User)
                    .Include(v => v.Specialization)
                    .Include(v => v.Skills)
                       .ThenInclude(v => v.Skill)
                    .SingleOrDefault(v => v.Id == vacancyId)
                    ?.ToModel() ?? throw new ArgumentException($"No Vacancy with id {vacancyId}!");
        }

        public Vacancy GetForUser(Int32 userId)
        {
            return _context.Vacancies
                       .Include(s => s.User)
                       .Include(v => v.Specialization)
                       .Include(v => v.Skills)
                            .ThenInclude(v => v.Skill)
                       .SingleOrDefault(v => v.UserId == userId)
                       ?.ToModel() ?? throw new ArgumentException($"No Vacancy for user with id {userId}!");
        }

        public Vacancy Create(Vacancy vacancy)
        {
            DbVacancy dbVacancy = DbVacancy.FromModel(vacancy);

            dbVacancy = _context.Vacancies
                .Add(dbVacancy)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create Vacancy with such data!");
            }

            return dbVacancy.ToModel();
        }

        public Vacancy Delete(Int32 vacancyId)
        {
            DbVacancy dbVacancy = _context.Vacancies
                                      .SingleOrDefault(v => v.Id == vacancyId) ??
                                  throw new ArgumentException($"No Vacancy with id {vacancyId}!");

            dbVacancy = _context.Vacancies
                .Remove(dbVacancy)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to delete Vacancy with such data!");
            }

            return dbVacancy.ToModel();
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return !(_context.Vacancies
                .SingleOrDefault(v => v.UserId == userId) is null);
        }
    }
}