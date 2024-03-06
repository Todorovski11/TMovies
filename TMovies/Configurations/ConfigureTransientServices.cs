using Microsoft.Extensions.DependencyInjection;
using TMovies.Repository.Abstract;
using TMovies.Repository.Concrete;

namespace TMovies.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureTransientServices(this IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IActorsRepository, ActorsRepository>();
            services.AddTransient<ITvShowRepository, TvShowRepository>();
        }
    }
}
