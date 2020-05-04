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

        public Boolean IsSatisfied(Summary summary)
        {
            return summary.Specialization.Id == _specialization.Id &&
                   summary.Skills
                       .Select(s => s.Id)
                       .Where(_skills.Select(s => s.Id).Contains)
                       .Count() * 2 >= _skills.Count;
        }
    }
}
