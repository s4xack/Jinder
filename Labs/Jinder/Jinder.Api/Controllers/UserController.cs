using System;
using System.Collections.Generic;
using System.Security.Authentication;
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
    public class UserController : AuthenticateController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IAccessService accessService) : base(accessService)
        {
            _userService = userService ?? throw new ArgumentException(nameof(userService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<UserDto>> GetAll([FromHeader] Guid token)
        {
            try
            {
                ValidateToken(token);
                return Ok(_userService.GetAll());
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("get/{userId}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> GetOne([FromHeader] Guid token, Int32 userId)
        {
            try
            {
                ValidateToken(token);
                return Ok(_userService.Get(userId));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("get/me")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserDto> GetMyself(Guid token)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                return Ok(_userService.Get(currentUserId));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}