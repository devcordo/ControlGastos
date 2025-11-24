namespace ControlGastosApp.Application.DTOs;

public record FondoDto(int Id, string Nombre, string? Tipo, decimal Saldo);
public record CreateFondoDto(string Nombre, string? Tipo, decimal SaldoInicial);
