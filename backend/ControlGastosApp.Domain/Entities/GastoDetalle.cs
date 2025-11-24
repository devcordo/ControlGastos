namespace ControlGastosApp.Domain.Entities
{
    public class GastoDetalle
    {
        public int Id { get; set; }
        public int GastoEncabezadoId { get; set; }
        public int TipoGastoId { get; set; }
        public decimal Monto { get; set; }
    }
}
