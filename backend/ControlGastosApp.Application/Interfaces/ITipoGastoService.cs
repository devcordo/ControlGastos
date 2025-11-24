using ControlGastosApp.Application.DTOs;

namespace ControlGastosApp.Application.Interfaces
{
    public interface ITipoGastoService
    {
        Task<IEnumerable<TipoGastoDto>> GetAllAsync();
        Task<TipoGastoDto> CreateAsync(CreateTipoGastoDto dto);
    }
}
