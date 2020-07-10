using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class SummarySuggestionDto
    {
        public Int32 Id { get; set; }
        public SummaryDto Summary { get; set; }
        public SuggestionStatus Status { get; set; }

        [JsonConstructor]
        public SummarySuggestionDto(Int32 id, SummaryDto summary, SuggestionStatus status)
        {
            Id = id;
            Summary = summary;
            Status = status;
        }

        public static SummarySuggestionDto Create(SummarySuggestion summarySuggestion)
        {
            return new SummarySuggestionDto(
                summarySuggestion.Id,
                SummaryDto.Create(summarySuggestion.Summary),
                summarySuggestion.Status);
        }
    }
}
