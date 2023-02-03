using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EagleEyeAssessment.Models
{
    public class MovieMetadata
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }
        [JsonIgnoreAttribute]
        public bool IsNew { get; set; }
    }
}
