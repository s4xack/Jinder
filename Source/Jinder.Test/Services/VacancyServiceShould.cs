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
    [TestFixture]
    public class VacancyServiceShould
    {
        [SetUp]
        public void VacancyServiceSetUp()
        {
            var users = new List<User>
            {
                new User(
                    "0@email.com",
                    "Admin",
                    UserType.Administrator),
                new User(
                    "1@email.com",
                    "Ivan",
                    UserType.Candidate),
                new User(
                    "2@email.com",
                    "Shepherd",
                    UserType.Recruiter),
                new User(
                    "3@email.com",
                    "Alex",
                    UserType.Candidate),
                new User(
                    "4@email.com",
                    "Max",
                    UserType.Candidate),
                new User(
                    "5@email.com",
                    "Roman",
                    UserType.Recruiter),
                new User(
                    "6@email.com",
                    "Nick",
                    UserType.Recruiter)
            };
            var skills = new List<Skill>
            {
                new Skill ("Skill1"),
                new Skill ("Skill2"),
                new Skill ("Skill3"),
                new Skill ("Skill4")
            };
            var specializations = new List<Specialization>
            {
                new Specialization ("Spec1"),
                new Specialization ("Spec2")
            };
            var vacancies = new List<Vacancy>
            {
                new Vacancy(
                    users[2],
                    new Specialization("Spec1"),
                    new List<Skill> {new Skill("Skill1"), new Skill("Skill2")},
                    "Info"),
                new Vacancy(
                    users[5],
                    new Specialization( "Spec2"),
                    new List<Skill> {new Skill("Skill3"), new Skill("Skill4")},
                    "Info")
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
            Assert.That(result.Equals(expected));
            Assert.That(result.Equals(VacancyDto.Create(_vacancyRepository.GetForUser(userId))));
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
            Assert.That(result.Equals(expected));
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
            for (var i = 0; i < result.Count; i++) Assert.That(result[i].Equals(expected[i]));
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
            Assert.That(result.Equals(expected));
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
            Assert.That(result.Equals(expected));
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