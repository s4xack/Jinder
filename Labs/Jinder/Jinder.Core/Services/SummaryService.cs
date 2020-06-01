using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.Repositories;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Core.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IUserRepository _userRepository;

        public SummaryService(ISummaryRepository summaryRepository,
            IUserRepository userRepository,
            ISkillRepository skillRepository,
            ISpecializationRepository specializationRepository)
        {
            _summaryRepository = summaryRepository ?? throw new ArgumentException(nameof(summaryRepository));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _skillRepository = skillRepository ?? throw new ArgumentException(nameof(skillRepository));
            _specializationRepository =
                specializationRepository ?? throw new ArgumentException(nameof(skillRepository));
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
            User user = _userRepository.Get(userId);
            if (user.Type != UserType.Candidate)
                throw new ArgumentException($"Unable to create summary for not candidate user with id {userId}!");

            if (_summaryRepository.IsHaveForUser(userId))
                throw new ArgumentException($"Summary for user with id {userId} have already created!");

            var summary = new Summary(
                userId,
                _summaryRepository.NewId,
                _specializationRepository.GetByName(summaryData.Specialization),
                summaryData.Skills
                    .Select(s => _skillRepository.GetByName(s))
                    .ToList(),
                summaryData.Information);
            summary = _summaryRepository.Create(summary);

            return SummaryDto.Create(summary);
        }

        public SummaryDto Delete(Int32 summaryId)
        {
            return SummaryDto.Create(_summaryRepository.Delete(summaryId));
        }
    }
}