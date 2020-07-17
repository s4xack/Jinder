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
    public class SpecializationController : AuthenticateController
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(IAccessService accessService, ISpecializationService specializationService) : base(accessService)
        {
            _specializationService = specializationService ?? throw new ArgumentException(nameof(specializationService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<SpecializationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<SpecializationDto>> GetAll([FromHeader]Guid token)
        {
            try
            {
                ValidateToken(token);
                return Ok(_specializationService.GetAll());
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

        [HttpPost]
        [Route("create/")]
        [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SpecializationDto> Create([FromHeader] Guid token, [FromBody]SpecializationDto specialization)
        {
            try
            {
                ValidateToken(token);
                return Ok(_specializationService.Create(specialization));
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

        [HttpDelete]
        [Route("delete/{specializationName}")]
        [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SpecializationDto> Delete([FromHeader] Guid token, String specializationName)
        {
            try
            {
                ValidateToken(token);
                return Ok(_specializationService.DeleteByName(specializationName));
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