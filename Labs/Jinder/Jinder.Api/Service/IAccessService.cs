using System;

namespace Jinder.Api.Service
{
    public interface IAccessService
    {
        public Int32 ValidateToken(Guid token);
    }
}