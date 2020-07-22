using System;
using Jinder.Desktop.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyClient _vacancyClient;

        public VacancyService()
        {
            _vacancyClient = RestService.For<IVacancyClient>(Session.HostUrl);
        }

        public VacancyDto GetForMe(Guid token)
        {
            return _vacancyClient.GetForMe(token).Result;
        }

        public VacancyDto CreateForMe(Guid token, CreateVacancyDto vacancy)
        {
            return _vacancyClient.Create(token, vacancy).Result;
        }

        public VacancyDto DeleteForMe(Guid token)
        {
            throw new NotImplementedException();
        }
    }
}