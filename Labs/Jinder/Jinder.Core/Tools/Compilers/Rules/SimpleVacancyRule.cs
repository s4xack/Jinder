using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jinder.Poco.Models;

namespace Jinder.Core.Tools.Compilers.Rules
{
    public class SimpleVacancyRule : IRule<Vacancy>
    {
        private readonly String _specialization;
        private readonly List<String> _skills;

        public SimpleVacancyRule(String specialization, List<String> skills)
        {
            _specialization = specialization;
            _skills = skills;
        }

        public Boolean IsSatisfied(Vacancy vacancy)
        {
            return vacancy.Specialization.Name == _specialization &&
                   vacancy.Skills
                       .Select(s => s.Name)
                       .Where(_skills.Contains)
                       .Count() * 2 >= _skills.Count;
        }
    }
}
