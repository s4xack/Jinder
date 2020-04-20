using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jinder.Poco.Dto;
using Jinder.Poco.Type;

namespace Jinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<UserDto> _users;

        static UserController()
        {
            _users = new List<UserDto>
            {
                new UserDto
                {
                    Id = Guid.NewGuid(), 
                    Email = "candidate@gmail.ru", 
                    Name = "Ivan", 
                    Type = UserType.Candidate
                },
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Email = "recruiter@veeam.ru",
                    Name = "Ovcharka",
                    Type = UserType.Recruiter
                },
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Email = "hacker@gmail.ru",
                    Name = "Admin",
                    Type = UserType.Administrator
                }
            };


        }

        [HttpGet]
        [Route("all")]
        [Consumes("application/json")]
        public List<UserDto> GetAllUsers()
        {
            return _users;
        }

        [HttpGet]
        [Route("get/{userId}")]
        [Consumes("application/json")]
        public UserDto GetUser(Guid userId)
        {
            return _users.First(user => user.Id == userId);
        }
    }
}