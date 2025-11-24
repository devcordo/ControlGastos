using ControlGastosApp.Domain.Entities;

namespace ControlGastosApp.Domain.Interfaces
{
    public interface IDepositoRepository : IRepository<Deposito>
    {
        Task<IEnumerable<Deposito>> GetByDateRangeAsync(DateTime desde, DateTime hasta);
    }
}
