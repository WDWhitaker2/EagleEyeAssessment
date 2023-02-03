using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEyeAssessment.Models
{
    public class MovieStatsViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public decimal AverageWatchDurationS { get; set; }
        public int  Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
