namespace ControlGastosApp.Application.DTOs;

public record TipoGastoDto(int Id, string Codigo, string Nombre, string? Descripcion);
public record CreateTipoGastoDto(string Nombre, string? Descripcion);