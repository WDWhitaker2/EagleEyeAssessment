using EagleEyeAssessment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EagleEyeAssessment
{
    public static class DBContext
    {
        public static List<MovieMetadata> MovieMetadata = new List<MovieMetadata>();
        public static List<MovieStats> MovieStats = new List<MovieStats>();
        public static void LoadMoviesFromFile()
        {

            var metadatafilelines = ReadAllResourceLines(@"moviemetadata.csv");
            MovieMetadata = metadatafilelines.Skip(1).Select(a => BuildMovieMetadataFromString(a)).ToList();

            var statfilelines = ReadAllResourceLines(@"stats.csv");
            MovieStats = statfilelines.Skip(1).Select(a => BuildMovieStatsFromString(a)).ToList();

        }
        static string[] ReadAllResourceLines(string resourceName)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), resourceName);
            return File.ReadAllLines(path);
        }
        static MovieMetadata BuildMovieMetadataFromString(string csvLine)
        {

            string[] values = Regex.Split(csvLine, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            MovieMetadata movieMetadata = new MovieMetadata();
            //Id,MovieId,Title,Language,Duration,ReleaseYear
            movieMetadata.Id = Convert.ToInt32(values[0]);
            movieMetadata.MovieId = Convert.ToInt32(values[1]);
            movieMetadata.Title = values[2];
            movieMetadata.Language = values[3];
            movieMetadata.Duration = values[4];
            movieMetadata.ReleaseYear = Convert.ToInt32(values[5]);
            movieMetadata.IsNew = false;

            return movieMetadata;
        }
        static MovieStats BuildMovieStatsFromString(string csvLine)
        {
            string[] values = csvLine.Split(',');

            MovieStats movieMetadata = new MovieStats();
            //Id,MovieId,Title,Language,Duration,ReleaseYear
            movieMetadata.MovieId = Convert.ToInt32(values[0]);
            movieMetadata.WatchDurationMs = Convert.ToUInt64(values[1]);

            return movieMetadata;
        }
    }
}
