using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMovies.Data;
using TMovies.Models;
using TMovies.Repository.Abstract;

namespace TMovies.Repository.Concrete
{
    public class TvShowRepository : ITvShowRepository
    {

        private readonly ApplicationDbContext _db;
        public TvShowRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TvShow> Create(TvShow tvshow)
        {
            await _db.TvShows.AddAsync(tvshow);
            await _db.SaveChangesAsync();
            return tvshow;
        }
        public async Task<TvShow> Update(TvShow updatedTvShow)
        {
            var existingTvShow = await _db.TvShows.FindAsync(updatedTvShow.Id);

            if (existingTvShow != null)
            {
                existingTvShow.Title = updatedTvShow.Title;
                existingTvShow.Rating = updatedTvShow.Rating;
                existingTvShow.Year = updatedTvShow.Year;
                existingTvShow.Plot = updatedTvShow.Plot;
                existingTvShow.Director = updatedTvShow.Director;
                existingTvShow.Seasons = updatedTvShow.Seasons;
                existingTvShow.Episodes = updatedTvShow.Episodes;
                existingTvShow.ImgLink = updatedTvShow.ImgLink;
                existingTvShow.IMDbLink = updatedTvShow.IMDbLink;
                existingTvShow.Duration = updatedTvShow.Duration;
                existingTvShow.Genre = updatedTvShow.Genre;
                existingTvShow.Actors = updatedTvShow.Actors;

                await _db.SaveChangesAsync();
                return updatedTvShow;
            }
            else
            {
                throw new ArgumentException("TvShow not found");
            }
        }
        public async Task<TvShow> Delete(int id)
        {
            var model = await _db.TvShows.FirstOrDefaultAsync(x => x.Id == id);
            _db.TvShows.Remove(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<List<TvShow>> GetAll()
        {
            return await _db.TvShows.Include(m => m.Actors).ThenInclude(ma => ma.Actor).ToListAsync();
        }

        public async Task<TvShow> GetById(int id)
        {
            return await _db.TvShows.Where(m => m.Id == id)
                                    .Include(m => m.Actors)
                                        .ThenInclude(ma => ma.Actor)
                                     .FirstOrDefaultAsync();
        }
    }
}
