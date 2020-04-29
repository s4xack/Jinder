using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class VacancyRepositoryMock : IVacancyRepository
    {
        private readonly List<Vacancy> _summaries;
        private Int32 _newId;

        public VacancyRepositoryMock(List<Vacancy> summaries)
        {
            _summaries = summaries;
            _newId = 0;
            foreach (var vacancy in _summaries) vacancy.Id = _newId++;
        }

        public VacancyRepositoryMock() :
            this(new List<Vacancy>
            {
                new Vacancy
                {
                    Id = 0,
                    UserId = 2,
                    Skills = new List<Skill> {new Skill {Id = 0, Name = "Skill1"}, new Skill {Id = 1, Name = "Skill2"}},
                    Specialization = new Specialization {Id = 0, Name = "Spec1"},
                    Information = "Info"
                },
                new Vacancy
                {
                    Id = 1,
                    UserId = 5,
                    Skills = new List<Skill> {new Skill {Id = 2, Name = "Skill3"}, new Skill {Id = 3, Name = "Skill4"}},
                    Specialization = new Specialization {Id = 1, Name = "Spec2"},
                    Information = "Info"
                }
            })
        {
        }

        public IEnumerable<Vacancy> GetAll()
        {
            return _summaries;
        }

        public Vacancy Get(Int32 vacancyId)
        {
            return _summaries.FirstOrDefault(s => s.Id == vacancyId) ??
                   throw new ArgumentException($"No vacancy with id {vacancyId}!");
        }

        public Vacancy GetForUser(Int32 userId)
        {
            return _summaries.FirstOrDefault(s => s.UserId == userId) ??
                   throw new ArgumentException($"No vacancy for user with id {userId}!");
        }

        public Vacancy Create(Vacancy vacancy)
        {
            vacancy.Id = _newId++;
            _summaries.Add(vacancy);
            return vacancy;
        }

        public Vacancy Delete(Int32 vacancyId)
        {
            var vacancy = _summaries.FirstOrDefault(s => s.Id == vacancyId) ?? throw new ArgumentException();
            _summaries.Remove(vacancy);
            return vacancy;
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return _summaries.Exists(s => s.UserId == userId);
        }
    }
}