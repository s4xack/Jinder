using System;
using System.Collections.Generic;
using Jinder.Poco.Model;

namespace Jinder.Api.Repository
{
    public interface ISummaryRepository
    {
        public IEnumerable<Summary> GetAll();
        public Summary Get(Int32 summaryId);
        public Summary GetForUser(Int32 userId);
        public Summary Create(Summary summary);
        public Summary Delete(Int32 summaryId);
        public Boolean IsHaveForUser(Int32 userId);
    }
}