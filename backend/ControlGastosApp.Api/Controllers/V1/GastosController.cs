using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Application.Interfaces;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public async Task<IActionResult> Movimientos([FromQuery] DateTime desde, [FromQuery] DateTime hasta, [FromServices] IGastoRepository gastoRepo, [FromServices] IDepositoRepository depositoRepo, [FromServices] IFondoRepository fondoRepo)
        {
            var gastos = await gastoRepo.GetMovimientoByDateRangeAsync(desde, hasta);
            var depositos = await depositoRepo.GetByDateRangeAsync(desde, hasta);

            var movimientos = new List<MovimientoDto>();

            foreach (var g in gastos)
            {
                var f = await fondoRepo.GetByIdAsync(g.Item1.FondoMonetarioId);

                movimientos.Add(new MovimientoDto
                {
                    Fecha = g.Item1.Fecha,
                    Tipo = "Gasto",
                    Fondo = f?.Nombre,
                    Monto = g.Item2.Monto,
                    TipoGastoId = g.Item2.TipoGastoId,
                    Comercio = g.Item1.NombreComercio,
                    Documento = g.Item1.TipoDocumento,
                    Observaciones = g.Item1.Observaciones
                });
            }

            foreach (var d in depositos)
            {
                var f = await fondoRepo.GetByIdAsync(d.FondoMonetarioId);

                movimientos.Add(new MovimientoDto
                {
                    Fecha = d.Fecha,
                    Tipo = "Deposito",
                    Fondo = f?.Nombre,
                    Monto = d.Monto,
                    TipoGastoId = null,
                    Comercio = string.Empty,
                    Documento = string.Empty,
                    Observaciones = string.Empty
                });
            }

            var ordered = movimientos.OrderBy(m => m.Fecha).ToList();

            return Ok(ordered);
        }
    }
}
