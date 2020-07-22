using System;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services
{
    public interface IVacancySuggestionService
    {
        VacancySuggestionDto GetForMe(Guid token);
        void Accept(Guid token, Int32 suggestionId);
        void Reject(Guid token, Int32 suggestionId);
        void Skip(Guid token, Int32 suggestionId);
    }
}