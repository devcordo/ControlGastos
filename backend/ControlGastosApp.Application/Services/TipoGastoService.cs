using ControlGastosApp.Application.DTOs;
using ControlGastosApp.Application.Interfaces;
using ControlGastosApp.Domain.Entities;
using ControlGastosApp.Domain.Interfaces;

namespace ControlGastosApp.Application.Services
{
    public class TipoGastoService : ITipoGastoService
    {
        private readonly ITipoGastoRepository _repo;

        public TipoGastoService(ITipoGastoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TipoGastoDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(t => new TipoGastoDto(t.Id, t.Codigo, t.Nombre, t.Descripcion));
        }

        public async Task<TipoGastoDto> CreateAsync(CreateTipoGastoDto dto)
        {
            var codigo = await _repo.GetNextCodigoAsync();
            var entity = new TipoGasto { Codigo = codigo, Nombre = dto.Nombre, Descripcion = dto.Descripcion };
            var created = await _repo.AddAsync(entity);
            return new TipoGastoDto(created.Id, created.Codigo, created.Nombre, created.Descripcion);
        }
    }
}
