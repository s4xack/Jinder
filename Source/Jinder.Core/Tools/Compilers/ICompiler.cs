using System.Collections.Generic;
using Jinder.Core.Tools.Compilers.Rules;

namespace Jinder.Core.Tools.Compilers
{
    public interface ICompiler<T>
    {
        IEnumerable<T> Compile(IEnumerable<T> items, IRule<T> rule);
    }
}