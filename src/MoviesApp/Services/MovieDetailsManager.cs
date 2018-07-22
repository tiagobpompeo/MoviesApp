﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MoviesApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MoviesApp.Services
{
    public class MoviesDetailsManager
    {
        public string content;

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");//ver se é dessa forma
            return client;
        }

        public async Task<MovieDetailsClass> GetAllDetail(MoviesNewClass.Result movie)
        {
            
            string id = movie.id.ToString();
          
            HttpClient client = GetClient();
            var uri = new Uri(string.Format("https://api.themoviedb.org/3/movie/"+id+"?api_key=1f54bd990f1cdfb230adb312546d765d", string.Empty));
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<MovieDetailsClass>(content);

        }

       
    }
}

