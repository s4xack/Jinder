using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface ISpecializationService
    {
        IReadOnlyCollection<SpecializationDto> GetAll();
        SpecializationDto Create(SpecializationDto specialization);
        SpecializationDto DeleteByName(String specializationName);
    }
}