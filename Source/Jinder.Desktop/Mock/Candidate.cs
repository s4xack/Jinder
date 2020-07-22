using System;
using System.Collections.Generic;
using System.Text;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Desktop.Mock
{
    public static class Candidate
    {
        public static Guid Token { get;  } = Guid.NewGuid();
        public static UserDto User { get; } = new UserDto(1, "Ivan", "deadinside@mail.ru", UserType.Candidate);

        public static SummaryDto Summary { get; } = Content.Summaries[0];
        public static List<VacancySuggestionDto> VacancySuggestions { get; } = new List<VacancySuggestionDto>()
        {
            new VacancySuggestionDto(1, Content.Vacancies[0], SuggestionStatus.Ready),
            new VacancySuggestionDto(2, Content.Vacancies[1], SuggestionStatus.Ready),
        };

        public static bool LikeTest = false;
    }
}
