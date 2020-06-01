using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface IMatchRepository
    {
        Match Get(Int32 matchId);
        IReadOnlyCollection<Match> GetAllForSummary(Int32 summaryId);
        IReadOnlyCollection<Match> GetAllForVacancy(Int32 vacancyId);
        Match Add(Match match);
    }
}