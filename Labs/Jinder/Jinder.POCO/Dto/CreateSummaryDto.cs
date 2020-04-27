using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class CreateSummaryDto
    {
        public String Specialization { get; set; }
        public List<String> Skills { get; set; }
        public String Information { get; set; }
    }
}