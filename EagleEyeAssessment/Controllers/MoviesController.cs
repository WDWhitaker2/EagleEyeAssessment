using EagleEyeAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEyeAssessment.Controllers
{
    [Route("Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet("Stats")]
        public IActionResult Get()
        {
            var results = DBContext.MovieMetadata
                .Where(a=>!a.IsNew)
                .Where(a=>a.Language == "EN")
                .GroupBy(a => a.MovieId)
                .Select(a => {

                    var result = new MovieStatsViewModel();

                    var lastmovie = a.OrderByDescending(b => b.Id).FirstOrDefault();

                    result.MovieId = a.Key;
                    result.Title = lastmovie.Title;
                    result.ReleaseYear = lastmovie.ReleaseYear;
                    result.Watches = DBContext.MovieStats.Where(b => b.MovieId == a.Key).Count();
                    result.AverageWatchDurationS =  (DBContext.MovieStats.Where(b => b.MovieId == a.Key).Average(a=>a.WatchDurationMs))/1000;

                    return result;
                })
                .OrderByDescending(a=>a.Watches)
                .ThenByDescending(a=>a.ReleaseYear)
                .ToList();

            return new JsonResult(results);
        }
    }
}
