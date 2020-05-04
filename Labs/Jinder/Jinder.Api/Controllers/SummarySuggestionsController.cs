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
    public class SummarySuggestionsController : ExtendedController
    {
        private readonly ISummarySuggestionService _summarySuggestionService;
        public SummarySuggestionsController(ISummarySuggestionService summarySuggestionService, IAccessService accessService) : base(accessService)
        {
            _summarySuggestionService = summarySuggestionService ?? throw new ArgumentException(nameof(summarySuggestionService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<SummarySuggestionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<SummarySuggestionDto>> GetAll([FromHeader]Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_summarySuggestionService.SuggestAllForUser(currentUserId));
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
        [ProducesResponseType(typeof(SummarySuggestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SummarySuggestionDto> Get([FromHeader]Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_summarySuggestionService.SuggestForUser(currentUserId));
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
        public ActionResult<SummarySuggestionDto> Accept([FromHeader]Guid token, Int32 suggestionId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                _summarySuggestionService.AcceptSuggestionForUser(currentUserId, suggestionId);
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
        public ActionResult<SummarySuggestionDto> Reject([FromHeader]Guid token, Int32 suggestionId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                _summarySuggestionService.RejectSuggestionForUser(currentUserId, suggestionId);
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
        public ActionResult<SummarySuggestionDto> Skip([FromHeader]Guid token, Int32 suggestionId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                _summarySuggestionService.SkipSuggestionForUser(currentUserId, suggestionId);
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