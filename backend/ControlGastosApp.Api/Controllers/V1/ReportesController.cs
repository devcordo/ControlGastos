using ControlGastosApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IPresupuestoRepository _presRepo;
        private readonly IGastoRepository _gastoRepo;
        private readonly ITipoGastoRepository _tipoRepo;

        public ReportesController(IPresupuestoRepository presRepo, IGastoRepository gastoRepo, ITipoGastoRepository tipoRepo)
        {
            _presRepo = presRepo;
            _gastoRepo = gastoRepo;
            _tipoRepo = tipoRepo;
        }

        [HttpGet("presupuesto-ejecucion")]
        public async Task<IActionResult> PresupuestoEjecucion([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var tipos = (await _tipoRepo.GetAllAsync()).ToList();
            var results = new List<object>();

            foreach (var tipo in tipos)
            {
                var presupuestos = (await _presRepo.GetByRangeAsync(tipo.Id, desde, hasta)).Where(p => p.TipoGastoId == tipo.Id);
                var presupuestado = presupuestos.Sum(p => p.Monto);

                decimal ejecutado = 0m;
                var monthStart = new DateTime(desde.Year, desde.Month, 1);
                var monthEnd = new DateTime(hasta.Year, hasta.Month, DateTime.DaysInMonth(hasta.Year, hasta.Month));

                for (var dt = monthStart; dt <= monthEnd; dt = dt.AddMonths(1))
                {
                    ejecutado += await _gastoRepo.GetEjecutadoPorTipoEnPeriodoAsync(tipo.Id, dt.Month, dt.Year);
                }

                results.Add(new { TipoGastoId = tipo.Id, TipoGastoNombre = tipo.Nombre, Presupuestado = presupuestado, Ejecutado = ejecutado });
            }

            return Ok(results);
        }
    }
}
