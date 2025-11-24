using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosApp.Infrastructure.Persistence.Repositories
{
    public class FondoRepository : IFondoRepository
    {
        private readonly ApplicationDbContext _db;
        public FondoRepository(ApplicationDbContext db) => _db = db;

        public async Task<FondoMonetario> AddAsync(FondoMonetario entity)
        {
            _db.FondosMonetarios.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<FondoMonetario>> GetAllAsync() =>
            await _db.FondosMonetarios.AsNoTracking().ToListAsync();

        public async Task<FondoMonetario?> GetByIdAsync(int id) => 
            await _db.FondosMonetarios.FindAsync(id);

        public async Task UpdateAsync(FondoMonetario entity)
        {
            _db.FondosMonetarios.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
