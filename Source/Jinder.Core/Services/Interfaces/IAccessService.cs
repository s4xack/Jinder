using System;

namespace Jinder.Core.Services
{
    public interface IAccessService
    {
        Int32 ValidateToken(Guid token);
    }
}