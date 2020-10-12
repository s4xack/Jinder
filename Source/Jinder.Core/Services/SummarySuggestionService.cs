using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Core.Tools.Compilers;
using Jinder.Core.Tools.Compilers.Rules;
using Jinder.Dal.Repositories;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Core.Services
{
    public class SummarySuggestionService : ISummarySuggestionService
    {
        private readonly ISummarySuggestionRepository _summarySuggestionRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IMatchService _matchService;

        public SummarySuggestionService(ISummarySuggestionRepository summarySuggestionRepository,
            IVacancyRepository vacancyRepository, IUserRepository userRepository, ISummaryRepository summaryRepository, IMatchService matchService)
        {
            _summarySuggestionRepository = summarySuggestionRepository ?? throw new ArgumentException(nameof(summarySuggestionRepository));
            _vacancyRepository = vacancyRepository ?? throw new ArgumentException(nameof(vacancyRepository));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository)); ;
            _summaryRepository = summaryRepository ?? throw new ArgumentException(nameof(summaryRepository));
            _matchService = matchService ?? throw new ArgumentException(nameof(matchService));
        }

        private Vacancy GetVacancyForUser(Int32 userId)
        {
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Recruiter)
                throw new ArgumentException($"User with id {userId} is not recruiter!");

            Vacancy vacancy = _vacancyRepository.GetForUser(userId);
            return vacancy;
        }

        private IEnumerable<SummarySuggestion> CompileForVacancy(Int32 vacancyId)
        {
            Vacancy vacancy = _vacancyRepository.Get(vacancyId);

            var compiler = new SummaryCompiler();
            var rule = new SimpleSummaryRule(vacancy.Specialization, vacancy.Skills);
            IEnumerable<Summary> summaries = compiler.Compile(_summaryRepository.Get(), rule);

            IReadOnlyCollection<SummarySuggestion> summarySuggestions =
                summaries.Select(s => new SummarySuggestion(vacancy, s)).ToList();
            summarySuggestions = _summarySuggestionRepository.Add(summarySuggestions).ToList();
            if (!summarySuggestions.Any())
                throw new ArgumentException("It's no suggestion for vacancy! Change vacancy or try later.");

            return summarySuggestions;
        }

        private IEnumerable<SummarySuggestion> ResetSkippedUser(Int32 userId)
        {
            Vacancy vacancy = GetVacancyForUser(userId);
            IEnumerable<SummarySuggestion> summarySuggestions = _summarySuggestionRepository.GetForVacancyByState(vacancy.Id, SuggestionStatus.Skipped).ToList();
            foreach (var summarySuggestion in summarySuggestions)
            {
                summarySuggestion.Reset();
                _summarySuggestionRepository.Update(summarySuggestion);
            }
            return summarySuggestions;
        }
        private IEnumerable<SummarySuggestion> GetAllReadyForUser(Int32 userId)
        {
            Vacancy vacancy = GetVacancyForUser(userId);
            IEnumerable<SummarySuggestion> summarySuggestions = _summarySuggestionRepository
                .GetForVacancyByState(vacancy.Id, SuggestionStatus.Ready);

            if (!summarySuggestions.Any())
            {
                if (!_summarySuggestionRepository.GetAllForVacancy(vacancy.Id).Any())
                    CompileForVacancy(vacancy.Id);
                else
                    ResetSkippedUser(userId);
                summarySuggestions = _summarySuggestionRepository
                    .GetForVacancyByState(vacancy.Id, SuggestionStatus.Ready);
            }

            if (!summarySuggestions.Any())
                throw new ArgumentException("It's no suggestion for vacancy! Change vacancy or try later.");
            return summarySuggestions;
        }

        public List<SummarySuggestionDto> SuggestAllForUser(Int32 userId)
        {
            IEnumerable<SummarySuggestion> summarySuggestions = GetAllReadyForUser(userId);
            return summarySuggestions
                .Select(SummarySuggestionDto.Create)
                .ToList();
        }

        public SummarySuggestionDto SuggestForUser(Int32 userId)
        {
            IEnumerable<SummarySuggestion> summarySuggestions = GetAllReadyForUser(userId);
            return SummarySuggestionDto.Create(summarySuggestions.FirstOrDefault());
        }

        public void AcceptSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Vacancy vacancy = GetVacancyForUser(userId);

            SummarySuggestion suggestion = _summarySuggestionRepository.Get(suggestionId);
            if (suggestion.Vacancy.Id != vacancy.Id)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Accept();
            _summarySuggestionRepository.Update(suggestion);
            _matchService.UpdateMatch(suggestion.Summary.Id, vacancy.Id);
        }

        public void RejectSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Vacancy vacancy = GetVacancyForUser(userId);

            SummarySuggestion suggestion = _summarySuggestionRepository.Get(suggestionId);
            if (suggestion.Vacancy.Id != vacancy.Id)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Reject();
            _summarySuggestionRepository.Update(suggestion);
        }

        public void SkipSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Vacancy vacancy = GetVacancyForUser(userId);

            SummarySuggestion suggestion = _summarySuggestionRepository.Get(suggestionId);
            if (suggestion.Vacancy.Id != vacancy.Id)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Skip();
            _summarySuggestionRepository.Update(suggestion);
        }
    }
}