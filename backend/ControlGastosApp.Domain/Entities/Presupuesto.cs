namespace ControlGastosApp.Domain.Entities
{
    public class Presupuesto
    {
        public int Id { get; set; }
        public int TipoGastoId { get; set; }
        public int Anio { get; set; }
        public int Mes {  get; set; }
        public decimal Monto { get; set; }
    }
}
