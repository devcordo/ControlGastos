using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Application.Interfaces;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _gastoService;
        public GastosController(IGastoService gastoService) => _gastoService = gastoService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGastoDto dto)
        {
            var result = await _gastoService.CreateGastoAsync(dto);
            return Ok(result);
        }

        [HttpGet("Movimientos")]
        public async Task<IActionResult> Movimientos([FromQuery] DateTime desde, [FromQuery] DateTime hasta, [FromServices] IGastoRepository gastoRepo, [FromServices] IDepositoRepository depositoRepo)
        {
            var gastos = await gastoRepo.GetMovimientoByDateRangeAsync(desde, hasta);
            var depositos = await depositoRepo.GetByDateRangeAsync(desde, hasta);

            var movimientos = new List<object>();

            movimientos.AddRange(gastos.Select(g => new
            {
                Fecha = g.Item1.Fecha,
                Tipo = "Gasto",
                FondoId = g.Item1.FondoMonetarioId,
                Monto = g.Item2.Monto,
                TipoGastoId = g.Item2.TipoGastoId,
                Comercio = g.Item1.NombreComercio,
                Observaciones = g.Item1.Observaciones
            }));

            movimientos.AddRange(depositos.Select(d => new
            {
                Fecha = d.Fecha,
                Tipo = "Deposito",
                FondoId = d.FondoMonetarioId,
                Monto = d.Monto,
                TipoGastoId = (int?)null,
                Comercio = (string?)null,
                Observaciones = (string?)null
            }));

            var ordered = movimientos.OrderBy(m => ((DateTime)m.GetType().GetProperty("Fecha")!.GetValue(m)!)).ToList();
            return Ok(ordered);
        }
    }
}
