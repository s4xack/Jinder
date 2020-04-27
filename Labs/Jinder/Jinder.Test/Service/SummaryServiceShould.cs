using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Api.Repository;
using Jinder.Api.Service;
using Jinder.Poco.Dto;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace Jinder.Test.Service
{
    public static class SummaryDtoExtension
    {
        public static Boolean EquivalentTo(this SummaryDto summary, SummaryDto other)
        {
            return summary.Id == other.Id &&
                   summary.UserId == other.UserId &&
                   summary.Skills.SequenceEqual(other.Skills) &&
                   summary.Specialization == other.Specialization &&
                   summary.Information == other.Information;
        }
    }
    [TestFixture]
    public class SummaryServiceShould
    {
        private ISummaryRepository _summaryRepository;
        private IUserRepository _userRepository;
        private ISkillRepository _skillRepository;
        private ISpecializationRepository _specializationRepository;
        private ISummaryService _summaryService;

        [SetUp]
        public void SummaryServiceSetUp()
        {
            _summaryRepository = new SummaryRepositoryMock();
            _userRepository = new UserRepositoryMock();
            _skillRepository = new SkillRepositoryMock();
            _specializationRepository = new SpecializationRepositoryMock();
            _summaryRepository = new SummaryRepositoryMock();
            _summaryService = new SummaryService(_summaryRepository, _userRepository, _skillRepository, _specializationRepository);
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
            for (var i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].EquivalentTo(expected[i]));
            }
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
            Assert.That(result.EquivalentTo(expected));
        }

        [Test]
        public void Should_return_summary_for_user_same_as_from_repository()
        {
            // Arrange
            var expected = SummaryDto.Create(_summaryRepository.GetForUser(1));

            // Act
            var result = _summaryService.GetForUser(1);

            // Assert
            Assert.That(result != null);
            Assert.That(result.EquivalentTo(expected));
        }

        [Test]
        public void Should_create_new_summary_for_user_in_repository()
        {
            // Arrange
            Int32 userId = 4;
            var summaryData = new CreateSummaryDto
            {
                Skills = new List<String> { "Skill1", "Skill3"},
                Specialization = "Spec2",
                Information = "Inform"
            };

            var expected = new SummaryDto
            {
                Id = 2,
                UserId = userId,
                Skills = summaryData.Skills,
                Specialization = summaryData.Specialization,
                Information = summaryData.Information
            };

            // Act
            var result = _summaryService.CreateForUser(userId, summaryData);

            // Assert
            Assert.That(result != null);
            Assert.That(result.EquivalentTo(expected));
            Assert.That(result.EquivalentTo(SummaryDto.Create(_summaryRepository.GetForUser(userId))));
        }

        [Test]
        public void Should_throw_argument_exception_when_try_to_create_summary_for_not_candidate_user()
        {
            // Arrange
            Int32 userId = 0;
            var summaryData = new CreateSummaryDto
            {
                Skills = new List<String> { "Skill1", "Skill3" },
                Specialization = "Spec2",
                Information = "Inform"
            };

            // Assert
            Assert.Throws<ArgumentException>(() => _summaryService.CreateForUser(userId, summaryData));
        }

        [Test]
        public void Should_throw_argument_exception_when_try_to_create_summary_for_user_who__already_have_summary()
        {
            // Arrange
            Int32 userId = 1;
            var summaryData = new CreateSummaryDto
            {
                Skills = new List<String> { "Skill1", "Skill3" },
                Specialization = "Spec2",
                Information = "Inform"
            };

            // Assert
            Assert.Throws<ArgumentException>(() => _summaryService.CreateForUser(userId, summaryData));
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
            Assert.That(result.EquivalentTo(expected));
            Assert.Throws<ArgumentException>(() => _summaryRepository.Get(summaryId));
        }
    }
}