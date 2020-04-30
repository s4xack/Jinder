using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jinder.Poco.Models;

namespace Jinder.Core.Tools.Compilers.Rules
{
    public class SimpleSummaryRule : IRule<Summary>
    {
        private readonly String _specialization;
        private readonly List<String> _skills;

        public SimpleSummaryRule(String specialization, List<String> skills)
        {
            _specialization = specialization;
            _skills = skills;
        }

        public Boolean IsSatisfied(Summary summary)
        {
            return summary.Specialization.Name == _specialization &&
                   summary.Skills
                       .Select(s => s.Name)
                       .Where(_skills.Contains)
                       .Count() * 2 >= _skills.Count;
        }
    }
}
