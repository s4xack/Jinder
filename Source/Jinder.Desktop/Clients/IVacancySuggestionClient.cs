using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface IVacancySuggestionClient
    {
        [Get("/api/vacancySuggestion/get")]
        Task<VacancySuggestionDto> Get([Header("token")] Guid token);

        [Get("/api/vacancySuggestion/get/accept/{suggestionId}")]
        Task Accept([Header("token")] Guid token, Int32 suggestionId);

        [Get("/api/vacancySuggestion/get/reject/{suggestionId}")]
        Task Reject([Header("token")] Guid token, Int32 suggestionId);

        [Get("/api/vacancySuggestion/get/skip/{suggestionId}")]
        Task Skip([Header("token")] Guid token, Int32 suggestionId);
    }
}