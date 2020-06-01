using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface IVacancyRepository
    {
        IReadOnlyCollection<Vacancy> GetAll();
        Vacancy Get(Int32 vacancyId);
        Vacancy GetForUser(Int32 userId);
        Vacancy Create(Vacancy vacancy);
        Vacancy Delete(Int32 vacancyId);
        Boolean IsHaveForUser(Int32 userId);
        Int32 NewId { get; }
    }
}