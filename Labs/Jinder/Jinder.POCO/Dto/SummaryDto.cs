using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class SummaryDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Specialization { get; set; }
        public List<string> Skills { get; set; }
        public string Information { get; set; }
    }   
}
