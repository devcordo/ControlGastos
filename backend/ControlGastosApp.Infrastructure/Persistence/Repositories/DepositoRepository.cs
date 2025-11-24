using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosApp.Infrastructure.Persistence.Repositories
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly ApplicationDbContext _db;
        public DepositoRepository(ApplicationDbContext db) => _db = db;

        public async Task<Deposito> AddAsync(Deposito deposito)
        {
            _db.Depositos.Add(deposito);
            await _db.SaveChangesAsync();
            return deposito;
        }

        public Task<IEnumerable<Deposito>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Deposito>> GetByDateRangeAsync(DateTime desde, DateTime hasta) => 
            await _db.Depositos.Where(d => d.Fecha >= desde && d.Fecha <= hasta).ToListAsync();

        public Task<Deposito?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Deposito entity)
        {
            throw new NotImplementedException();
        }
    }
}
