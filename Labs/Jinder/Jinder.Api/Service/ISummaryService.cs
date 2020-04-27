using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Api.Service
{
    public interface ISummaryService
    {
        public List<SummaryDto> GetAll();
        public SummaryDto Get(Int32 summaryId);
        public SummaryDto GetForUser(Int32 userId);
        public SummaryDto CreateForUser(Int32 userId, CreateSummaryDto summary);
        public SummaryDto Delete(Int32 summaryId);
    }
}