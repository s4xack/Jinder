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
    public class SkillController : AuthenticateController
    {
        private readonly ISkillService _skillService;

        public SkillController(IAccessService accessService, ISkillService skillService) : base(accessService)
        {
            _skillService = skillService ?? throw new ArgumentException(nameof(skillService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<SkillDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<SkillDto>> Get([FromHeader]Guid token)
        {
            try
            {
                ValidateToken(token);
                return Ok(_skillService.Get());
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
        [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SkillDto> Create([FromHeader] Guid token, [FromBody]SkillDto skill)
        {
            try
            {
                ValidateToken(token);
                return Ok(_skillService.Create(skill));
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
        [Route("delete/{skillName}")]
        [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SkillDto> Delete([FromHeader] Guid token, String skillName)
        {
            try
            {
                ValidateToken(token);
                return Ok(_skillService.DeleteByName(skillName));
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