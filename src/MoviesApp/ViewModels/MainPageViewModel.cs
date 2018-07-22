using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MoviesApp.Services;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace MoviesApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        //readonly IList<Models.MoviesNewClass.Result> ListMovies = new ObservableCollection<Models.MoviesNewClass.Result>();
        public INavigationService _navigationService;
        public readonly MoviesManager managerMovies = new MoviesManager();
        public int moviescount;
        public string genreOut;
        public string _poster_path;
        public string _title;
        public string _release_date;
        public string _genresOut;
        public List<MoviesApp.Models.MoviesNewClass.Result> _results;

        public ObservableCollection<MoviesApp.Models.MoviesNewClass.Result> ListMovies { get; set; }

        public List<MoviesApp.Models.MoviesNewClass.Result> results
        {
            get { return _results; }
            set { SetProperty(ref _results, value); }
        }

        public DelegateCommand<MoviesApp.Models.MoviesNewClass.Result> ItemMoviesTappedCommand { get; set; }


        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "Main Page";
           ListMovies = new ObservableCollection<MoviesApp.Models.MoviesNewClass.Result>();
            ReturnData();
            ItemMoviesTappedCommand = new DelegateCommand<MoviesApp.Models.MoviesNewClass.Result>(ItemMovie);
            _navigationService = navigationService;
        }


        private async void ReturnData()
        {
            var listGenres = await managerMovies.GetAllGenres();
            var moviesCollection = await managerMovies.GetAll(1);//page 1//create command to continue...

            if (moviesCollection.results != null)
            {
                foreach (Models.MoviesNewClass.Result outView in moviesCollection.results)
                {
                    //var id_movie = outView.id;//id movie for detail
                    var genreIds = outView.genre_ids;//list id genres of movie in upcoming compare to another list                   
                    var genreListAll = listGenres.genres;//lista all genres

                    foreach (var a in genreIds)
                    {
                        int num = a;
                        foreach (var b in genreListAll)
                        {
                            var id = b.id;
                            if (num == id)
                            {
                                genreOut = String.Concat(b.name + "");
                            }
                        }
                    }

                    //string poster_path_generator = "https://image.tmdb.org/t/p/w200" + outView.poster_path;


                    ListMovies.Add(new Models.MoviesNewClass.Result
                    {
                        id = outView.id,
                        poster_path = "https://image.tmdb.org/t/p/w200" + outView.poster_path,
                        title = outView.title,
                        release_date = outView.release_date,
                        genresOut = genreOut
                    });

                }
            }

        }


        private void ItemMovie(Models.MoviesNewClass.Result movie)
        {
            var m = movie;
            NavigationParameters param = new NavigationParameters();
            param.Add("movies", movie);
            _navigationService.NavigateAsync("MoviesDetails", param);

        }



        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO: Implement your initialization logic
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            // TODO: Handle any final tasks before you navigate away
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    // TODO: Handle any tasks that should occur only when navigated back to
                    break;
                case NavigationMode.New:
                    // TODO: Handle any tasks that should occur only when navigated to for the first time
                    break;
            }

            // TODO: Handle any tasks that should be done every time OnNavigatedTo is triggered
        }
    }
}