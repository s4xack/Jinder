using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface IVacancyService
    {
        List<VacancyDto> Get();
        VacancyDto Get(Int32 vacancyId);
        VacancyDto GetForUser(Int32 vacancyId);
        VacancyDto CreateForUser(Int32 userId, CreateVacancyDto vacancyData);
        VacancyDto Delete(Int32 vacancyId);
    }
}
