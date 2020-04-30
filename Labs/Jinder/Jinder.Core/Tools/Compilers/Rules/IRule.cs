using System;

namespace Jinder.Core.Tools.Compilers.Rules
{
    public interface IRule<T>
    {
        Boolean IsSatisfied(T item);
    }
}