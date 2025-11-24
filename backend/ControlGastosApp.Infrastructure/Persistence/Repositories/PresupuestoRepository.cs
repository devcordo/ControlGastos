using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosApp.Infrastructure.Persistence.Repositories
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly ApplicationDbContext _db;
        public PresupuestoRepository(ApplicationDbContext db) => _db = db;

        public async Task<Presupuesto> AddAsync(Presupuesto entity)
        {
            _db.Presupuestos.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Presupuesto?> GetByTipoMesAnioAsync(int tipoGastoId, int mes, int anio) =>
            await _db.Presupuestos.AsNoTracking().FirstOrDefaultAsync(p => p.TipoGastoId == tipoGastoId && p.Mes == mes && p.Anio == anio);

        public async Task UpdateAsync(Presupuesto entity)
        {
            _db.Presupuestos.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Presupuesto>> GetByRangeAsync(int? tipoGastoId, DateTime desde, DateTime hasta)
        {
            return await _db.Presupuestos.AsNoTracking().ToListAsync();
        }

        public Task<Presupuesto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Presupuesto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
