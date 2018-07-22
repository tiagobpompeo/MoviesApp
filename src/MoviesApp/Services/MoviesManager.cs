using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MoviesApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MoviesApp.Services
{
	public class MoviesManager
	{
        const string Url = "https://api.themoviedb.org/3/movie/upcoming?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US&page=";
        const string UrlGender = "https://api.themoviedb.org/3/genre/movie/list?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US";
        public string content;
        public string page;

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");//ver se é dessa forma
            return client;
        }


        public async Task<MoviesNewClass> GetAll(int page)
        {
            this.page = page.ToString();
            HttpClient client = GetClient();
            var uri = new Uri(string.Format(Url+this.page.ToString(), string.Empty));
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();            
            }

            return JsonConvert.DeserializeObject<MoviesNewClass>(content);
        }

        public async Task<GenreClass> GetAllGenres()
        {
                       
            HttpClient client = GetClient();
            var uri = new Uri(string.Format(UrlGender, string.Empty));
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<GenreClass>(content);
        }

	}
}

