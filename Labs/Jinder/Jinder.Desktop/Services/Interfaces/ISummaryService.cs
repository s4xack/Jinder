using System;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services
{
    public interface ISummaryService
    {
        SummaryDto GetForMe(Guid token);
        SummaryDto CreateForMe(Guid token, CreateSummaryDto summary);
        SummaryDto DeleteForMe(Guid token);
    }
}