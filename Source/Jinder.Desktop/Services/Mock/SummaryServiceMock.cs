using System;
using Jinder.Desktop.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services.Mock
{
    public class SummaryServiceMock : ISummaryService
    {
        public SummaryDto GetForMe(Guid token)
        {
            return Candidate.Summary;
        }

        public SummaryDto CreateForMe(Guid token, CreateSummaryDto summary)
        {
            throw new NotImplementedException();
        }

        public SummaryDto DeleteForMe(Guid token)
        {
            throw new NotImplementedException();
        }
    }
}