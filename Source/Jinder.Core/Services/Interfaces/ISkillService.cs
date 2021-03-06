﻿using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface ISkillService
    {
        IReadOnlyCollection<SkillDto> Get();
        SkillDto Create(SkillDto skill);
        SkillDto DeleteByName(String skillName);
    }
}