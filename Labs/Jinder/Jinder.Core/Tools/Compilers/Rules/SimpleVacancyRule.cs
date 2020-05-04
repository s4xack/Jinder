using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jinder.Poco.Models;

namespace Jinder.Core.Tools.Compilers.Rules
{
    public class SimpleVacancyRule : IRule<Vacancy>
    {
        private readonly Specialization _specialization;
        private readonly List<Skill> _skills;

        public SimpleVacancyRule(Specialization specialization, List<Skill> skills)
        {
            _specialization = specialization;
            _skills = skills;
        }

        public Boolean IsSatisfied(Vacancy vacancy)
        {
            return vacancy.Specialization.Id == _specialization.Id &&
                   vacancy.Skills
                       .Select(s => s.Id)
                       .Where(_skills.Select(s => s.Id).Contains)
                       .Count() * 2 >= _skills.Count;
        }
    }
}
