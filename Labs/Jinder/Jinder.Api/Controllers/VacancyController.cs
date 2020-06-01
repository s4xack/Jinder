using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
    public class VacancyController : AuthenticateController
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService, IAccessService accessService) : base(accessService)
        {
            _vacancyService = vacancyService ?? throw new ArgumentException (nameof(vacancyService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<VacancyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<VacancyDto>> GetAll([FromHeader] Guid token)
        {
            try
            {
                ValidateToken(token);
                return Ok(_vacancyService.GetAll());
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
        [Route("get/{vacancyId}")]
        [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancyDto> Get([FromHeader] Guid token, Int32 vacancyId)
        {
            try
            {
                ValidateToken(token);
                return Ok(_vacancyService.Get(vacancyId));
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
        [Route("getForUser/{userId}")]
        [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancyDto> GetForUser([FromHeader] Guid token, Int32 userId)
        {
            try
            {
                ValidateToken(token);
                return Ok(_vacancyService.GetForUser(userId));
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
        [Route("getForUser/me")]
        [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancyDto> GetForMe([FromHeader] Guid token)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                return Ok(_vacancyService.GetForUser(currentUserId));
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
        [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VacancyDto> Create([FromHeader] Guid token, [FromBody] CreateVacancyDto vacancyData)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                return Ok(_vacancyService.CreateForUser(currentUserId, vacancyData));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VacancyDto> Delete([FromHeader] Guid token, Int32 vacancyId)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                return Ok(_vacancyService.Delete(vacancyId));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
        }
    }
}
