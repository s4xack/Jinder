using System;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    interface ISummaryClient
    {
        [Get("/api/summary/getForUser/me")]
        Task<SummaryDto> GetForMe([Header("token")] Guid token);

        [Post("/api/summary/create")]
        Task<SummaryDto> Create([Header("token")] Guid token, [Body] CreateSummaryDto summary);
    }
}