using Microsoft.EntityFrameworkCore;
using TMovies.Data;
using TMovies.Models;
using TMovies.Repository.Abstract;

namespace TMovies.Repository.Concrete
{
    public class MovieRepository : IMovieRepository
    {

        private readonly ApplicationDbContext _db;
        public MovieRepository(ApplicationDbContext db){
            _db = db;
        }

        public async Task<Movie> Create(Movie movie)
        { 
            await _db.Movies.AddAsync(movie);
            await _db.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Delete(int id)
        {
            var model = await _db.Movies.FirstOrDefaultAsync(x => x.Id == id);
            _db.Movies.Remove(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<List<Movie>> GetAll(){
            return await _db.Movies.Include(m => m.Actors).ThenInclude(ma => ma.Actor).ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _db.Movies.Where(m => m.Id == id)
                                    .Include(m => m.Actors)
                                        .ThenInclude(ma => ma.Actor)
                                     .FirstOrDefaultAsync();

        }

        public async Task<Movie> Update(Movie updatedMovie)
        {
            var existingTvShow = await _db.Movies.FindAsync(updatedMovie.Id);

            if (existingTvShow != null)
            {
                existingTvShow.Title = updatedMovie.Title;
                existingTvShow.Rating = updatedMovie.Rating;
                existingTvShow.ReleaseDate = updatedMovie.ReleaseDate;
                existingTvShow.Plot = updatedMovie.Plot;
                existingTvShow.Director = updatedMovie.Director;
                existingTvShow.ImgLink = updatedMovie.ImgLink;
                existingTvShow.IMDbLink = updatedMovie.IMDbLink;
                existingTvShow.Duration = updatedMovie.Duration;
                existingTvShow.Genre = updatedMovie.Genre;
                existingTvShow.Actors = updatedMovie.Actors;

                await _db.SaveChangesAsync();
                return updatedMovie;
            }
            else
            {
                throw new ArgumentException("Movies not found");
            }
        }

    }
}

