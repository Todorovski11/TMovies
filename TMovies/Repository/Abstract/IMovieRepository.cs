using Microsoft.AspNetCore.Mvc;
using TMovies.Models;

namespace TMovies.Repository.Abstract
{
    public interface IMovieRepository
    {

        public Task<List<Movie>> GetAll();

        public Task<Movie> GetById(int id);

        public Task<Movie> Delete(int id);

        public Task<Movie> Create(Movie movie);

        public Task<Movie> Update(Movie updatedMovie);

    }
}
