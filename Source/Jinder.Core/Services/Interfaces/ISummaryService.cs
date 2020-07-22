using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface ISummaryService
    {
        List<SummaryDto> GetAll();
        SummaryDto Get(Int32 summaryId);
        SummaryDto GetForUser(Int32 userId);
        SummaryDto CreateForUser(Int32 userId, CreateSummaryDto summary);
        SummaryDto Delete(Int32 summaryId);
    }
}