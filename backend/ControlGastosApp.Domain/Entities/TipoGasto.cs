namespace ControlGastosApp.Domain.Entities
{
    public class TipoGasto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion {  get; set; }
    }
}
