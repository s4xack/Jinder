using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jinder.Poco.Models;

namespace Jinder.Core.Tools.Compilers.Rules
{
    public class SimpleSummaryRule : IRule<Summary>
    {
        private readonly Specialization _specialization;
        private readonly List<Skill> _skills;

        public SimpleSummaryRule(Specialization specialization, List<Skill> skills)
        {
            _specialization = specialization;
            _skills = skills;
        }

        public Boolean IsSatisfied(Summary vacancy)
        {
            return vacancy.Specialization.Name == _specialization.Name &&
                   vacancy.Skills
                       .Select(s => s.SkillName)
                       .Where(_skills.Select(s => s.Name).Contains)
                       .Count() * 2 >= _skills.Count;
        }
    }
}
