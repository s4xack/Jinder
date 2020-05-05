﻿using System;
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
            var user = _userRepository.Get(userId);
            if (user.Type != UserType.Recruiter)
                throw new ArgumentException($"User with id {userId} is not recruiter!");
            var vacancy = _vacancyRepository.GetForUser(userId);
            return vacancy.Id;
        }

        private Int32 GetSummaryIdForUser(Int32 userId)
        {
            var user = _userRepository.Get(userId);
            if (user.Type != UserType.Candidate)
                throw new ArgumentException($"User with id {userId} is not candidate!");
            var summary = _summaryRepository.GetForUser(userId);
            return summary.Id;
        }

        public List<SummaryDto> GetAllForRecruiter(Int32 userId)
        {
            var vacancyId = GetVacancyIdForUser(userId);
            var matches = _matchRepository.GetAllForVacancy(vacancyId).Where(m => m.Status == MatchStatus.Full);
            if (!matches.Any())
                throw new ArgumentException($"No matches for user with id{userId}!");
            return matches.Select(m => _summaryRepository.Get(m.SummaryId))
                .Select(SummaryDto.Create)
                .ToList();
        }

        public List<VacancyDto> GetAllForCandidate(Int32 userId)
        {
            var summaryId = GetSummaryIdForUser(userId);
            var matches = _matchRepository.GetAllForVacancy(summaryId).Where(m => m.Status == MatchStatus.Full);
            if (!matches.Any())
                throw new ArgumentException($"No matches for user with id{userId}!");
            return matches.Select(m => _vacancyRepository.Get(m.VacancyId))
                .Select(VacancyDto.Create)
                .ToList();
        }

        public void UpdateMatch(Int32 summaryId, Int32 vacancyId)
        {
            var match = _matchRepository.GetAllForSummary(summaryId).FirstOrDefault(m => m.VacancyId == vacancyId);
            if (match is null)
                _matchRepository.Add(new Match(_matchRepository.NewId, summaryId, vacancyId));
            else
                match.Update();
        }
    }
}