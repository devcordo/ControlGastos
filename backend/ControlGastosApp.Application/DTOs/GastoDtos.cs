namespace ControlGastosApp.Application.DTOs;

public record GastoDetalleDto(int TipoGastoId, decimal Monto);
public record CreateGastoDto(DateTime Fecha, int FondoMonetarioId, string Observaciones, string NombreComercio, string TipoDocumento, IEnumerable<GastoDetalleDto> Detalles);
public record BudgetAlertDto(int TipoGastoId, decimal Presupuestado, decimal Ejecutado, decimal Sobrepaso);
public record GastoCreatedResultDto(int GastoId, IEnumerable<BudgetAlertDto> Alerts);