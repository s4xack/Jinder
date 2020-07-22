using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jinder.Core.Tools.Compilers.Rules;
using Jinder.Poco.Models;

namespace Jinder.Core.Tools.Compilers
{
    public class VacancyCompiler : ICompiler<Vacancy>
    {
        public IEnumerable<Vacancy> Compile(IEnumerable<Vacancy> vacancies, IRule<Vacancy> rule)
        {
            return vacancies.Where(rule.IsSatisfied);
        }
    }
}
