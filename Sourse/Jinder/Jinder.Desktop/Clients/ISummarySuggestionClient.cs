using System;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface ISummarySuggestionClient
    {
        [Get("/api/summarySuggestion/get")]
        Task<SummarySuggestionDto> Get([Header("token")] Guid token);

        [Get("/api/summarySuggestion/get/accept/{suggestionId}")]
        Task Accept([Header("token")] Guid token, Int32 suggestionId);

        [Get("/api/summarySuggestion/get/reject/{suggestionId}")]
        Task Reject([Header("token")] Guid token, Int32 suggestionId);

        [Get("/api/summarySuggestion/get/skip/{suggestionId}")]
        Task Skip([Header("token")] Guid token, Int32 suggestionId);
    }
}