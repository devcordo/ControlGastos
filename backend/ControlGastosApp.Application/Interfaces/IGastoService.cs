using ControlGastosApp.Application.DTOs;

namespace ControlGastosApp.Application.Interfaces
{
    public interface IGastoService
    {
        Task<GastoCreatedResultDto> CreateGastoAsync(CreateGastoDto dto);
    }
}
