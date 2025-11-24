using ControlGastosApp.Application.Interfaces;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace ControlGastosApp.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(
            ApplicationDbContext context,
            IGastoRepository gastoRepository,
            IPresupuestoRepository presupuestoRepository,
            IFondoRepository fondoRepository,
            ITipoGastoRepository tipoGastoRepository)
        {
            _context = context;
            GastoRepository = gastoRepository;
            PresupuestoRepository = presupuestoRepository;
            FondoRepository = fondoRepository;
            TipoGastoRepository = tipoGastoRepository;
        }

        public IGastoRepository GastoRepository { get; }
        public IPresupuestoRepository PresupuestoRepository { get; }
        public IFondoRepository FondoRepository { get; }
        public ITipoGastoRepository TipoGastoRepository { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
        }
    }
}
