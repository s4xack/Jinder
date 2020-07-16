using System;
using Jinder.Core.Clients;
using Refit;

namespace Jinder.Core.Services
{
    public class AccessService : IAccessService
    {
        private readonly IAuthorizeClient _authorizeClient;

        public AccessService()
        {
            _authorizeClient = RestService.For<IAuthorizeClient>("http://localhost:64642");
        }

        public Int32 ValidateToken(Guid token)
        {
            return _authorizeClient.ValidateToken(token).Result;
        }
    }
}