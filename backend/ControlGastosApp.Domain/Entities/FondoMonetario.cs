namespace ControlGastosApp.Domain.Entities
{
    public class FondoMonetario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Tipo { get; set; }
        public decimal Saldo { get; set; } = 0m;
    }
}
