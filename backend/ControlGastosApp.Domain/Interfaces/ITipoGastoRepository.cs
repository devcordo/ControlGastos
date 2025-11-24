using ControlGastosApp.Domain.Entities;

namespace ControlGastosApp.Domain.Interfaces
{
    public interface ITipoGastoRepository : IRepository<TipoGasto>
    {
        Task<string> GetNextCodigoAsync();
    }
}
