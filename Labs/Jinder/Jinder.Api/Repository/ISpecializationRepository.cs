using System;
using System.Collections.Generic;
using Jinder.Poco.Model;

namespace Jinder.Api.Repository
{
    public interface ISpecializationRepository
    {
        public List<Specialization> GetAll();
        public Specialization Get(Int32 specializationId);
        public Specialization GetByName(String specializationName);
        public Specialization Add(Specialization specialization);
        public Specialization Delete(Int32 specializationId);
    }
}