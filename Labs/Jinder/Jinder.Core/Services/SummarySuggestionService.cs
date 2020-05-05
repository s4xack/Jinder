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

        private Int32 GetVacancyIdForUser(Int32 userId)
        {
            var user = _userRepository.Get(userId);
            if (user.Type != UserType.Recruiter)
                throw new ArgumentException($"User with id {userId} is not recruiter!");
            var vacancy = _vacancyRepository.GetForUser(userId);
            return vacancy.Id;
        }

        private IEnumerable<SummarySuggestion> GetAllForUser(Int32 userId)
        {
            var vacancyId = GetVacancyIdForUser(userId);
            var suggestions = _summarySuggestionRepository.GetAllForVacancy(vacancyId);
            if (!suggestions.Any())
                suggestions = CompileForVacancy(vacancyId);
            return suggestions;
        }

        private IEnumerable<SummarySuggestion> CompileForVacancy(Int32 vacancyId)
        {
            var vacancy = _vacancyRepository.Get(vacancyId);

            var compiler = new SummaryCompiler();
            var rule = new SimpleSummaryRule(vacancy.Specialization, vacancy.Skills);
            var summaries = compiler.Compile(_summaryRepository.GetAll(), rule);

            var summarySuggestions =
                summaries.Select(s => new SummarySuggestion(_summarySuggestionRepository.NewId, vacancyId, s));
            summarySuggestions = _summarySuggestionRepository.Add(summarySuggestions);

            if (!summarySuggestions.Any())
                throw new ArgumentException("It's no suggestion for vacancy! Change vacancy or try later.");

            return summarySuggestions;
        }

        private IEnumerable<SummarySuggestion> ResetSkippedUser(Int32 userId)
        {
            var summarySuggestions =
                GetAllForUser(userId).Where(s => s.Status == SuggestionStatus.Skipped);
            foreach (var summarySuggestion in summarySuggestions)
                summarySuggestion.Reset();
            return summarySuggestions;
        }
        private IEnumerable<SummarySuggestion> GetAllReadyForUser(Int32 userId)
        {
            var summarySuggestions = GetAllForUser(userId)
                .Where(s => s.Status == SuggestionStatus.Ready);
            if (!summarySuggestions.Any())
                ResetSkippedUser(userId);
            if (!summarySuggestions.Any())
                throw new ArgumentException("It's no suggestion for vacancy! Change vacancy or try later.");
            return summarySuggestions;
        }

        public List<SummarySuggestionDto> SuggestAllForUser(Int32 userId)
        {
            var summarySuggestions = GetAllReadyForUser(userId);
            return summarySuggestions
                .Select(SummarySuggestionDto.Create)
                .ToList();
        }

        public SummarySuggestionDto SuggestForUser(Int32 userId)
        {
            var summarySuggestions = GetAllReadyForUser(userId);
            return SummarySuggestionDto.Create(summarySuggestions.FirstOrDefault());
        }

        public void AcceptSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            var vacancyId = GetVacancyIdForUser(userId);
            var suggestion = _summarySuggestionRepository.Get(suggestionId);
            if (suggestion.VacancyId != vacancyId)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");
            suggestion.Accept();
            _matchService.UpdateMatch(suggestion.Summary.Id, vacancyId);
        }

        public void RejectSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            var vacancyId = GetVacancyIdForUser(userId);
            var suggestion = _summarySuggestionRepository.Get(suggestionId);
            if (suggestion.VacancyId != vacancyId)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");
            suggestion.Reject();
        }

        public void SkipSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            var vacancyId = GetVacancyIdForUser(userId);
            var suggestion = _summarySuggestionRepository.Get(suggestionId);
            if (suggestion.VacancyId != vacancyId)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");
            suggestion.Skip();
        }
    }
}