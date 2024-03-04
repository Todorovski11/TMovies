using TMovies.Models;

namespace TMovies.Repository.Abstract
{
    public interface IActorsRepository
    {
        public Task<List<Actor>> GetAll();
        public Task<Actor> GetById(int id);
        public Task<Actor> Create(Actor actor);
    }
}
