using Microsoft.EntityFrameworkCore;
using TMovies.Data;
using TMovies.Models;
using TMovies.Repository.Abstract;

namespace TMovies.Repository.Concrete
{
    public class ActorsRepository : IActorsRepository
    {
        private readonly ApplicationDbContext _db;
        public ActorsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Actor> Create(Actor actor)
        {
            await _db.Actors.AddAsync(actor);
            await _db.SaveChangesAsync();
            return actor;
        }

        public async Task<List<Actor>> GetAll()
        {
            return await _db.Actors.ToListAsync();
        }

        public async Task<Actor> GetById(int id)
        {
            return await _db.Actors.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
