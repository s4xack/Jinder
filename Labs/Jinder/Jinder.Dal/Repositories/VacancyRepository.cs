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
                .Include(v => v.Specialization)
                .Include(v => v.Skills)
                .ThenInclude(v => v.Skill)
                .Select(v => v.ToModel())
                .ToList();
        }

        public Vacancy Get(Int32 vacancyId)
        {
            return _context.Vacancies
                    .Include(v => v.Specialization)
                    .Include(v => v.Skills)
                       .ThenInclude(v => v.Skill)
                    .SingleOrDefault(v => v.Id == vacancyId)
                    ?.ToModel() ?? throw new ArgumentException($"No Vacancy with id {vacancyId}!");
        }

        public Vacancy GetForUser(Int32 userId)
        {
            return _context.Vacancies
                       .Include(v => v.Specialization)
                       .Include(v => v.Skills)
                            .ThenInclude(v => v.Skill)
                       .SingleOrDefault(v => v.UserId == userId)
                       ?.ToModel() ?? throw new ArgumentException($"No Vacancy dor user with id {userId}!");
        }

        public Vacancy Create(Vacancy vacancy)
        {
            vacancy = _context.Vacancies
                .Add(DbVacancy.FromModel(vacancy))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create Vacancy with such data!");
            }

            return vacancy;
        }

        public Vacancy Delete(Int32 vacancyId)
        {
            Vacancy vacancy = Get(vacancyId);

            _context.Vacancies
                .Remove(DbVacancy.FromModel(vacancy))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to delete Vacancy with such data!");
            }

            return vacancy;
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return !(_context.Vacancies
                .SingleOrDefault(v => v.UserId == userId) is null);
        }
    }
}