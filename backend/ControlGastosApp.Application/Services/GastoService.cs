using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Application.Interfaces;
using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;

namespace ControlGastosApp.Application.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _gastoRepo;
        private readonly IPresupuestoRepository _presRepo;
        private readonly IFondoRepository _fondoRepo;
        private readonly IUnitOfWork _uow;

        public GastoService(IGastoRepository gastoRepo, IPresupuestoRepository presRepo, IFondoRepository fondoRepo, IUnitOfWork uow)
        {
            _gastoRepo = gastoRepo;
            _presRepo = presRepo;
            _fondoRepo = fondoRepo;
            _uow = uow;
        }

        public async Task<GastoCreatedResultDto> CreateGastoAsync(CreateGastoDto dto)
        {
            if (dto.Detalles == null || !dto.Detalles.Any())
                throw new ArgumentException("El gasto debe tener al menos un detalle.");

            await _uow.BeginTransactionAsync();

            try
            {
                // Creamos el encabezado y los detalles en un solo objeto
                var encabezado = new GastoEncabezado
                {
                    Fecha = dto.Fecha,
                    FondoMonetarioId = dto.FondoMonetarioId,
                    Observaciones = dto.Observaciones,
                    NombreComercio = dto.NombreComercio,
                    TipoDocumento = dto.TipoDocumento,
                    Detalles = dto.Detalles.Select(d => new GastoDetalle
                    {
                        TipoGastoId = d.TipoGastoId,
                        Monto = d.Monto
                    }).ToList()
                };

                // Calculamos el total
                encabezado.Total = encabezado.Detalles.Sum(d => d.Monto);

                // Insertamos todo de una vez
                await _gastoRepo.AddEncabezadoAsync(encabezado);

                // Actualizamos el fondo monetario
                var fondo = await _fondoRepo.GetByIdAsync(dto.FondoMonetarioId)
                            ?? throw new InvalidOperationException("Fondo no encontrado");
                fondo.Saldo -= encabezado.Total;
                await _fondoRepo.UpdateAsync(fondo);

                // Generamos alertas de presupuesto
                var alerts = new List<BudgetAlertDto>();
                var anio = dto.Fecha.Year;
                var mes = dto.Fecha.Month;

                foreach (var detalle in encabezado.Detalles)
                {
                    var pres = await _presRepo.GetByTipoMesAnioAsync(detalle.TipoGastoId, mes, anio);
                    decimal presupuestado = pres?.Monto ?? 0m;
                    var ejecutado = await _gastoRepo.GetEjecutadoPorTipoEnPeriodoAsync(detalle.TipoGastoId, mes, anio);

                    if (ejecutado > presupuestado)
                    {
                        alerts.Add(new BudgetAlertDto(detalle.TipoGastoId, presupuestado, ejecutado, ejecutado - presupuestado));
                    }
                }

                await _uow.CommitAsync();

                return new GastoCreatedResultDto(encabezado.Id, alerts);
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}
