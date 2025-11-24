namespace ControlGastosApp.Domain.Entities
{
    public class GastoEncabezado
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int FondoMonetarioId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string NombreComercio { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<GastoDetalle> Detalles { get; set; } = new();
    }
}
