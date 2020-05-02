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
    [Route("api/summarySuggestions")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SummaryCompilationController : ExtendedController
    {
        private readonly ISummaryCompilationService _summaryCompilationService;
        public SummaryCompilationController(ISummaryCompilationService summaryCompilationService, IAccessService accessService) : base(accessService)
        {
            _summaryCompilationService = summaryCompilationService;
        }

        [HttpGet]
        [Route("get/compilation")]
        [ProducesResponseType(typeof(List<SummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<SummaryDto>> GetCompilation([FromQuery]Guid token)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                return Ok(_summaryCompilationService.GetCompilationForUser(currentUserId));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("get/suggestion")]
        [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SummaryDto> GetSuggestion([FromQuery]Guid token)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                return Ok(_summaryCompilationService.GetSuggestionForUser(currentUserId));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Create([FromQuery]Guid token)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                _summaryCompilationService.CreateForUser(currentUserId);
                return Ok();
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("accept/{summaryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AcceptSuggestion([FromQuery]Guid token, Int32 summaryId)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                _summaryCompilationService.AcceptSuggestionForUser(currentUserId, summaryId);
                return Ok();
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("reject/{summaryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult RejectSuggestion([FromQuery]Guid token, Int32 summaryId)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                _summaryCompilationService.RejectSuggestionForUser(currentUserId, summaryId);
                return Ok();
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("skip/{summaryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult SkipSuggestion([FromQuery]Guid token, Int32 summaryId)
        {
            try
            {
                Int32 currentUserId = ValidateToken(token);
                _summaryCompilationService.SkipSuggestionForUser(currentUserId, summaryId);
                return Ok();
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
