using System;
using Jinder.Poco.Models;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class SpecializationDto
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public SpecializationDto()
        {
        }

        [JsonConstructor]
        public SpecializationDto(Int32 id, String name)
        {
            Id = id;
            Name = name;
        }

        public static SpecializationDto Create(Specialization specialization)
        {
            return new SpecializationDto(specialization.Id, specialization.Name);
        }

    }
}