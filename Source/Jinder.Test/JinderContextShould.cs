using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Api;
using Jinder.Core.Tools;
using Jinder.Dal;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Jinder.Test
{
    [TestFixture]
    public class JinderContextShould
    {
        // SHIT: separate to multiple tests
        [Test]
        public void Should_CRUD_correctly()
        {
            var user = new User("email", "Ivan", UserType.Candidate);
            var specialization = new Specialization("Specialization");
            var skills = new List<Skill> {new Skill("Skill1"), new Skill("Skill2"), new Skill("Skill3")};
            

            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("JinderTest")
                .Options;

            using var context = new JinderContext(options);

            var dbUser = DbUser.FromModel(user);
            var dbSpecialization = DbSpecialization.FromModel(specialization);
            var dbSkills = skills.Select(DbSkill.FromModel).ToList();

            context.Users.Add(dbUser);
            context.Specializations.Add(dbSpecialization);
            context.Skills.AddRange(dbSkills);
            context.SaveChanges();

            user = context.Users.SingleOrDefault()?.ToModel();
            specialization = context.Specializations.SingleOrDefault()?.ToModel();
            skills = context.Skills.Select(s => s.ToModel()).ToList();

            Assert.IsNotNull(user);
            Assert.IsNotNull(specialization);
            Assert.IsNotNull(skills.Aggregate((a, b) => (a is null) || (b is null) ? null : a));

            var summary = new Summary(user, specialization, skills, "Information");
            var dbSummary = DbSummary.FromModel(summary);

            context.Summaries.Add(dbSummary);
            context.SaveChanges();

            summary = context.Summaries.SingleOrDefault()?.ToModel();
            Assert.IsNotNull(summary);

            context.Summaries.Remove(dbSummary);
            context.SaveChanges();

            user = context.Users.SingleOrDefault()?.ToModel();
            specialization = context.Specializations.SingleOrDefault()?.ToModel();
            skills = context.Skills.Select(s => s.ToModel()).ToList();
            summary = context.Summaries.SingleOrDefault()?.ToModel();

            Assert.IsNotNull(user);
            Assert.IsNotNull(specialization);
            Assert.IsNotNull(skills.Aggregate((a, b) => (a is null) || (b is null) ? null : a));
            Assert.IsNull(summary);

            summary = new Summary(user, specialization, skills, "Information");
            dbSummary = DbSummary.FromModel(summary);

            Assert.DoesNotThrow(() => context.Summaries.Add(dbSummary));
        }
    }
}