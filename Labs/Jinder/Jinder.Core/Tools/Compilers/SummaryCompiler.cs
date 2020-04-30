using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jinder.Core.Tools.Compilers.Rules;
using Jinder.Poco.Models;

namespace Jinder.Core.Tools.Compilers
{
    public class SummaryCompiler : ICompiler<Summary>
    {
        public IEnumerable<Summary> Compile(IEnumerable<Summary> summaries, IRule<Summary> rule)
        {
            return summaries.Where(rule.IsSatisfied);
        }
    }
}
