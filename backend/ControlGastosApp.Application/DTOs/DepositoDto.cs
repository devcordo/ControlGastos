namespace ControlGastosApp.Application.DTOs;

public record DepositoDto(int Id, DateTime Fecha, int FondoMonetarioId, decimal Monto);
public record CreateDepositoDto(DateTime Fecha, int FondoMonetarioId, decimal Monto);