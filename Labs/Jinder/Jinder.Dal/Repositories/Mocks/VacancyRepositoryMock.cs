using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class VacancyRepositoryMock : IVacancyRepository
    {
        private readonly List<Vacancy> _vacancies;
        private Int32 _newId;

        public VacancyRepositoryMock(List<Vacancy> vacancies)
        {
            _vacancies = vacancies;
            _newId = 0;
            foreach (var vacancy in _vacancies) _newId = Math.Max(_newId, vacancy.Id); 
            _newId++;
        }

        public VacancyRepositoryMock() :
            this(new List<Vacancy>
            {
                new Vacancy(
                    2,
                    0,
                    new Specialization(0, "Spec1"),
                    new List<Skill> {new Skill(0, "Skill1"), new Skill(1, "Skill2")},
                    "Info"),
                new Vacancy(
                    5,
                    1,
                    new Specialization(1, "Spec2"),
                    new List<Skill> {new Skill(2, "Skill3"), new Skill(3, "Skill4")},
                    "Info")
            })
        {
        }

        public IEnumerable<Vacancy> GetAll()
        {
            return _vacancies;
        }

        public Vacancy Get(Int32 vacancyId)
        {
            return _vacancies.FirstOrDefault(s => s.Id == vacancyId) ??
                   throw new ArgumentException($"No vacancy with id {vacancyId}!");
        }

        public Vacancy GetForUser(Int32 userId)
        {
            return _vacancies.FirstOrDefault(s => s.UserId == userId) ??
                   throw new ArgumentException($"No vacancy for user with id {userId}!");
        }

        public Vacancy Create(Vacancy vacancy)
        {
            _vacancies.Add(vacancy);
            return vacancy;
        }

        public Vacancy Delete(Int32 vacancyId)
        {
            var vacancy = _vacancies.FirstOrDefault(s => s.Id == vacancyId) ?? throw new ArgumentException();
            _vacancies.Remove(vacancy);
            return vacancy;
        }

        public Boolean IsHaveForUser(Int32 userId)
        {
            return _vacancies.Exists(s => s.UserId == userId);
        }

        public Int32 NewId { get => _newId++; }
    }
}