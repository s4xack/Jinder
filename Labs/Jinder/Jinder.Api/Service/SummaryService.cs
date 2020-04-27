﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Api.Repository;
using Jinder.Poco.Dto;
using Jinder.Poco.Model;
using Jinder.Poco.Type;

namespace Jinder.Api.Service
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISpecializationRepository _specializationRepository;

        public SummaryService(ISummaryRepository summaryRepository, 
            IUserRepository userRepository, 
            ISkillRepository skillRepository, 
            ISpecializationRepository specializationRepository)
        {
            _summaryRepository = summaryRepository ?? throw new ArgumentException(nameof(summaryRepository));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _skillRepository = skillRepository ?? throw new ArgumentException(nameof(skillRepository));
            _specializationRepository = specializationRepository ?? throw new ArgumentException(nameof(skillRepository));
        }

        public List<SummaryDto> GetAll()
        {
            return _summaryRepository
                .GetAll()
                .Select(SummaryDto.Create)
                .ToList();
        }

        public SummaryDto Get(Int32 summaryId)
        {
            return SummaryDto.Create(_summaryRepository.Get(summaryId));
        }

        public SummaryDto GetForUser(Int32 userId)
        {
            return SummaryDto.Create(_summaryRepository.GetForUser(userId));
        }

        public SummaryDto CreateForUser(Int32 userId, CreateSummaryDto summaryData)
        {
            var user = _userRepository.Get(userId);
            if (user.Type != UserType.Candidate)
                throw new ArgumentException($"Unable to create summary for not candidate user with id {userId}!");
            if (_summaryRepository.IsHaveForUser(userId))
                throw new ArgumentException($"Summary for user with id {userId} have already created!");
            var summary = new Summary
            {
                UserId = userId,
                Specialization = _specializationRepository.GetByName(summaryData.Specialization),
                Skills = summaryData.Skills
                    .Select(s => _skillRepository.GetByName(s))
                    .ToList(),
                Information = summaryData.Information
            };
            summary = _summaryRepository.Create(summary);
            return SummaryDto.Create(summary);
        }

        public SummaryDto Delete(Int32 summaryId)
        {
            return SummaryDto.Create(_summaryRepository.Delete(summaryId));
        }
    }
}