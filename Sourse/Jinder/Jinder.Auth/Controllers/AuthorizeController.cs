using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jinder.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jinder.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("register/{userId}/{login}/{password}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public ActionResult<Boolean> Register(string login, string password, Int32 userId)
        {
            return Ok(_authorizeService.Register(login, password, userId));
        }

        [HttpGet]
        [Route("validate/login/{login}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public ActionResult<Boolean> ValidateLogin(string login)
        {
            return Ok(_authorizeService.ValidateLogin(login));
        }

        [HttpGet]
        [Route("validate/token/{token}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public ActionResult<Int32> ValidateToken(Guid token)
        {
            return Ok(_authorizeService.ValidateToken(token));
        }
    }
}