

using ControlGastosApp.Domain.Entities;

namespace ControlGastosApp.Domain.Interfaces
{
    public interface IGastoRepository
    {
        Task AddEncabezadoAsync(GastoEncabezado encabezado);
        Task AddDetalleAsync(IEnumerable<GastoDetalle> detalles);
        Task<decimal> GetEjecutadoPorTipoEnPeriodoAsync(int tipoGastoId, int mes, int anio);
        Task<IEnumerable<(GastoEncabezado Encabezado, GastoDetalle Detalle)>> GetMovimientoByDateRangeAsync(DateTime desde, DateTime hasta);
    }
}
