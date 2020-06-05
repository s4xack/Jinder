using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter
{
    public static class NameFilter
    {
        public static IEnumerable<INamed> FilterByPosition(IEnumerable<INamed> elements)
        {
            return elements
                .Where((element, position) => element.Name.Length > position);
        }
    }
}
