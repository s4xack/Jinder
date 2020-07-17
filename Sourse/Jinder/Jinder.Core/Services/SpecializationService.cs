using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.Repositories;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;

namespace Jinder.Core.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository ??
                                        throw new ArgumentNullException(nameof(specializationRepository));
        }

        public IReadOnlyCollection<SpecializationDto> GetAll()
        {
            return _specializationRepository.GetAll()
                .Select(SpecializationDto.Create)
                .ToList();
        }

        public SpecializationDto Create(SpecializationDto specialization)
        {
            Specialization newSpecialization = _specializationRepository.Add(new Specialization(specialization.Name));

            return SpecializationDto.Create(newSpecialization);
        }

        public SpecializationDto DeleteByName(String specializationName)
        {
            return SpecializationDto.Create(_specializationRepository.DeleteByName(specializationName));
        }
    }
}