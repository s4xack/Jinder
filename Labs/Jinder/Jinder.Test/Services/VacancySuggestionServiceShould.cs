using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Core.Services;
using Jinder.Core.Tools.Compilers;
using Jinder.Core.Tools.Compilers.Rules;
using Jinder.Dal.Repositories;
using Jinder.Dal.Repositories.Mocks;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Moq;
using NUnit.Framework;

namespace Jinder.Test.Services
{
    public class VacancySuggestionServiceShould
    {
        private IUserRepository _userRepository;
        private ISummaryRepository _summaryRepository;
        private IVacancyRepository _vacancyRepository;
        private IVacancySuggestionRepository _vacancySuggestionRepository;
        private IVacancySuggestionService _vacancySuggestionService;
        private IRule<Vacancy> _rule;

        [SetUp]
        public void SummarySuggestionServiceSetUp()
        {
            var user = new User(0, "", "", "", UserType.Candidate);
            var specializations = new List<Specialization>() { new Specialization(0, "Spec1"), new Specialization(1, "Spec2") };
            var summary = new Summary(0, 0, specializations[0], new List<Skill>(), "");
            var vacancies = new List<Vacancy>();
            for (int i = 0; i < 10; i++)
                vacancies.Add(new Vacancy(i + 1, i, specializations[i % 2], new List<Skill>(), ""));

            _userRepository = Mock.Of<IUserRepository>(
                x => x.Get(0) == user);
            _summaryRepository = Mock.Of<ISummaryRepository>(
                x => x.GetForUser(0) == summary && x.Get(0) == summary);
            _vacancyRepository = Mock.Of<IVacancyRepository>(
                x => x.GetAll() == vacancies);
            _vacancySuggestionRepository = new VacancySuggestionRepositoryMock(new List<VacancySuggestion>());

            _vacancySuggestionService = new VacancySuggestionService(_vacancySuggestionRepository, _summaryRepository, _userRepository, _vacancyRepository, Mock.Of<IMatchService>());

            _rule = new SimpleVacancyRule(specializations[0], new List<Skill>());
        }

        [Test]
        public void Should_create_suggestions_for_user_when_it_have_not_in_repository()
        {
            // Arrange
            var compiler = new VacancyCompiler();
            var expected = compiler.Compile(_vacancyRepository.GetAll(), _rule);

            // Act
            var result = _vacancySuggestionService.SuggestAllForUser(0);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expected.Count()));
            Assert.That(result.Select(s => s.Vacancy.Id), Is.EquivalentTo(expected.Select(s => s.Id)));
        }

        [Test]
        public void Should_decrease_suggestions_count_after_accepting()
        {
            // Arrange
            var expected = _vacancySuggestionService.SuggestAllForUser(0).Count - 1;

            // Act
            var suggestion = _vacancySuggestionService.SuggestForUser(0);
            _vacancySuggestionService.AcceptSuggestionForUser(0, suggestion.Id);
            var result = _vacancySuggestionService.SuggestAllForUser(0).Count;

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_throw_argument_exception_when_all_suggestions_accepted_and_try_to_get_one_more()
        {
            // Arrange
            var count = _vacancySuggestionService.SuggestAllForUser(0).Count;

            // Act
            for (int i = 0; i < count; i++)
            {
                var suggestion = _vacancySuggestionService.SuggestForUser(0);
                _vacancySuggestionService.AcceptSuggestionForUser(0, suggestion.Id);
            }

            // Assert
            Assert.Throws<ArgumentException>(() => _vacancySuggestionService.SuggestForUser(0));
        }

        [Test]
        public void Should_reset_only_skipped_suggestions_after_all_rated()
        {
            // Arrange
            var count = _vacancySuggestionService.SuggestAllForUser(0).Count;
            var expected = count - 1;

            // Act
            var suggestion = _vacancySuggestionService.SuggestForUser(0);
            _vacancySuggestionService.AcceptSuggestionForUser(0, suggestion.Id);
            for (int i = 0; i < count - 1; i++)
            {
                suggestion = _vacancySuggestionService.SuggestForUser(0);
                _vacancySuggestionService.SkipSuggestionForUser(0, suggestion.Id);
            }
            var result = _vacancySuggestionService.SuggestAllForUser(0).Count;

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}