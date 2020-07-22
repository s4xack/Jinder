using System;
using Jinder.Desktop.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Services
{
    public class SummarySuggestionService : ISummarySuggestionService
    {
        private readonly ISummarySuggestionClient _summarySuggestionClient;

        public SummarySuggestionService()
        {
            _summarySuggestionClient = RestService.For<ISummarySuggestionClient>(Session.HostUrl);
        }

        public SummarySuggestionDto GetForMe(Guid token)
        {
            try
            {
                return _summarySuggestionClient.Get(token).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Accept(Guid token, Int32 suggestionId)
        {
            _summarySuggestionClient.Accept(token, suggestionId).Wait();
        }

        public void Reject(Guid token, Int32 suggestionId)
        {
            _summarySuggestionClient.Reject(token, suggestionId).Wait();

        }

        public void Skip(Guid token, Int32 suggestionId)
        {
            _summarySuggestionClient.Skip(token, suggestionId).Wait();

        }
    }
}