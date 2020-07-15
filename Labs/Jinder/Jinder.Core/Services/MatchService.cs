using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.Repositories;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IVacancyRepository _vacancyRepository;

        public MatchService(IMatchRepository matchRepository, IUserRepository userRepository, ISummaryRepository summaryRepository, IVacancyRepository vacancyRepository)
        {
            _matchRepository = matchRepository ?? throw new ArgumentException(nameof(matchRepository));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _summaryRepository = summaryRepository ?? throw new ArgumentException(nameof(summaryRepository));
            _vacancyRepository = vacancyRepository ?? throw new ArgumentException(nameof(vacancyRepository));
        }

        private Int32 GetVacancyIdForUser(Int32 userId)
        {
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Recruiter)
                throw new ArgumentException($"User with id {userId} is not recruiter!");

            Vacancy vacancy = _vacancyRepository.GetForUser(userId);
            return vacancy.Id;
        }

        private Int32 GetSummaryIdForUser(Int32 userId)
        {
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Candidate)
                throw new ArgumentException($"User with id {userId} is not candidate!");

            Summary summary = _summaryRepository.GetForUser(userId);
            return summary.Id;
        }

        public List<SummaryDto> GetAllForRecruiter(Int32 userId)
        {
            Int32 vacancyId = GetVacancyIdForUser(userId);

            IEnumerable<Match> matches = _matchRepository.GetAllForVacancy(vacancyId)
                .Where(m => m.Status == MatchStatus.Full).ToList();
            if (!matches.Any())
                throw new ArgumentException($"No matches for user with id{userId}!");

            return matches.Select(m => m.Summary)
                .Select(SummaryDto.Create)
                .ToList();
        }

        public List<VacancyDto> GetAllForCandidate(Int32 userId)
        {
            Int32 summaryId = GetSummaryIdForUser(userId);

            IEnumerable<Match> matches = _matchRepository.GetAllForVacancy(summaryId)
                .Where(m => m.Status == MatchStatus.Full).ToList();
            if (!matches.Any())
                throw new ArgumentException($"No matches for user with id{userId}!");

            return matches.Select(m => m.Vacancy)
                .Select(VacancyDto.Create)
                .ToList();
        }

        public void UpdateMatch(Int32 summaryId, Int32 vacancyId)
        {
            Match match = _matchRepository.GetAllForSummary(summaryId).FirstOrDefault(m => m.Vacancy.Id == vacancyId);
            if (match is null)
            {
                var summary = _summaryRepository.Get(summaryId);
                var vacancy = _vacancyRepository.Get(vacancyId);
                _matchRepository.Add(new Match(summary, vacancy));
            }
            else
            {
                match.Update();
                _matchRepository.Update(match);
            }
        }
    }
}