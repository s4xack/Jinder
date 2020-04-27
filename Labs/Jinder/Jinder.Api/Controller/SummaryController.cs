﻿using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Jinder.Api.Service;
using Jinder.Poco.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jinder.Api.Controller
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
        public IActionResult Create([FromHeader] Guid token, [FromBody] CreateSummaryDto newSummary)
        {
            throw new NotSupportedException();
        }

        [HttpDelete]
        public IActionResult Delete([FromHeader] Guid token, Int32 summaryId)
        {
            throw new NotSupportedException();
        }
    }
}