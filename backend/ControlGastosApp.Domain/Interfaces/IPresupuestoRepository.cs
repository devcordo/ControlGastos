using ControlGastosApp.Domain.Entities;

namespace ControlGastosApp.Domain.Interfaces
{
    public interface IPresupuestoRepository : IRepository<Presupuesto>
    {
        Task<Presupuesto?> GetByTipoMesAnioAsync(int tipoGastoId, int mes, int anio);
        Task<IEnumerable<Presupuesto>> GetByRangeAsync(int? tipoGastoId, DateTime desde, DateTime hasta);
    }
}
