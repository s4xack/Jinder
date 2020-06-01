﻿using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface ISpecializationRepository
    {
        IReadOnlyCollection<Specialization> GetAll();
        Specialization Get(Int32 specializationId);
        Specialization GetByName(String specializationName);
        Specialization Add(Specialization specialization);
        Specialization Delete(Int32 specializationId);
    }
}