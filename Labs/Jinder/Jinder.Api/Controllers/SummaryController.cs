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
    public class SummaryController : ExtendedController
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService, IAccessService accessService) : base(accessService)
        {
            _summaryService = summaryService ?? throw new ArgumentException(nameof(summaryService));
        }

        [HttpGet]
        [Route("get/all")]
        [ProducesResponseType(typeof(List<SummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<SummaryDto>> GetAll([FromHeader] Guid token)
        {
            try
            {
                ValidateToken(token);
                return Ok(_summaryService.GetAll());
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
        [Route("get/{summaryId}")]
        [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SummaryDto> Get([FromHeader] Guid token, Int32 summaryId)
        {
            try
            {
                ValidateToken(token);
                return Ok(_summaryService.Get(summaryId));
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
        [Route("getForUser/{userId}")]
        [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SummaryDto> GetForUser([FromHeader] Guid token, Int32 userId)
        {
            try
            {
                ValidateToken(token);
                return Ok(_summaryService.GetForUser(userId));
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
        [Route("getForUser/me")]
        [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SummaryDto> GetForMe([FromHeader] Guid token)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_summaryService.GetForUser(currentUserId));
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

        [HttpPost]
        [Route("create/")]
        [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SummaryDto> Create([FromHeader] Guid token, [FromBody] CreateSummaryDto summaryData)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_summaryService.CreateForUser(currentUserId, summaryData));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SummaryDto> Delete([FromHeader] Guid token, Int32 summaryId)
        {
            try
            {
                var currentUserId = ValidateToken(token);
                return Ok(_summaryService.Delete(summaryId));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}