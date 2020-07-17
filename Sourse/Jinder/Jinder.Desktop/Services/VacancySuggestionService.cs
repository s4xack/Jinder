using System;
using System.Threading.Tasks;
using Jinder.Desktop.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Services
{
    public class VacancySuggestionService : IVacancySuggestionService
    {
        private readonly IVacancySuggestionClient _vacancySuggestionClient;

        public VacancySuggestionService()
        {
            _vacancySuggestionClient = RestService.For<IVacancySuggestionClient>(Session.HostUrl);
        }

        public VacancySuggestionDto GetForMe(Guid token)
        {
            try
            {
                return _vacancySuggestionClient.Get(token).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Accept(Guid token, Int32 suggestionId)
        {
            _vacancySuggestionClient.Accept(token, suggestionId).Wait();
        }

        public void Reject(Guid token, Int32 suggestionId)
        {
            _vacancySuggestionClient.Reject(token, suggestionId).Wait();
        }

        public void Skip(Guid token, Int32 suggestionId)
        {
            _vacancySuggestionClient.Skip(token, suggestionId).Wait();
        }
    }
}