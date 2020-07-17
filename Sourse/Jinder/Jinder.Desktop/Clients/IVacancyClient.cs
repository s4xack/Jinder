using System;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface IVacancyClient
    {
        [Get("/api/vacancy/getForUser/me")]
        Task<VacancyDto> GetForMe([Header("token")] Guid token);

        [Post("/api/vacancy/create")]
        Task<VacancyDto> Create([Header("token")] Guid token, [Body] CreateVacancyDto vacancy);
    }
}