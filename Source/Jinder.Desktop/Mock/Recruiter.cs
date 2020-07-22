using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Jinder.Poco.Dto;
using Jinder.Poco.Types;

namespace Jinder.Desktop.Mock
{
    public static class Recruiter
    {
        public static Guid Token { get; } = Guid.NewGuid();
        public static UserDto User { get; } = new UserDto(2, "Shepherd", "hr@veeamacademy.ru", UserType.Recruiter);

        public static VacancyDto Vacancy { get; } = Content.Vacancies[0];
        public static List<SummarySuggestionDto> SummarySuggestions { get; } = new List<SummarySuggestionDto>()
        {
            new SummarySuggestionDto(1, Content.Summaries[0], SuggestionStatus.Ready),
            new SummarySuggestionDto(2, Content.Summaries[1], SuggestionStatus.Ready)
        };
        
        public static bool LikeTest = false;
    }
}