using System.Collections.Generic;
using Jinder.Core.Services;
using Jinder.Dal.Repositories;
using Jinder.Dal.Repositories.Mocks;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Moq;
using NUnit.Framework;
using Match = Jinder.Poco.Models.Match;

namespace Jinder.Test.Services
{
    [TestFixture]
    public class MatchServiceShould
    {
        private IMatchService _matchService;

        [SetUp]
        public void MatchServiceSetUp()
        {
            var candidate = new User("", "", UserType.Candidate);
            var recruiter = new User("", "", UserType.Recruiter);
            var specialization = new Specialization("spec");
            var summary = new Summary(candidate, specialization, new List<Skill>(), "");
            var vacancy = new Vacancy(recruiter, specialization, new List<Skill>(), "");
            var matchRepository = new MatchRepositoryMock(new List<Match>());
            var userRepository = Mock.Of<IUserRepository>(x => 
                x.Get(0) == candidate);
            var summaryRepository = Mock.Of<ISummaryRepository>(x =>
                x.GetForUser(0) == summary &&
                x.Get(0) == summary);
            var vacancyRepository = Mock.Of<IVacancyRepository>(x =>
                x.Get(0) == vacancy);
            _matchService = new MatchService(matchRepository, userRepository, summaryRepository, vacancyRepository);
        }

        [Test]
        public void Should_create_full_match_after_double_match_update()
        {
            // Act
            _matchService.UpdateMatch(0, 0);
            _matchService.UpdateMatch(0, 0);
            var result = _matchService.GetAllForCandidate(0);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(0));
        }

    }
}