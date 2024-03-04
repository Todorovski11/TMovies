
using TMovies.Models;

namespace TMovies.Repository.Abstract
{
    public interface ITvShowRepository
    {

        public Task<List<TvShow>> GetAll();

        public Task<TvShow> GetById(int id);

        public Task<TvShow> Delete(int id);

        public Task<TvShow> Create(TvShow tvshow);

        public Task<TvShow> Update(TvShow updatedTvShow);

    }
}
