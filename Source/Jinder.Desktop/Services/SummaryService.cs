using System;
using Jinder.Desktop.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryClient _summaryClient;

        public SummaryService()
        {
            _summaryClient = RestService.For<ISummaryClient>(Session.HostUrl);
        }

        public SummaryDto GetForMe(Guid token)
        {
            return _summaryClient.GetForMe(token).Result;
        }

        public SummaryDto CreateForMe(Guid token, CreateSummaryDto summary)
        {
            return _summaryClient.Create(token, summary).Result;
        }

        public SummaryDto DeleteForMe(Guid token)
        {
            throw new NotImplementedException();
        }
    }
}