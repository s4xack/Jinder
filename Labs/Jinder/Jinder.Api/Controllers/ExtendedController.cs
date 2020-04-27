using System;
using Jinder.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jinder.Api.Controllers
{
    public class ExtendedController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public ExtendedController(IAccessService accessService)
        {
            _accessService = accessService ?? throw new ArgumentException(nameof(accessService));
        }

        public Int32 ValidateToken(Guid token)
        {
            return _accessService.ValidateToken(token);
        }
    }
}