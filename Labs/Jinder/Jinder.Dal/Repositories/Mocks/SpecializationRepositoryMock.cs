using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class SpecializationRepositoryMock : ISpecializationRepository
    {
        private readonly List<Specialization> _specializations;
        private Int32 _newId;

        public SpecializationRepositoryMock(List<Specialization> specializations)
        {
            _specializations = specializations;
            _newId = 0;
            foreach (var specialization in _specializations)
                specialization.Id = _newId++;
        }

        public SpecializationRepositoryMock() : this(new List<Specialization>())
        {
        }

        public IReadOnlyCollection<Specialization> GetAll()
        {
            return _specializations;
        }

        public Specialization Get(Int32 specializationId)
        {
            return _specializations.FirstOrDefault(s => s.Id == specializationId) ??
                   throw new ArgumentException($"No specialization with id {specializationId}!");
        }

        public Specialization GetByName(String specializationName)
        {
            return _specializations.FirstOrDefault(s => s.Name == specializationName) ??
                   throw new ArgumentException($"No specialization with id {specializationName}!");
        }

        public Specialization Add(Specialization specialization)
        {
            specialization.Id = _newId++;
            _specializations.Add(specialization);
            return specialization;
        }

        public Specialization Delete(Int32 specializationId)
        {
            Specialization specialization = _specializations.FirstOrDefault(s => s.Id == specializationId) ??
                                            throw new ArgumentException($"No skill with id {specializationId}");
            _specializations.Remove(specialization);
            return specialization;
        }
    }
}