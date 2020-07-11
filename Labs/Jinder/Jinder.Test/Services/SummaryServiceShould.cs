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
    public class SummaryServiceShould
    {
        [SetUp]
        public void SummaryServiceSetUp()
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
            var summaries = new List<Summary>
            {
                new Summary(
                    users[1],
                    new Specialization("Spec1"),
                    new List<Skill> {new Skill("Skill1"), new Skill("Skill2")},
                    "Info"),
                new Summary(
                    users[3],
                    new Specialization("Spec2"),
                    new List<Skill> {new Skill("Skill3"), new Skill("Skill4")},
                    "Info"),
            };

            _userRepository = new UserRepositoryMock(users);
            _skillRepository = new SkillRepositoryMock(skills);
            _specializationRepository = new SpecializationRepositoryMock(specializations);
            _summaryRepository = new SummaryRepositoryMock(summaries);
            _summaryService = new SummaryService(_summaryRepository, _userRepository, _skillRepository,
                _specializationRepository);
        }

        private ISummaryRepository _summaryRepository;
        private IUserRepository _userRepository;
        private ISkillRepository _skillRepository;
        private ISpecializationRepository _specializationRepository;
        private ISummaryService _summaryService;

        [Test]
        public void Should_create_new_summary_for_user_in_repository()
        {
            // Arrange
            var userId = 4;
            var summaryData = new CreateSummaryDto(
                "Spec2",
                new List<String> {"Skill1", "Skill3"},
                "Inform");

            var expected = new SummaryDto(
                userId,
                2,
                summaryData.Specialization,
                summaryData.Skills,
                summaryData.Information);

            // Act
            var result = _summaryService.CreateForUser(userId, summaryData);

            // Assert
            Assert.That(result != null);
            Assert.That(result.Equals(expected));
            Assert.That(result.Equals(SummaryDto.Create(_summaryRepository.GetForUser(userId))));
        }

        [Test]
        public void Should_delete_summary_by_id_from_repository()
        {
            // Arrange
            var summaryId = 0;
            var expected = _summaryService.Get(summaryId);

            // Act
            var result = _summaryService.Delete(summaryId);

            // Assert
            Assert.That(result.Equals(expected));
            Assert.Throws<ArgumentException>(() => _summaryRepository.Get(summaryId));
        }

        [Test]
        public void Should_return_all_summaries_same_as_from_repository()
        {
            // Arrange
            var expected = _summaryRepository
                .GetAll()
                .Select(SummaryDto.Create)
                .ToList();

            // Act
            var result = _summaryService.GetAll();

            // Assert
            Assert.That(result != null);
            Assert.That(result.Count == expected.Count);
            for (var i = 0; i < result.Count; i++) Assert.That(result[i].Equals(expected[i]));
        }

        [Test]
        public void Should_return_summary_by_id_same_as_from_repository()
        {
            // Arrange
            var summaryId = 0;
            var expected = SummaryDto.Create(_summaryRepository.Get(summaryId));

            // Act
            var result = _summaryService.Get(summaryId);

            // Assert
            Assert.That(result != null);
            Assert.That(result.Equals(expected));
        }

        [Test]
        public void Should_return_summary_for_user_same_as_from_repository()
        {
            // Arrange
            var userId = 1;
            var expected = SummaryDto.Create(_summaryRepository.GetForUser(userId));

            // Act
            var result = _summaryService.GetForUser(userId);

            // Assert
            Assert.That(result != null);
            Assert.That(result.Equals(expected));
        }

        [Test]
        public void Should_throw_argument_exception_when_try_to_create_summary_for_not_candidate_user()
        {
            // Arrange
            var userId = 0;
            var summaryData = new CreateSummaryDto(
                "Spec2",
                new List<String> { "Skill1", "Skill3" },
                "Inform");

            // Assert
            Assert.Throws<ArgumentException>(() => _summaryService.CreateForUser(userId, summaryData));
        }

        [Test]
        public void Should_throw_argument_exception_when_try_to_create_summary_for_user_who__already_have_summary()
        {
            // Arrange
            var userId = 1;
            var summaryData = new CreateSummaryDto(
                "Spec2",
                new List<String> { "Skill1", "Skill3" },
                "Inform");

            // Assert
            Assert.Throws<ArgumentException>(() => _summaryService.CreateForUser(userId, summaryData));
        }
    }
}