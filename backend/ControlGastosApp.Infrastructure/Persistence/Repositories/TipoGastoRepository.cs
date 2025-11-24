using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosApp.Infrastructure.Persistence.Repositories
{
    public class TipoGastoRepository : ITipoGastoRepository
    {
        private readonly ApplicationDbContext _db;
        public TipoGastoRepository(ApplicationDbContext db) => _db = db;

        public async Task<TipoGasto> AddAsync(TipoGasto entity)
        {
            _db.TipoGastos.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TipoGasto>> GetAllAsync() =>
            await _db.TipoGastos.AsNoTracking().ToListAsync();

        public async Task<TipoGasto?> GetByIdAsync(int id) =>
            await _db.TipoGastos.FindAsync(id);

        public async Task<string> GetNextCodigoAsync()
        {
            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT NEXT VALUE FOR Seq_TipoGasto_Code";
                var res = await cmd.ExecuteScalarAsync();
                await conn.CloseAsync();
                var val = Convert.ToInt32(res);
                return $"TG-{val.ToString("D4")}";
            }
            catch
            {
                var last = await _db.TipoGastos.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
                var next = last == null ? 1 : last.Id + 1;
                return $"TG-{next.ToString("D4")}";
            }
        }

        public Task UpdateAsync(TipoGasto entity)
        {
            throw new NotImplementedException();
        }
    }
}
