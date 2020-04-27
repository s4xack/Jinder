using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface ISummaryRepository
    {
        IEnumerable<Summary> GetAll();
        Summary Get(Int32 summaryId);
        Summary GetForUser(Int32 userId);
        Summary Create(Summary summary);
        Summary Delete(Int32 summaryId);
        Boolean IsHaveForUser(Int32 userId);
    }
}