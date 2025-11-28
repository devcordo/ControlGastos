using ControlGastosApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DepositosController : ControllerBase
    {
        private readonly IDepositoRepository _repo;
        public DepositosController(IDepositoRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("Range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var depositos = await _repo.GetByDateRangeAsync(desde, hasta);
            return Ok(depositos);
        }
    }
}
