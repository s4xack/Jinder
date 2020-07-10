using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class SpecializationRepositoryMock : ISpecializationRepository
    {
        private readonly List<Specialization> _specializations;

        public SpecializationRepositoryMock(List<Specialization> specializations)
        {
            _specializations = specializations;
        }

        public SpecializationRepositoryMock() : this(new List<Specialization>())
        {
        }

        public IReadOnlyCollection<Specialization> GetAll()
        {
            return _specializations;
        }

        public Specialization GetByName(String specializationName)
        {
            return _specializations.FirstOrDefault(s => s.Name == specializationName) ??
                   throw new ArgumentException($"No specialization with id {specializationName}!");
        }

        public Specialization Add(Specialization specialization)
        {
            _specializations.Add(specialization);
            return specialization;
        }

        public Specialization DeleteByName(String specializationName)
        {
            Specialization specialization = _specializations.FirstOrDefault(s => s.Name == specializationName) ??
                                            throw new ArgumentException($"No skill with id {specializationName}");
            _specializations.Remove(specialization);
            return specialization;
        }
    }
}