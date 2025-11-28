using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastosApp.Application.DTOs
{
    public class MovimientoDto
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string? Fondo { get; set; }
        public decimal Monto { get; set; }
        public object? TipoGastoId { get; set; }
        public string Comercio { get; set; }
        public string Documento { get; set; }
        public string Observaciones { get; set; }
    }
}
