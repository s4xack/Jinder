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
    public class MatchController : ExtendedController
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService, IAccessService accessService) : base(accessService)
        {
            _matchService = matchService ?? throw new ArgumentException(nameof(accessService));
        }

        [HttpGet]
        [Route("candidate/get/all/")]
        [ProducesResponseType(typeof(List<VacancyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<VacancyDto>> GetAllForCandidate([FromHeader]Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_matchService.GetAllForCandidate(currentUserId));
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

        [HttpGet]
        [Route("candidate/get/all/")]
        [ProducesResponseType(typeof(List<VacancyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<VacancyDto>> GetAllForRecruiter([FromHeader]Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_matchService.GetAllForRecruiter(currentUserId));
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