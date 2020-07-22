using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface ISpecializationRepository
    {
        IReadOnlyCollection<Specialization> GetAll();
        Specialization GetByName(String specializationName);
        Specialization Add(Specialization specialization);
        Specialization DeleteByName(String specializationName);
    }
}