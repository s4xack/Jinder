using System;
using System.Collections.Generic;
using System.Linq;

namespace Grouper
{
    public static class StringGrouper
    {
        public static IEnumerable<IGrouping<Int32, String>> GroupWordsByLength(String sentence)
        {
            return sentence
                .Split(new String[] {" - ", " ", ", ", ": ", ". ", "; ", "... "}, StringSplitOptions.None)
                .GroupBy(w => w.Length)
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.Key);
        }
    }
}
