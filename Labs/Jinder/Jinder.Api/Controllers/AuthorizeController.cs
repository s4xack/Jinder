using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jinder.Core.Services;
using Jinder.Poco.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jinder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthorizeController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService ?? throw new ArgumentNullException(nameof(authorizeService));
        }

        [HttpGet]
        [Route("login/{login}/{password}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Guid> Login(string login, string password)
        {
            try
            {
                return Ok(_authorizeService.Login(login, password));
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("register/{login}/{password}")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public ActionResult<Boolean> Register(string login, string password, [FromBody] CreateUserDto user)
        {
            return Ok(_authorizeService.Register(login, password, user));
        }

        [HttpGet]
        [Route("validate/login/{login}")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public ActionResult<Boolean> ValidateLogin(string login)
        {
            return Ok(_authorizeService.ValidateLogin(login));
        }

        [HttpGet]
        [Route("validate/token/{token}")]
        [ProducesResponseType(typeof(Int32), StatusCodes.Status200OK)]
        public ActionResult<Int32> ValidateToken(Guid token)
        {
            return Ok(_authorizeService.ValidateToken(token));
        }
    }
}