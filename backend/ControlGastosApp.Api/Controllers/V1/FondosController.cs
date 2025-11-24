using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FondosController : ControllerBase
    {
        private readonly IFondoRepository _repo;
        private readonly IDepositoRepository _depositoRepo;
        public FondosController(IFondoRepository repo, IDepositoRepository depositoRepo)
        {
            _repo = repo;
            _depositoRepo = depositoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fondos = await _repo.GetAllAsync();
            var dtos = fondos.Select(f => new FondoDto(f.Id, f.Nombre, f.Tipo, f.Saldo));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFondoDto dto)
        {
            var entity = new FondoMonetario { Nombre = dto.Nombre, Tipo = dto.Tipo, Saldo = dto.SaldoInicial };
            var created = await _repo.AddAsync(entity);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, new FondoDto(created.Id, created.Nombre, created.Tipo, created.Saldo));
        }

        [HttpPost("{id}/depositos")]
        public async Task<IActionResult> Deposito(int id, [FromBody] CreateDepositoDto dto)
        {
            var fondo = await _repo.GetByIdAsync(id);
            if (fondo == null) return NotFound();

            var deposito = new Deposito { Fecha = dto.Fecha, FondoMonetarioId = id, Monto = dto.Monto };
            await _depositoRepo.AddAsync(deposito);

            fondo.Saldo += dto.Monto;
            await _repo.UpdateAsync(fondo);

            return Ok(new { deposito.Id, fondo.Saldo });
        }
    }
}
