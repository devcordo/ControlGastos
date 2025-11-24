namespace ControlGastosApp.Application.DTOs;

public record PresupuestoDto(int Id, int TipoGastoId, int Anio, int Mes, decimal Monto);

public record CreatePresupuestoDto(int TipoGastoId, int Anio, int Mes, decimal Monto);
