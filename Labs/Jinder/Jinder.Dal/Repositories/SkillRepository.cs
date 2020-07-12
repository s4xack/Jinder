using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly JinderContext _context;

        public SkillRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyCollection<Skill> GetAll()
        {
            return _context.Skills
                .Select(s => s.ToModel())
                .ToList();
        }

        public Skill GetByName(String skillName)
        {
            return _context.Skills
                       .SingleOrDefault(s => s.Name == skillName)
                       ?.ToModel() ?? throw new ArgumentException($"No skill with name {skillName}!");
        }

        public Skill Add(Skill skill)
        {
            skill = _context.Skills
                .Add(DbSkill.FromModel(skill))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create skill with such data!");
            }

            return skill;
        }

        public Skill DeleteByName(String skillName)
        {
            Skill skill = GetByName(skillName);

            skill = _context.Skills
                .Remove(DbSkill.FromModel(skill))
                .Entity
                .ToModel();

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to delete skill with such name!");
            }

            return skill;
        }
    }
}