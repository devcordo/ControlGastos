using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosApp.Infrastructure.Persistence.Repositories
{
    public class GastoRepository : IGastoRepository
    {
        private readonly ApplicationDbContext _db;
        public GastoRepository(ApplicationDbContext db) => _db = db;

        public async Task AddEncabezadoAsync(GastoEncabezado encabezado)
        {
            _db.GastosEncabezado.Add(encabezado);
            await _db.SaveChangesAsync();
        }

        public async Task AddDetalleAsync(IEnumerable<GastoDetalle> detalles)
        {
            _db.GastosDetalle.AddRange(detalles);
            await _db.SaveChangesAsync();
        }

        public async Task<decimal> GetEjecutadoPorTipoEnPeriodoAsync(int tipoGastoId, int mes, int anio)
        {
            var sum = await (from d in _db.GastosDetalle
                             join e in _db.GastosEncabezado on d.GastoEncabezadoId equals e.Id
                             where d.TipoGastoId == tipoGastoId
                                   && e.Fecha.Year == anio && e.Fecha.Month == mes
                             select d.Monto).SumAsync();
            return sum;
        }

        public async Task<IEnumerable<(GastoEncabezado Encabezado, GastoDetalle Detalle)>> GetMovimientoByDateRangeAsync(DateTime desde, DateTime hasta)
        {
            var query = from e in _db.GastosEncabezado
                        join d in _db.GastosDetalle on e.Id equals d.GastoEncabezadoId
                        where e.Fecha.Date >= desde.Date && e.Fecha.Date <= hasta.Date
                        select new { Enc = e, Det = d };

            var list = await query.ToListAsync();
            return list.Select(x => (x.Enc, x.Det));
        }
    }
}
