using System;
using Jinder.Core.Clients;
using Refit;

namespace Jinder.Core.Services
{
    public class AccessService : IAccessService
    {
        private readonly IAuthorizeClient _authorizeClient;

        public AccessService(IAuthorizeClient authorizeClient)
        {
            _authorizeClient = authorizeClient ?? throw new ArgumentNullException(nameof(authorizeClient));
        }

        public Int32 ValidateToken(Guid token)
        {
            return _authorizeClient.ValidateToken(token).Result;
        }
    }
}