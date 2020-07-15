using System;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services
{
    public interface IVacancyService
    {
        VacancyDto GetForMe(Guid token);
        VacancyDto CreateForMe(Guid token, CreateVacancyDto vacancy);
        VacancyDto DeleteForMe(Guid token);
    }
}