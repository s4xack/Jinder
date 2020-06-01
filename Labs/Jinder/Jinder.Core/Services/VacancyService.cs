using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.Repositories;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Core.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUserRepository _userRepository;

        public VacancyService(IVacancyRepository vacancyRepository,
            IUserRepository userRepository,
            ISkillRepository skillRepository,
            ISpecializationRepository specializationRepository)
        {
            _vacancyRepository = vacancyRepository ?? throw new ArgumentException(nameof(vacancyRepository));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _skillRepository = skillRepository ?? throw new ArgumentException(nameof(skillRepository));
            _specializationRepository =
                specializationRepository ?? throw new ArgumentException(nameof(skillRepository));
        }

        public List<VacancyDto> GetAll()
        {
            return _vacancyRepository
                .GetAll()
                .Select(VacancyDto.Create)
                .ToList();
        }

        public VacancyDto Get(Int32 vacancyId)
        {
            return VacancyDto.Create(_vacancyRepository.Get(vacancyId));
        }

        public VacancyDto GetForUser(Int32 userId)
        {
            return VacancyDto.Create(_vacancyRepository.GetForUser(userId));
        }

        public VacancyDto CreateForUser(Int32 userId, CreateVacancyDto vacancyData)
        {
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Recruiter)
                throw new ArgumentException($"Unable to create recruiter for not candidate user with id {userId}!");
            if (_vacancyRepository.IsHaveForUser(userId))
                throw new ArgumentException($"Vacancy for user with id {userId} have already created!");

            var vacancy = new Vacancy(
                userId,
                _specializationRepository.GetByName(vacancyData.Specialization),
                vacancyData.Skills
                    .Select(s => _skillRepository.GetByName(s))
                    .ToList(),
                vacancyData.Information);
            vacancy = _vacancyRepository.Create(vacancy);

            return VacancyDto.Create(vacancy);
        }

        public VacancyDto Delete(Int32 vacancyId)
        {
            return VacancyDto.Create(_vacancyRepository.Delete(vacancyId));
        }
    }
}