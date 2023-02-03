using EagleEyeAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEyeAssessment.Controllers
{
    [Route("Metadata")]
    [ApiController]
    public class MetadataController : ControllerBase
    {

        [HttpPost("")]
        public IActionResult Post([FromForm]MetadataViewModel model)
        {
            var newmovie = new MovieMetadata();
            newmovie.Id = DBContext.MovieMetadata.Select(a => a.Id).OrderByDescending(a => a).FirstOrDefault() + 1;
            newmovie.Language = model.Language;
            newmovie.MovieId = model.MovieId;
            newmovie.ReleaseYear = model.ReleaseYear;
            newmovie.Duration = model.Duration;
            newmovie.Title = model.Title;
            newmovie.IsNew = true;

            DBContext.MovieMetadata.Add(newmovie);

            return RedirectToAction("Get", new { id = newmovie.MovieId });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var movies = DBContext.MovieMetadata
                .Where(a => MetadataIsValid(a))
                .Where(a => a.MovieId == id)
                .GroupBy(a => new { a.MovieId, a.Language })
                .Select(a => a.OrderByDescending(b => b.Id).FirstOrDefault())
                .OrderBy(a=>a.Language)
                .ToList();

            if (movies.Any())
            {
                return new JsonResult(movies);
            }
            else
            {
                return NotFound();
            }
        }

        private bool MetadataIsValid(MovieMetadata a)
        {
            if (String.IsNullOrEmpty(a.Duration)) return false;
            if (String.IsNullOrEmpty(a.Language)) return false;
            if (String.IsNullOrEmpty(a.Title)) return false;

            return true;
        }
    }
}
