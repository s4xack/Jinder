using System;
using System.Collections.Generic;
using System.Linq;

namespace Joiner
{
    public static class NameJoiner
    {
        public static String JoinNames(IEnumerable<INamed> elements, String delimiter)
        {
            return elements
                .Select(e => e.Name)
                .Aggregate((a, b) => a + delimiter + b);
        }
    }
}