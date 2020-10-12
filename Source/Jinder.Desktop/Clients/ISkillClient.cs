using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface ISkillClient
    {
        [Get("/api/skill/get/all")]
        Task<List<SkillDto>> Get([Header("token")] Guid token);
    }
}