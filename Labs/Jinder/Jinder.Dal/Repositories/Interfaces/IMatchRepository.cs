using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface IMatchRepository
    {
        Match Get(Int32 matchId);
        IEnumerable<Match> GetAllForSummary(Int32 summaryId);
        IEnumerable<Match> GetAllForVacancy(Int32 vacancyId);
        Match Add(Match match);
        Int32 NewId { get; } 
    }
}