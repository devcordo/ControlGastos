using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestoRepository _repo;
        public PresupuestosController(IPresupuestoRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var presupuestos = await _repo.GetAllAsync();
            var dtos = presupuestos.Select(p => new PresupuestoDto(p.Id, p.TipoGastoId, p.Anio, p.Mes, p.Monto));
            var ordered = dtos.OrderBy(o => o.Anio).OrderBy(o => o.Mes);
            return Ok(ordered);
        }

        [HttpGet("Mes")]
        public async Task<IActionResult> GetByMesAnio([FromQuery] int mes, [FromQuery] int anio)
        {
            var presupuestos = await _repo.GetByMesAnioAsync(mes, anio);
            var dtos = presupuestos.Select(p => new PresupuestoDto(p.Id, p.TipoGastoId, p.Anio, p.Mes, p.Monto));
            var ordered = dtos.OrderBy(o => o.Anio).OrderBy(o => o.Mes);
            return Ok(ordered);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePresupuestoDto dto)
        {
            var existing = await _repo.GetByTipoMesAnioAsync(dto.TipoGastoId, dto.Mes, dto.Anio);
            if (existing is not null)
            {
                existing.Monto = dto.Monto;
                await _repo.UpdateAsync(existing);
                return Ok(new PresupuestoDto(existing.Id, existing.TipoGastoId, existing.Anio, existing.Mes, existing.Monto));
            }

            var entity = new Presupuesto { TipoGastoId = dto.TipoGastoId, Mes = dto.Mes, Anio = dto.Anio, Monto = dto.Monto };
            await _repo.AddAsync(entity);
            return CreatedAtAction(nameof(Create), new { id = entity.Id }, new PresupuestoDto(entity.Id, entity.TipoGastoId, entity.Anio, entity.Mes, entity.Monto));
        }
    }
}
