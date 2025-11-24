using ControlGastosApp.Domain.Interfaces;

namespace ControlGastosApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGastoRepository GastoRepository { get; }
        IPresupuestoRepository PresupuestoRepository { get; }
        IFondoRepository FondoRepository { get; }
        ITipoGastoRepository TipoGastoRepository { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
