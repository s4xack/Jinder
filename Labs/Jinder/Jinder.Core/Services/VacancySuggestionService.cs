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
    public class VacancySuggestionService : IVacancySuggestionService
    {
        private readonly IVacancySuggestionRepository _vacancySuggestionRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IMatchService _matchService;

        public VacancySuggestionService(IVacancySuggestionRepository vacancySuggestionRepository,
            ISummaryRepository summaryRepository, IUserRepository userRepository, IVacancyRepository vacancyRepository, IMatchService matchService)
        {
            _vacancySuggestionRepository = vacancySuggestionRepository ?? throw new ArgumentException(nameof(vacancySuggestionRepository));
            _summaryRepository = summaryRepository ?? throw new ArgumentException(nameof(summaryRepository));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository)); ;
            _vacancyRepository = vacancyRepository ?? throw new ArgumentException(nameof(vacancyRepository));
            _matchService = matchService ?? throw new ArgumentException(nameof(matchService));
        }

        private Int32 GetSummaryIdForUser(Int32 userId)
        {
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Candidate)
                throw new ArgumentException($"User with id {userId} is not candidate!");

            Summary summary = _summaryRepository.GetForUser(userId);
            return summary.Id;
        }

        private IEnumerable<VacancySuggestion> GetAllForUser(Int32 userId)
        {
            Int32 summaryId = GetSummaryIdForUser(userId);

            IEnumerable<VacancySuggestion> suggestions = _vacancySuggestionRepository.GetAllForSummary(summaryId);
            if (!suggestions.Any())
                suggestions = CompileForSummary(summaryId);

            return suggestions;
        }

        private IEnumerable<VacancySuggestion> CompileForSummary(Int32 summaryId)
        {
            Summary summary = _summaryRepository.Get(summaryId);

            var compiler = new VacancyCompiler();
            var rule = new SimpleVacancyRule(summary.Specialization, summary.Skills);
            IEnumerable<Vacancy> summaries = compiler.Compile(_vacancyRepository.GetAll(), rule);

            IReadOnlyCollection<VacancySuggestion> vacancySuggestions =
                summaries.Select(s => new VacancySuggestion(_vacancySuggestionRepository.NewId, summaryId, s)).ToList();
            vacancySuggestions = _vacancySuggestionRepository.Add(vacancySuggestions).ToList();

            if (!vacancySuggestions.Any())
                throw new ArgumentException("It's no suggestion for summary! Change summary or try later.");

            return vacancySuggestions;
        }

        private IEnumerable<VacancySuggestion> ResetSkippedUser(Int32 userId)
        {
            IEnumerable<VacancySuggestion> vacancySuggestions =
                GetAllForUser(userId).Where(s => s.Status == SuggestionStatus.Skipped).ToList();
            foreach (var vacancySuggestion in vacancySuggestions)
                vacancySuggestion.Reset();
            return vacancySuggestions;
        }
        private IEnumerable<VacancySuggestion> GetAllReadyForUser(Int32 userId)
        {
            IEnumerable<VacancySuggestion> vacancySuggestions = GetAllForUser(userId)
                .Where(s => s.Status == SuggestionStatus.Ready).ToList();
            if (!vacancySuggestions.Any())
                ResetSkippedUser(userId);
            if (!vacancySuggestions.Any())
                throw new ArgumentException("It's no suggestion for summary! Change summary or try later.");
            return vacancySuggestions;
        }

        public List<VacancySuggestionDto> SuggestAllForUser(Int32 userId)
        {
            IEnumerable<VacancySuggestion> vacancySuggestions = GetAllReadyForUser(userId);
            return vacancySuggestions
                .Select(VacancySuggestionDto.Create)
                .ToList();
        }

        public VacancySuggestionDto SuggestForUser(Int32 userId)
        {
            var vacancySuggestions = GetAllReadyForUser(userId);
            return VacancySuggestionDto.Create(vacancySuggestions.FirstOrDefault());
        }

        public void AcceptSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Int32 summaryId = GetSummaryIdForUser(userId);

            VacancySuggestion suggestion = _vacancySuggestionRepository.Get(suggestionId);
            if (suggestion.SummaryId != summaryId)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Accept();
            _matchService.UpdateMatch(summaryId, suggestion.Vacancy.Id);
        }

        public void RejectSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Int32 summaryId = GetSummaryIdForUser(userId);

            VacancySuggestion suggestion = _vacancySuggestionRepository.Get(suggestionId);
            if (suggestion.SummaryId != summaryId)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Reject();
        }

        public void SkipSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Int32 summaryId = GetSummaryIdForUser(userId);

            VacancySuggestion suggestion = _vacancySuggestionRepository.Get(suggestionId);
            if (suggestion.SummaryId != summaryId)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Skip();
        }
    }
}