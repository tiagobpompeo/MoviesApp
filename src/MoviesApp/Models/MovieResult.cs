using System;
using System.Collections.Generic;

namespace MoviesApp.Models
{
    public class MovieResult
    {
        public List<ResultMovie> resultsMovies { get; set; }

        public class ResultMovie
        {

            public int id { get; set; }
            public string title { get; set; }
            public string poster_path { get; set; }
            public string release_date { get; set; }
            public string genresOut { get; set; }
        }

    }
}