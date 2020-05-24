using System;
using Jinder.Desktop.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services.Mock
{
    public class VacancyServiceMock : IVacancyService
    {
        public VacancyDto GetForMe(Guid token)
        {
            return Recruiter.Vacancy;
        }

        public VacancyDto CreateForMe(Guid token)
        {
            throw new NotImplementedException();
        }

        public VacancyDto DeleteForMe(Guid token)
        {
            throw new NotImplementedException();
        }
    }
}