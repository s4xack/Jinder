using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Jinder.Core.Services;
using Jinder.Dal.Repositories;
using Jinder.Dal.Repositories.Mocks;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Test.Services
{
    public static class VacancyDtoExtension
    {
        public static Boolean EquivalentTo(this VacancyDto vacancy, VacancyDto other)
        {
            return vacancy.Id == other.Id &&
                   vacancy.UserId == other.UserId &&
                   vacancy.Skills.SequenceEqual(other.Skills) &&
                   vacancy.Specialization == other.Specialization &&
                   vacancy.Information == other.Information;
        }
    }

    [TestFixture]
    public class VacancyServiceShould
    {
        [SetUp]
        public void VacancyServiceSetUp()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "0@email.com",
                    Id = 0,
                    Name = "Admin",
                    PasswordHash = string.Empty,
                    Type = UserType.Administrator
                },
                new User
                {
                    Email = "1@email.com",
                    Id = 1,
                    Name = "Ivan",
                    PasswordHash = string.Empty,
                    Type = UserType.Candidate
                },
                new User
                {
                    Email = "2@email.com",
                    Id = 2,
                    Name = "Shepherd",
                    PasswordHash = string.Empty,
                    Type = UserType.Recruiter
                },
                new User
                {
                    Email = "3@email.com",
                    Id = 3,
                    Name = "Alex",
                    PasswordHash = string.Empty,
                    Type = UserType.Candidate
                },
                new User
                {
                    Email = "4@email.com",
                    Id = 4,
                    Name = "Max",
                    PasswordHash = string.Empty,
                    Type = UserType.Candidate
                },
                new User
                {
                    Email = "5@email.com",
                    Id = 5,
                    Name = "Roman",
                    PasswordHash = string.Empty,
                    Type = UserType.Recruiter
                },
                new User
                {
                    Email = "6@email.com",
                    Id = 6,
                    Name = "Nick",
                    PasswordHash = string.Empty,
                    Type = UserType.Recruiter
                }
            };

            var skills = new List<Skill>
            {
                new Skill {Id = 0, Name = "Skill1"},
                new Skill {Id = 2, Name = "Skill2"},
                new Skill {Id = 3, Name = "Skill3"},
                new Skill {Id = 4, Name = "Skill4"}
            };

            var specializations = new List<Specialization>
            {
                new Specialization {Id = 0, Name = "Spec1"},
                new Specialization {Id = 1, Name = "Spec2"}
            };

            var vacancies = new List<Vacancy>
            {
                new Vacancy
                {
                    Id = 0,
                    UserId = 2,
                    Skills = new List<Skill> {new Skill {Id = 0, Name = "Skill1"}, new Skill {Id = 1, Name = "Skill2"}},
                    Specialization = new Specialization {Id = 0, Name = "Spec1"},
                    Information = "Info"
                },
                new Vacancy
                {
                    Id = 1,
                    UserId = 5,
                    Skills = new List<Skill> {new Skill {Id = 2, Name = "Skill3"}, new Skill {Id = 3, Name = "Skill4"}},
                    Specialization = new Specialization {Id = 1, Name = "Spec2"},
                    Information = "Info"
                }
            };

            _userRepository = new UserRepositoryMock(users);
            _skillRepository = new SkillRepositoryMock(skills);
            _specializationRepository = new SpecializationRepositoryMock(specializations);
            _vacancyRepository = new VacancyRepositoryMock(vacancies);
            _vacancyService = new VacancyService(_vacancyRepository, _userRepository, _skillRepository,
                _specializationRepository);
        }

        private IVacancyRepository _vacancyRepository;
        private IUserRepository _userRepository;
        private ISkillRepository _skillRepository;
        private ISpecializationRepository _specializationRepository;
        private IVacancyService _vacancyService;

        [Test]
        public void Should_create_new_vacancy_for_user_in_repository()
        {
            // Arrange
            var userId = 6;
            var vacancyData = new CreateVacancyDto(
                "Spec2",
                new List<String> { "Skill1", "Skill3" },
                "Inform");

            var expected = new VacancyDto(
                userId,
                2,
                vacancyData.Specialization,
                vacancyData.Skills,
                vacancyData.Information);

            // Act
            var result = _vacancyService.CreateForUser(userId, vacancyData);

            // Assert
            Assert.That(result != null);
            Assert.That(result.EquivalentTo(expected));
            Assert.That(result.EquivalentTo(VacancyDto.Create(_vacancyRepository.GetForUser(userId))));
        }

        [Test]
        public void Should_delete_vacancy_by_id_from_repository()
        {
            // Arrange
            var vacancyId = 0;
            var expected = _vacancyService.Get(vacancyId);

            // Act
            var result = _vacancyService.Delete(vacancyId);

            // Assert
            Assert.That(result.EquivalentTo(expected));
            Assert.Throws<ArgumentException>(() => _vacancyRepository.Get(vacancyId));
        }

        [Test]
        public void Should_return_all_summaries_same_as_from_repository()
        {
            // Arrange
            var expected = _vacancyRepository
                .GetAll()
                .Select(VacancyDto.Create)
                .ToList();

            // Act
            var result = _vacancyService.GetAll();

            // Assert
            Assert.That(result != null);
            Assert.That(result.Count == expected.Count);
            for (var i = 0; i < result.Count; i++) Assert.That(result[i].EquivalentTo(expected[i]));
        }

        [Test]
        public void Should_return_vacancy_by_id_same_as_from_repository()
        {
            // Arrange
            var vacancyId = 0;
            var expected = VacancyDto.Create(_vacancyRepository.Get(vacancyId));

            // Act
            var result = _vacancyService.Get(vacancyId);

            // Assert
            Assert.That(result != null);
            Assert.That(result.EquivalentTo(expected));
        }

        [Test]
        public void Should_return_vacancy_for_user_same_as_from_repository()
        {
            // Arrange
            var userId = 2;
            var expected = VacancyDto.Create(_vacancyRepository.GetForUser(userId));

            // Act
            var result = _vacancyService.GetForUser(userId);

            // Assert
            Assert.That(result != null);
            Assert.That(result.EquivalentTo(expected));
        }

        [Test]
        public void Should_throw_argument_exception_when_try_to_create_vacancy_for_not_recruiter_user()
        {
            // Arrange
            var userId = 0;
            var vacancyData = new CreateVacancyDto(
                "Spec2",
                new List<String> { "Skill1", "Skill3" },
                "Inform");

            // Assert
            Assert.Throws<ArgumentException>(() => _vacancyService.CreateForUser(userId, vacancyData));
        }

        [Test]
        public void Should_throw_argument_exception_when_try_to_create_vacancy_for_user_who__already_have_vacancy()
        {
            // Arrange
            var userId = 2;
            var vacancyData = new CreateVacancyDto(
                "Spec2",
                new List<String> { "Skill1", "Skill3" },
                "Inform");

            // Assert
            Assert.Throws<ArgumentException>(() => _vacancyService.CreateForUser(userId, vacancyData));
        }
    }
}