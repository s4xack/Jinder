using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface ISpecializationClient
    {
        [Get("/api/specialization/get/all")]
        Task<List<SpecializationDto>> GetAll([Header("token")] Guid token);
    }
}