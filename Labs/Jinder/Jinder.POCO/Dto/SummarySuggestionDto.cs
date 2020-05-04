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
        public Int32 Id { get; }
        public SummaryDto Summary { get; }
        public SuggestionStatus Status { get; }

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
