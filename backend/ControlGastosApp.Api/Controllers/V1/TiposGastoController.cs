using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TiposGastoController : ControllerBase
    {
        private readonly ITipoGastoService _service;
        public TiposGastoController(ITipoGastoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTipoGastoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}