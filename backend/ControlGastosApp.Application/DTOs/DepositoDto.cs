namespace ControlGastosApp.Application.DTOs;

public record CreateDepositoDto(DateTime Fecha, int FondoMonetarioId, decimal Monto);