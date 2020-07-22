using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly JinderContext _context;

        public SpecializationRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyCollection<Specialization> GetAll()
        {
            return _context.Specializations
                .Select(s => s.ToModel())
                .ToList();
        }

        public Specialization GetByName(String specializationName)
        {
            return _context.Specializations
                       .SingleOrDefault(s => s.Name == specializationName)
                       ?.ToModel() ?? throw new ArgumentException($"No skill with name {specializationName}!");
        }

        public Specialization Add(Specialization specialization)
        {
            DbSpecialization dbSpecialization = DbSpecialization.FromModel(specialization);

            dbSpecialization = _context.Specializations
                .Add(dbSpecialization)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create skill with such data!");
            }

            return dbSpecialization.ToModel();
        }

        public Specialization DeleteByName(String specializationName)
        {
            DbSpecialization dbSpecialization = _context.Specializations
                                                    .SingleOrDefault(s => s.Name == specializationName) ??
                                                throw new ArgumentException($"No skill with name {specializationName}!");

            dbSpecialization = _context.Specializations
                .Remove(dbSpecialization)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to delete specialization with such name!");
            }

            return dbSpecialization.ToModel();
        }
    }
}