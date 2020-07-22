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

        private Summary GetSummaryForUser(Int32 userId)
        {
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Candidate)
                throw new ArgumentException($"User with id {userId} is not candidate!");

            Summary summary = _summaryRepository.GetForUser(userId);
            return summary;
        }

        private IEnumerable<VacancySuggestion> CompileForSummary(Int32 summaryId)
        {
            Summary summary = _summaryRepository.Get(summaryId);

            var compiler = new VacancyCompiler();
            var rule = new SimpleVacancyRule(summary.Specialization, summary.Skills);
            IEnumerable<Vacancy> summaries = compiler.Compile(_vacancyRepository.GetAll(), rule);

            IReadOnlyCollection<VacancySuggestion> vacancySuggestions =
                summaries.Select(s => new VacancySuggestion(summary, s)).ToList();
            vacancySuggestions = _vacancySuggestionRepository.Add(vacancySuggestions).ToList();

            if (!vacancySuggestions.Any())
                throw new ArgumentException("It's no suggestion for summary! Change summary or try later.");

            return vacancySuggestions;
        }

        private IEnumerable<VacancySuggestion> ResetSkippedUser(Int32 userId)
        {
            Summary summary = GetSummaryForUser(userId);
            IEnumerable<VacancySuggestion> vacancySuggestions = _vacancySuggestionRepository.GetForSummaryByState(summary.Id, SuggestionStatus.Skipped).ToList();
            foreach (var vacancySuggestion in vacancySuggestions)
            {
                vacancySuggestion.Reset();
                _vacancySuggestionRepository.Update(vacancySuggestion);
            }
            return vacancySuggestions;
        }
        private IEnumerable<VacancySuggestion> GetAllReadyForUser(Int32 userId)
        {
            Summary summary = GetSummaryForUser(userId);
            IEnumerable<VacancySuggestion> vacancySuggestions = _vacancySuggestionRepository
                .GetForSummaryByState(summary.Id, SuggestionStatus.Ready);
            
            if (!vacancySuggestions.Any())
            {
                if (!_vacancySuggestionRepository.GetAllForSummary(summary.Id).Any())
                    CompileForSummary(summary.Id);
                else
                    ResetSkippedUser(userId);
                vacancySuggestions = _vacancySuggestionRepository
                    .GetForSummaryByState(summary.Id, SuggestionStatus.Ready);
            }
            
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
            Summary summary = GetSummaryForUser(userId);

            VacancySuggestion suggestion = _vacancySuggestionRepository.Get(suggestionId);
            if (suggestion.Summary.Id != summary.Id)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Accept();
            _vacancySuggestionRepository.Update(suggestion);
            _matchService.UpdateMatch(summary.Id, suggestion.Vacancy.Id);
        }

        public void RejectSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Summary summary = GetSummaryForUser(userId);

            VacancySuggestion suggestion = _vacancySuggestionRepository.Get(suggestionId);
            if (suggestion.Summary.Id != summary.Id)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Reject();
            _vacancySuggestionRepository.Update(suggestion);
        }

        public void SkipSuggestionForUser(Int32 userId, Int32 suggestionId)
        {
            Summary summary = GetSummaryForUser(userId);

            VacancySuggestion suggestion = _vacancySuggestionRepository.Get(suggestionId);
            if (suggestion.Summary.Id != summary.Id)
                throw new ArgumentException(
                    $"User with id {userId} don't have access for suggestion with id {suggestion.Id}!");

            suggestion.Skip();
            _vacancySuggestionRepository.Update(suggestion);
        }
    }
}