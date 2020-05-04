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
    public class VacancySuggestionController : ExtendedController
    {
        private readonly IVacancySuggestionService _vacancySuggestionService;
        public VacancySuggestionController(IVacancySuggestionService vacancySuggestionService, IAccessService accessService) : base(accessService)
        {
            _vacancySuggestionService = vacancySuggestionService ?? throw new ArgumentException(nameof(vacancySuggestionService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<VacancySuggestionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<VacancySuggestionDto>> GetAll([FromHeader]Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_vacancySuggestionService.SuggestAllForUser(currentUserId));
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
        [Route("get/")]
        [ProducesResponseType(typeof(VacancySuggestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancySuggestionDto> Get([FromHeader]Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_vacancySuggestionService.SuggestForUser(currentUserId));
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
        [Route("get/accept/{suggestionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancySuggestionDto> Accept([FromHeader]Guid token, Int32 suggestionId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                _vacancySuggestionService.AcceptSuggestionForUser(currentUserId, suggestionId);
                return Ok();
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
        [Route("get/reject/{suggestionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancySuggestionDto> Reject([FromHeader]Guid token, Int32 suggestionId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                _vacancySuggestionService.RejectSuggestionForUser(currentUserId, suggestionId);
                return Ok();
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
        [Route("get/skip/{suggestionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VacancySuggestionDto> Skip([FromHeader]Guid token, Int32 suggestionId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                _vacancySuggestionService.SkipSuggestionForUser(currentUserId, suggestionId);
                return Ok();
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