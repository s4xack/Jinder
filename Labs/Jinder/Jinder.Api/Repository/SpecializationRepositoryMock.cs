﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Model;

namespace Jinder.Api.Repository
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
            {
                specialization.Id = _newId++;
            }
        }

        public SpecializationRepositoryMock() :
            this(new List<Specialization>
            {
                new Specialization{Id = 0, Name = "Spec1"},
                new Specialization{Id = 1, Name = "Spec2"}
            })
        {
        }

        public List<Specialization> GetAll()
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
            var specialization = _specializations.FirstOrDefault(s => s.Id == specializationId) ??
                        throw new ArgumentException($"No skill with id {specializationId}");
            _specializations.Remove(specialization);
            return specialization;
        }
    }
}